using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;
using Domain.Models;

namespace DAL.Interfaces
{
    public interface IUserGameRowRepository : IEFRepository<UserGameRow>
    {
        int GetUserScore(int userId, int gameId);
        List<UserGameRow> MapUserGameRows(List<GameRow> gameRows, UserInt userInt);
    }
}
