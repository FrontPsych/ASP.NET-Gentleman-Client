using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace DAL.Interfaces
{
    public interface IGameRowTypesRepository : IBaseRepository<GameRowType>
    {
        List<GameRowType> GetRowTypesByGameType(GameType gt);
    }
}
