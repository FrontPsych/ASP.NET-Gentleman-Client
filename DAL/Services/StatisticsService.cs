using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain.Aggregates;
using Domain.Identity;
using Domain.Models;

namespace DAL.Services
{
    public class StatisticsService
    {
        private readonly IUOW _uow;

        public StatisticsService(IUOW uow)
        {
            this._uow = uow;
        }

        public List<GameStatistic> GetAllGameStatisticsForGameType(int gameTypeId, int userId)
        {
            var allUserGamesForGivenType = this._uow.UsersInt.GetGivenTypeGamesUserHasPlayed(gameTypeId, userId);
            var result = new List<GameStatistic>();

            foreach (var game in allUserGamesForGivenType)
            {
                result.Add(new GameStatistic()
                {
                    Game = game,
                    NumberOfPlayers = this._uow.GameRows.GetNumberOfPlayersInGame(game.GameId),
                    Position = CalculatePosition(game.GameId, userId),
                    Score = this._uow.UserGameRows.GetUserScore(userId, game.GameId),
                    DurationTimeSpan = game.StoppedAt == null ? TimeSpan.Zero : game.StartedAt.Subtract(game.StoppedAt.Value)
                });
            }


            return result;
        }

        private int CalculatePosition(int gameId, int userId)
        {
            var allPlayersInGame = this._uow.GameRows.GetAllPlayersInGame(gameId);

            var userScoreList = new List<Tuple<UserInt, int>>();

            foreach (var player in allPlayersInGame)
            {
                var playerScore = this._uow.UserGameRows.GetUserScore(userId, gameId);
                userScoreList.Add(new Tuple<UserInt, int>(player, playerScore));
            }

            userScoreList = userScoreList.OrderByDescending(x => x.Item2).ToList();

            var position = 1;
            foreach (var tuple in userScoreList)
            {
                Console.WriteLine("{0}. {1}({2}p)", position, tuple.Item1.PersonName, tuple.Item2 );
                if (tuple.Item1.Id == userId)
                {
                    return position;
                }
            }


            return 0;
        }

    }
}
