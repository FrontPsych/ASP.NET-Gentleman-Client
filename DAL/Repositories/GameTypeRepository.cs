using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain.Models;
using Microsoft.Owin.Security;

namespace DAL.Repositories
{
    public class GameTypeRepository : WebApiRepository<GameType>, IGameTypeRepository
    {
        public GameTypeRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
    }
}
