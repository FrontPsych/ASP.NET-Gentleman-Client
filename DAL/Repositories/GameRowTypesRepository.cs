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
    public class GameRowTypesRepository : WebApiRepository<GameRowType>, IGameRowTypesRepository
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public GameRowTypesRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

        public List<GameRowType> GetRowTypesByGameType(int gameTypeId)
        {
            var response = HttpClient.GetAsync(EndPoint + nameof(GetRowTypesByGameType) + "/" + gameTypeId).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<GameRowType>>().Result;
                return res;
            }
            this._logger.Debug("Web API statuscode: " + response.StatusCode + " Uri:" + response.RequestMessage.RequestUri);

            return new List<GameRowType>();
        }
    }
}
