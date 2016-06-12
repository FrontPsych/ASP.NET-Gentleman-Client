using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Interfaces;
using Domain.Models;
using Microsoft.AspNet.Identity;
using NLog.Web.LayoutRenderers;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class UserVariaController : BaseController
    {
        private readonly IUOW _uow;

        public UserVariaController(IUOW uow)
        {
            _uow = uow;
        }


        public ActionResult Index(int userId = 0)
        {

            var vm = new StatisticsUserVariaViewModel();


            if (userId == 0)
            {
                vm = new StatisticsUserVariaViewModel()
                {
                    AllGameTypeSelectList =
                        new SelectList(this._uow.GameTypes.All, nameof(GameType.GameTypeId), nameof(GameType.Name))
                };
            }
            else
            {
                vm = new StatisticsUserVariaViewModel()
                {
                    AllGameTypeSelectList =
                        new SelectList(this._uow.GameTypes.All, nameof(GameType.GameTypeId), nameof(GameType.Name)),
                    FriendUser = this._uow.UsersInt.GetById(userId)
                };
            }
            

            return View("Statistics", vm);
        }



        public ActionResult GetStatistics(string gameTypeId, int userId = 0)
        {
            if (string.IsNullOrWhiteSpace(gameTypeId))
            {
                return Json(new string[0]);
            }

            if (userId == 0)
            {
                userId = User.Identity.GetUserId<int>();
            }

            var gTypeId = int.Parse(gameTypeId);

            var stats = this._uow.Games.GetAllGameStatisticsForGameType(gTypeId, userId);

            return Json(stats.Select(x => new { GameName = x.Game.GameName, NumberOfPlayers = x.NumberOfPlayers, YourScore = x.Score, YourPosition = x.Position, Duration = x.DurationTimeSpan.Duration().ToString(@"mm\:ss") }).ToArray());


        }
    }
}