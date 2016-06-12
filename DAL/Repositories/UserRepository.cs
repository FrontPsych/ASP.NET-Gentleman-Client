using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Repositories;
using Domain.Aggregates;
using Domain.Identity;
using Domain.Models;
using Microsoft.Owin.Security;
using NLog;

namespace DAL.Repositories
{
    public class UserIntRepository : UserRepository<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>,
        IUserIntRepository
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public UserIntRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

        public List<Game> GetGivenTypeGamesUserHasPlayed(int gameTypeId, int userId)
        {
            throw new NotImplementedException();
        }

        public void AddFriend(string username)
        {
            var response = HttpClient.GetAsync(EndPoint + nameof(AddFriend) + "/" + username).Result;

            if (!response.IsSuccessStatusCode)
            {
                _logger.Error(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
                throw new Exception(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            }
        }

        public List<Friend> GetUserFriendRequest()
        {
            var response = HttpClient.GetAsync(EndPoint + nameof(GetUserFriendRequest)).Result;

            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<Friend>>().Result;
                return res;
            }
            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);

            return new List<Friend>();
        }

        public Task<List<UserInt>> SearchUserInts(string searchTxt)
        {
            var response = HttpClient.GetAsync(EndPoint + nameof(SearchUserInts) + "/" + searchTxt).Result;

            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<UserInt>>().Result;
                return Task.FromResult(res);
            }
            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);

            return Task.FromResult<List<UserInt>>(null);
        }


        public List<Friend> GetAllFriends()
        {
            var response = HttpClient.GetAsync(EndPoint + nameof(GetAllFriends)).Result;

            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<Friend>>().Result;
                return res;
            }
            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);

            return new List<Friend>();
        }

        public List<Friend> GetFriends()
        {
            var response = HttpClient.GetAsync(EndPoint + nameof(GetFriends)).Result;

            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<Friend>>().Result;
                return res;
            }
            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);

            return new List<Friend>();
        }

        public List<UserWithRole> GetAllForUser(string filter, string sortProperty, int pageNumber, int pageSize)
        {
            filter = filter ?? "_";
            sortProperty = sortProperty ?? "_";
            var response = HttpClient.GetAsync(EndPoint + nameof(GetAllForUser) + "/" + filter + "/" + sortProperty + "/" + pageNumber + "/" + pageSize).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<UserWithRole>>().Result;
                return res;
            }

            return null;
        }

        public Tuple<List<UserInt>,int, string> GetAllForUser(string filter, DateTime? filterFromDT, DateTime? filterToDt, string sortProperty,
            int pageNumber, int pageSize)
        {
            filter = filter ?? "_";
            sortProperty = sortProperty ?? "lastname";
            var x = Tuple.Create(filterFromDT, filterToDt);

            var response = HttpClient.PostAsJsonAsync(EndPoint + nameof(GetAllForUser) + "/" + filter + "/"+ sortProperty + "/"+ pageNumber + "/" + pageSize, x).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<Tuple<List<UserInt>, int, string>>().Result;
                return res;
            }
            _logger.Debug("Web API statuscode: " + response.StatusCode + " Uri:" + response.RequestMessage.RequestUri);
            return null;
        }

       
    }

    public class UserRepository : UserRepository<string, Role, User, UserClaim, UserLogin, UserRole>, IUserRepository
    {
        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public UserRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

       
    }

    public class UserRepository<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole> : WebApiRepository<TUser>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
    {
        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public UserRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
        public TUser GetUserByUserName(string userName)
        {
            //return DbSet.FirstOrDefault(a => a.UserName.ToUpper() == userName.ToUpper());

            var response = HttpClient.GetAsync(EndPoint + nameof(GetUserByUserName) + "/" + userName + "/").Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<TUser>().Result;
                return res;
            }
            _logger.Debug("Web API statuscode: " + response.StatusCode + " Uri:" + response.RequestMessage.RequestUri);
            return null;
        }

        public TUser GetUserByEmail(string email)
        {
            var response = HttpClient.GetAsync(EndPoint + nameof(GetUserByEmail) + "/" + email + "/").Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<TUser>().Result;
                return res;
            }
            _logger.Debug("Web API statuscode: " + response.StatusCode + " Uri:" + response.RequestMessage.RequestUri);
            return null;
        }

        public bool? CheckIfUsernameExists(string username)
        {
            var response = HttpClient.GetAsync(EndPoint + nameof(CheckIfUsernameExists) + "/" + username).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<bool>().Result;
                return res;
            }
            _logger.Debug("Web API statuscode: " + response.StatusCode + " Uri:" + response.RequestMessage.RequestUri);
            return null;
        }

        public bool IsInRole(TKey userId, string roleName)
        {
            throw new NotImplementedException();
        }

        public void AddUserToRole(TKey userId, string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
