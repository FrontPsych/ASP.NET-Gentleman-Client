using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Identity;

namespace DAL.Interfaces
{
    public interface IGameRowRepository : IBaseRepository<GameRow>
    {
        int GetNumberOfPlayersInGame(Game game);
        int GetNumberOfPlayersInGame(int gameId);
        List<UserInt> GetAllPlayersInGame(int gameId);
    }
}
