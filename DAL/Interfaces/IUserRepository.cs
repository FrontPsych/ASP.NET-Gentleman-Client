using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregates;
using Domain.Identity;
using Domain.Models;
using Microsoft.AspNet.Identity;

namespace DAL.Interfaces
{
    public interface IUserIntRepository : IUserRepository<int, UserInt>
    {
        void AddFriend(string username);
        Task<List<UserInt>> SearchUserInts(string searchTxt);
        List<Friend> GetAllFriends();
        List<Friend> GetFriends();
        List<UserWithRole> GetAllForUser(string filter, string sortProperty, int pageNumber, int pageSize);
        Tuple<List<UserInt>,int, string> GetAllForUser(string filter, DateTime? filterFromDT, DateTime? filterToDt, string sortProperty, int pageNumber, int pageSize);
        List<Friend> GetUserFriendRequest();
    }

    public interface IUserRepository : IUserRepository<string, User>
    {
    }

    public interface IUserRepository<in TKey, TUser> : IBaseRepository<TUser>
        where TUser : class, IUser<TKey>
    {
        TUser GetUserByUserName(string userName);
        TUser GetUserByEmail(string email);
        bool IsInRole(TKey userId, string roleName);
        void AddUserToRole(TKey userId, string roleName);
        bool? CheckIfUsernameExists(string username);

    }
}