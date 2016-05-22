using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain.Models;

namespace DAL.Repositories
{
    public class UserGameRowRepository : EFRepository<UserGameRow>, IUserGameRowRepository
    {
        public UserGameRowRepository(IDbContext dbContext) : base(dbContext)
        {

        }

        public List<UserGameRow> MapUserGameRows (List<GameRow> gameRows)
        {
            return gameRows.Select(x => new UserGameRow() {GameRow = x}).ToList();
        } 
    }
}
