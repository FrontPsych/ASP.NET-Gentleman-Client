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
    public class GameRowTypesRepository : WebApiRepository<GameRowType>, IGameRowTypesRepository
    {
        public GameRowTypesRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

        public List<GameRowType> GetRowTypesByGameType(GameType gt)
        {
            throw new NotImplementedException();
        }
    }
}
