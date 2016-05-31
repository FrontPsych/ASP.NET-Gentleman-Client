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

namespace DAL.Repositories
{
    public class UserGameRowRepository : WebApiRepository<UserGameRow>, IUserGameRowRepository
    {
        public UserGameRowRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

        public int GetUserScore(int userId, int gameId)
        {
            throw new NotImplementedException();
        }

        public List<UserGameRow> MapUserGameRows(List<GameRow> gameRows, UserInt userInt)
        {
            throw new NotImplementedException();
        }
    }
}
