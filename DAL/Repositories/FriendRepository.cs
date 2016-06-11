using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.Interfaces;
using Domain.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using NLog;

namespace DAL.Repositories
{
    public class FriendRepository : WebApiRepository<Friend>, IFriendRepository
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public FriendRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }


        public void UpdateFriendStatus(bool accept, int friendId)
        {
            var response = HttpClient.PostAsJsonAsync(EndPoint + nameof(UpdateFriendStatus) + "/" + friendId, accept).Result;

            if (!response.IsSuccessStatusCode)
            {
                _logger.Error(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
                throw new Exception(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            }
        }
    }
}
