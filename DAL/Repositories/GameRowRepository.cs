using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain.Models;

namespace DAL.Repositories
{
    public class GameRowRepository : EFRepository<GameRow>, IGameRowRepository
    {
        public GameRowRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public void GetAllUniqueRowsForGame(Game game)
        {
            
        }
    }
}
