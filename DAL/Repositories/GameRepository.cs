using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
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
    }
}
