using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain.Models;

namespace DAL.Repositories
{
    public class GameRowTypesRepository : EFRepository<GameRowType>, IGameRowTypesRepository
    { 
        public GameRowTypesRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public List<GameRowType> GetRowTypesByGameType(GameType gt)
        {
            if(gt == null) throw new NullReferenceException("GameType is null.");
            return DbSet.Where(x => x.GameTypeId == gt.GameTypeId).OrderBy(x => x.SortOrder).ToList();
        }
    }
}
