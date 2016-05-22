using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain.Models;

namespace DAL.Repositories
{
    public class GameRepository : EFRepository<Game>, IGameRepository
    {
        public GameRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
