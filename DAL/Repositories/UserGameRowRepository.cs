using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain.Identity;
using Domain.Models;
using Microsoft.Owin.Security;
using NLog;

namespace DAL.Repositories
{
    public class UserGameRowRepository : WebApiRepository<UserGameRow>, IUserGameRowRepository
    {
        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public UserGameRowRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

        public int GetUserScore(int userId, int gameId)
        {
            throw new NotImplementedException();
        }

        public List<UserGameRow> MapUserGameRows(List<GameRow> gameRows, int userIntId)
        {
            var response = HttpClient.PostAsJsonAsync(EndPoint + "/" + nameof(MapUserGameRows) + "/" + userIntId, gameRows).Result;

            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<UserGameRow>>().Result;
                return res;
            }
            this._logger.Debug("Web API statuscode: " + response.StatusCode + " Uri:" + response.RequestMessage.RequestUri);

            return new List<UserGameRow>();
        }
    }
}
