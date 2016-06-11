using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace DAL.Interfaces
{
    public interface IUOW
    {
        //save pending changes to the data store
        void Commit();
        //void RefreshAllEntities();

        //UOW Methods, that dont fit into specific repo

        //get repository for type
        T GetRepository<T>() where T : class;

        //repos
        IArticleRepository Articles { get; }
        IFriendRepository Friends { get; }
        IGameRepository Games { get; }
        IGameTypeRepository GameTypes { get; }
        IGameRowTypesRepository GameRowTypes { get; }
        IGameRowRepository GameRows { get; }
        IUserGameRowRepository UserGameRows { get; }

        // Identity, PK - int
        IUserIntRepository UsersInt { get; }
        IUserRoleIntRepository UserRolesInt { get; }
        IRoleIntRepository RolesInt { get; }
        IUserClaimIntRepository UserClaimsInt { get; }
        IUserLoginIntRepository UserLoginsInt { get; }
    }
}