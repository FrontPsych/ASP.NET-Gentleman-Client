using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Http;
using DAL.Interfaces;
using Domain.Identity;
using Domain.Models;
using Microsoft.Owin.Security;

namespace DAL.Repositories
{
    public class GameRowRepository : WebApiRepository<GameRow>, IGameRowRepository
    {
        public GameRowRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

        public int GetNumberOfPlayersInGame(Game game)
        {
            throw new NotImplementedException();
        }

        public int GetNumberOfPlayersInGame(int gameId)
        {
            throw new NotImplementedException();
        }

        public List<UserInt> GetAllPlayersInGame(int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
