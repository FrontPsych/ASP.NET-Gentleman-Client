using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;
using Domain.Models;
using Microsoft.AspNet.Identity;

namespace DAL.Interfaces
{
    public interface IUserIntRepository : IUserRepository<int, UserInt>
    {
    }

    public interface IUserRepository : IUserRepository<string, User>
    {
    }

    public interface IUserRepository<in TKey, TUser> : IBaseRepository<TUser>
        where TUser : class, IUser<TKey>
    {
        List<Game> GetGivenTypeGamesUserHasPlayed(int gameTypeId, int userId);
        TUser GetUserByUserName(string userName);
        TUser GetUserByEmail(string email);
        bool IsInRole(TKey userId, string roleName);
        void AddUserToRole(TKey userId, string roleName);
        bool? CheckIfUsernameExists(string username);

    }
}