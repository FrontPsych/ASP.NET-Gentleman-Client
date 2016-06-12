using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain.Aggregates;
using Domain.Models;
using Microsoft.Owin.Security;
using NLog;

namespace DAL.Repositories
{
    public class GameRepository : WebApiRepository<Game>, IGameRepository
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public GameRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

        public List<GameStatistic> GetLatestPlayedGames()
        {
            var response = HttpClient.GetAsync(EndPoint + nameof(GetLatestPlayedGames)).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<GameStatistic>>().Result;
                return res;
            }
            _logger.Debug("Web API statuscode: " + response.StatusCode + " Uri:" + response.RequestMessage.RequestUri);
            return new List<GameStatistic>();
        }

        public List<GameStatistic> GetAllGameStatisticsForGameType(int gameTypeId, int userId)
        {
            var response = HttpClient.GetAsync(EndPoint + nameof(GetAllGameStatisticsForGameType) + "/" + gameTypeId + "/" + "/" + userId).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<GameStatistic>>().Result;
                return res;
            }
            _logger.Debug("Web API statuscode: " + response.StatusCode + " Uri:" + response.RequestMessage.RequestUri);
            return new List<GameStatistic>();
        }


        public Game AddGameWithReturn(Game game)
        {
            var response = HttpClient.PostAsJsonAsync(EndPoint + nameof(AddGameWithReturn), game).Result;

            if (!response.IsSuccessStatusCode)
            {
                _logger.Error(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
                throw new Exception(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            }

            return response.Content.ReadAsAsync<Game>().Result;
        }

        public List<GameResult> GetGameResults(int gameId)
        {
            var response = HttpClient.GetAsync(EndPoint + nameof(GetGameResults) + "/" + gameId).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<GameResult>>().Result;
                return res;
            }
            _logger.Debug("Web API statuscode: " + response.StatusCode + " Uri:" + response.RequestMessage.RequestUri);
            return null;
        }
    }
}
