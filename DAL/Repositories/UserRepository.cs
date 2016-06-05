using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Repositories;
using Domain.Identity;
using Domain.Models;
using Microsoft.Owin.Security;
using NLog;

namespace DAL.Repositories
{
    public class UserIntRepository : UserRepository<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>,
        IUserIntRepository
    {
        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public UserIntRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

        public List<Game> GetGivenTypeGamesUserHasPlayed(int gameTypeId, int userId)
        {
            throw new NotImplementedException();
        }

        public List<UserInt> GetAllForUser(int userId, string filter, string sortProperty, int pageNumber, int pageSize)
        {
            filter = filter ?? "_";
            sortProperty = sortProperty ?? "_";
            var response = HttpClient.GetAsync(EndPoint + nameof(GetAllForUser) + "/" + userId + "/" + filter + "/" + sortProperty + "/" + pageNumber + "/" + pageSize).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<UserInt>>().Result;
                return res;
            }

            return null;
        }

        //public List<UserInt> GetUsersInt()
        //{
        //    var response = HttpClient.GetAsync(EndPoint).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var res = response.Content.ReadAsAsync<List<UserInt>>().Result;
        //        return res;
        //    }
        //    _logger.Debug("Web API statuscode: " + response.StatusCode + " Uri:" + response.RequestMessage.RequestUri);

        //    return null;
        //} 
        public Tuple<List<UserInt>,int, string> GetAllForUser(int userId, string filter, DateTime? filterFromDT, DateTime? filterToDt, string sortProperty,
            int pageNumber, int pageSize)
        {
            filter = filter ?? "_";
            sortProperty = sortProperty ?? "lastname";
            var x = Tuple.Create(filterFromDT, filterToDt);

            var response = HttpClient.PostAsJsonAsync(EndPoint + nameof(GetAllForUser) + "/" + userId + "/" + filter + "/"+ sortProperty + "/"+ pageNumber + "/" + pageSize, x).Result;
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
