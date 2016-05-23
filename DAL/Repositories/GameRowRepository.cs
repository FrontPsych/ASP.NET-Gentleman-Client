using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using DAL.Interfaces;
using Domain.Identity;
using Domain.Models;

namespace DAL.Repositories
{
    public class GameRowRepository : EFRepository<GameRow>, IGameRowRepository
    {
        public GameRowRepository(IDbContext dbContext) : base(dbContext)
        {
        }



        public int GetNumberOfPlayersInGame(Game game)
        {

            var firstOrDefault = DbSet.Include(x => x.GameId).FirstOrDefault(x => x.GameId == game.GameId);
            if (firstOrDefault != null)
            {
                var usersInSpecificRow = firstOrDefault.UserGameRows.Count;
                return usersInSpecificRow;
            }
            return 0;
        }

        public int GetNumberOfPlayersInGame(int gameId)
        {
            var firstOrDefaultGameRow = DbSet.Where(x => x.GameId == gameId).FirstOrDefault();

            if (firstOrDefaultGameRow != null)
            {
                var usersInSpecificRow = firstOrDefaultGameRow.UserGameRows.Count;
                return usersInSpecificRow;
            }
            return 0;
        }

        public List<UserInt> GetAllPlayersInGame(int gameId)
        {
            var firstOrDefaultGameRow = DbSet.FirstOrDefault(x => x.GameId == gameId);

            if (firstOrDefaultGameRow != null)
            {
                var users = firstOrDefaultGameRow.UserGameRows.Select(x => x.UserInt).ToList();
                return users;
            }

            return new List<UserInt>();
        }


    }
}
