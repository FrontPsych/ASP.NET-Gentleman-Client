using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Interfaces;
using Domain.Models;
using Microsoft.AspNet.Identity;
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


        public ActionResult Friends()
        {
            var vm = new AllFriendsUserVariaViewModel()
            {
                AllFriends = this._uow.UsersInt.GetAllFriends()
            };
            return View(vm);
        }

        public ActionResult Index()
        {
            var vm = new StatisticsUserVariaViewModel()
            {
                AllGameTypeSelectList = new SelectList(this._uow.GameTypes.All, nameof(GameType.GameTypeId), nameof(GameType.Name))
            };

            return View("Statistics", vm);
        }

        public ActionResult GetStatistics(string gameTypeId)
        {
            if (string.IsNullOrWhiteSpace(gameTypeId))
            {
                return Json(new string[0]);
            }

            var gTypeId = int.Parse(gameTypeId);
            var userId = int.Parse(User.Identity.GetUserId());

            var stats = this._uow.Games.GetAllGameStatisticsForGameType(gTypeId, userId);

            return Json(stats.Select(x => new { GameName = x.Game.GameName, NumberOfPlayers = x.NumberOfPlayers, YourScore = x.Score, YourPosition = x.Position, Duration = x.DurationTimeSpan.Duration().ToString(@"mm\:ss") }).ToArray());
        }
    }
}