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
using NLog;

namespace DAL.Repositories
{
    public class GameRowRepository : WebApiRepository<GameRow>, IGameRowRepository
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

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

        public GameRow AddGameRowWithReturn(GameRow gameRow)
        {
            var response = HttpClient.PostAsJsonAsync(EndPoint + nameof(AddGameRowWithReturn), gameRow).Result;

            if (!response.IsSuccessStatusCode)
            {
                _logger.Error(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
                throw new Exception(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            }

            return response.Content.ReadAsAsync<GameRow>().Result;
        }
    }
}
