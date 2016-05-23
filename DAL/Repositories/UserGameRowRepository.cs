using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain.Identity;
using Domain.Models;

namespace DAL.Repositories
{
    public class UserGameRowRepository : EFRepository<UserGameRow>, IUserGameRowRepository
    {
        public UserGameRowRepository(IDbContext dbContext) : base(dbContext)
        {

        }

        public int GetUserScore(int userId, int gameId)
        {
            var allUserRows = DbSet.Where(x => x.GameRow.GameId == gameId && x.UserIntId == userId).ToList();

            var sum = 0;

            foreach (var row in allUserRows)
            {
                if (row.EndBet == null) continue;

                if (row.StartBet == row.EndBet)
                {
                    sum = row.StartBet == 0 ? sum + 5 : sum + (row.StartBet * 10);
                }
                else
                {
                    sum += row.EndBet.Value;
                }

            }

            return sum;
        }

       

        //Sobilik koht?
        public List<UserGameRow> MapUserGameRows (List<GameRow> gameRows, UserInt userInt)
        {
            return gameRows.Select(x => new UserGameRow() {GameRow = x, UserInt = userInt, GameRowId = x.GameRowId, UserIntId = userInt.Id}).ToList();
        } 
    }
}
