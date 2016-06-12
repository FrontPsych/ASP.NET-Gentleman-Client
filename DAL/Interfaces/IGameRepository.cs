using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregates;
using Domain.Models;

namespace DAL.Interfaces
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        List<GameStatistic> GetLatestPlayedGames();
        List<GameStatistic> GetAllGameStatisticsForGameType(int gameTypeId, int userId);
        Game AddGameWithReturn(Game game);
        List<GameResult> GetGameResults(int gameId);
    }
}
