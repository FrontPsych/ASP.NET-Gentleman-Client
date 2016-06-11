using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using NLog;
using NLog.Internal;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace DAL
{
    public class WebApiUOW : IUOW, IWebApiUOW, IDisposable
    {

        private readonly IAuthenticationManager _authenticationManager;

        // repo factories (delegate method for creating repos)
        private readonly IDictionary<Type, Func<HttpClient, IAuthenticationManager, object>> _repositoryFactories;

        // cache for already created repos
        private readonly IDictionary<Type, object> _repositories = new Dictionary<Type, object>();

        // httpclient, common for all repos
        private readonly HttpClient _httpClient = new HttpClient();

        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public WebApiUOW(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
            // initialize list of repo factories
            _repositoryFactories = GetCustomFactories();

            // set up httpclient
            var baseAddr = ConfigurationManager.AppSettings["WebApi_BaseUri"];
            if (string.IsNullOrWhiteSpace(baseAddr))
            {
                throw new KeyNotFoundException("WebApi_BaseAddress not defined in Web.config!");
            }
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.BaseAddress = new Uri(baseAddr);

        }

        public string GetWebApiToken(string userName, string password)
        {
            var bearerToken = "";
            using (var tokenHttpClient = new HttpClient())
            {
                var tokenAddr = ConfigurationManager.AppSettings["WebApi_TokenUri"];
                if (string.IsNullOrWhiteSpace(tokenAddr))
                {
                    throw new KeyNotFoundException("WebApi_TokenUri not defined in config!");
                }
                tokenHttpClient.BaseAddress = new Uri(tokenAddr);
                tokenHttpClient.DefaultRequestHeaders.Accept.Clear();
                tokenHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var tokenContent = new StringContent($"grant_type=password&username={userName}&password={password}");

                tokenContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded")
                {
                    CharSet = "UTF-8"
                };

                var response = tokenHttpClient.PostAsync("", tokenContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    var tokenResult = response.Content.ReadAsStringAsync().Result;
                    bearerToken = JObject.Parse(tokenResult)["access_token"].ToString();
                }
                else
                {
                    _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
                }

            }
            return bearerToken;
        }

        // list of all delegate methods for creating our repos when needed
        private static IDictionary<Type, Func<HttpClient, IAuthenticationManager, object>> GetCustomFactories()
        {
            return new Dictionary<Type, Func<HttpClient, IAuthenticationManager, object>>
            {
                    {typeof(IArticleRepository), (httpClient, authenticationManager) => new ArticleRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_Articles"], authenticationManager)},
                    {typeof(IUserIntRepository), (httpClient, authenticationManager) => new UserIntRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_UsersInt"], authenticationManager)},
                    {typeof(IUserRoleIntRepository), (httpClient, authenticationManager) => new UserRoleIntRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_UserRolesInt"], authenticationManager)},
                    {typeof(IRoleIntRepository), (httpClient, authenticationManager) => new RoleIntRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_RolesInt"], authenticationManager)},
                    {typeof(IUserClaimIntRepository), (httpClient, authenticationManager) => new UserClaimIntRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_UserClaimsInt"], authenticationManager)},
                    {typeof(IUserLoginIntRepository), (httpClient, authenticationManager) => new UserLoginIntRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_UserLoginsInt"], authenticationManager)},
                    {typeof(IGameRepository), (httpClient, authenticationManager) => new GameRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_Games"], authenticationManager)},
                    {typeof(IGameTypeRepository), (httpClient, authenticationManager) => new GameTypeRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_GameTypes"], authenticationManager)},
                    {typeof(IGameRowTypesRepository), (httpClient, authenticationManager) => new GameRowTypesRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_GameRowTypes"], authenticationManager)},
                    {typeof(IUserGameRowRepository), (httpClient, authenticationManager) => new UserGameRowRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_UserGameRows"], authenticationManager)},
                    {typeof(IGameRowRepository), (httpClient, authenticationManager) => new GameRowRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_GameRows"], authenticationManager)},
                    {typeof(IFriendRepository), (httpClient, authenticationManager) => new FriendRepository(httpClient, ConfigurationManager.AppSettings["WebApi_EndPoint_Friends"], authenticationManager)},


            };
        }


        /// <summary>
        /// Returns repo instance by repo interface
        /// Repo is first searched from cache, if not found then its created new and stored into cache
        /// </summary>
        /// <typeparam name="TRepo">Repo interface</typeparam>
        /// <returns></returns>
        public TRepo GetRepository<TRepo>() where TRepo : class
        {
            var repo = GetWebApiRepo<TRepo>() as TRepo;
            if (repo == null)
            {
                throw new NotImplementedException("No repository for type, " + typeof(TRepo).FullName);
            }
            return repo;
        }

        private TRepo GetWebApiRepo<TRepo>() where TRepo : class
        {

            // Look for TRepo in dictionary cache under typeof(TRepo).
            object repo;
            _repositories.TryGetValue(typeof(TRepo), out repo);
            if (repo != null)
            {
                return (TRepo)repo;
            }

            return MakeRepository<TRepo>();
        }

        protected virtual TRepo MakeRepository<TRepo>() where TRepo : class
        {
            // repo factory (delegate), not yet initialized
            Func<HttpClient, IAuthenticationManager, object> repoFactory;

            // try to get factroy for this repo type
            _repositoryFactories.TryGetValue(typeof(TRepo), out repoFactory);
            if (repoFactory == null)
            {
                throw new NotImplementedException("No factory for repository type: " + typeof(TRepo).FullName);
            }

            // there should be user identity built up already at this moment
            if (HttpContext.Current?.Request.IsAuthenticated ?? false)
            {
                var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
                var token = identity?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Authentication)?.Value;
                if (!string.IsNullOrWhiteSpace(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                _logger.Debug("Bearer token added to the httpclient header!");
            }

            // make the repo
            var repo = (TRepo)repoFactory(_httpClient, _authenticationManager);

            // save it to dictionary
            _repositories[typeof(TRepo)] = repo;

            //return it
            return repo;
        }


        public IMultiLangStringRepository MultiLangStrings => GetWebApiRepo<IMultiLangStringRepository>();
        public ITranslationRepository Translations => GetWebApiRepo<ITranslationRepository>();
        public IArticleRepository Articles => GetWebApiRepo<IArticleRepository>();
        public IFriendRepository Friends => GetWebApiRepo<IFriendRepository>();
        public IGameRepository Games => GetWebApiRepo<IGameRepository>();
        public IGameTypeRepository GameTypes => GetWebApiRepo<IGameTypeRepository>();
        public IGameRowTypesRepository GameRowTypes => GetWebApiRepo<IGameRowTypesRepository>();
        public IGameRowRepository GameRows => GetWebApiRepo<IGameRowRepository>();
        public IUserGameRowRepository UserGameRows => GetWebApiRepo<IUserGameRowRepository>();
        public IUserIntRepository UsersInt => GetWebApiRepo<IUserIntRepository>();
        public IUserRoleIntRepository UserRolesInt => GetWebApiRepo<IUserRoleIntRepository>();
        public IRoleIntRepository RolesInt => GetWebApiRepo<IRoleIntRepository>();
        public IUserClaimIntRepository UserClaimsInt => GetWebApiRepo<IUserClaimIntRepository>();
        public IUserLoginIntRepository UserLoginsInt => GetWebApiRepo<IUserLoginIntRepository>();


        /// <summary>
        /// Not used in Web API
        /// </summary>
        public void Commit()
        {

        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

    }
}
