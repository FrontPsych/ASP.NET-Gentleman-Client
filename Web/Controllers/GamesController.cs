using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI;
using DAL;
using DAL.Interfaces;
using Domain.Identity;
using Domain.Models;
using Microsoft.AspNet.Identity;
using Web.ViewModels;

namespace Web.Controllers
{
    [System.Web.Mvc.Authorize]
    public class GamesController : BaseController
    {
        private readonly IUOW _uow;

        public GamesController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: Games
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId<int>();
            return View();
        }

        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = this._uow.Games.GetById(id);
            if (game == null)
            {
                return HttpNotFound();
            }

            var vm = new GameDetailsViewModel()
            {
                Game = game
            };

            return View(vm);
        }

        // GET: Games/Create
        public ActionResult Create()
        {

            var vm = new GameCreateViewModel()
            {
                //UserMultiSelectList = new MultiSelectList(this._uow.UsersInt.All, nameof(UserInt.Id), nameof(UserInt.PersonName)),
                GameTypeSelectList = new SelectList(this._uow.GameTypes.All, nameof(GameType.GameTypeId), nameof(GameType.Name))
            };

            return View(vm);
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GameCreateViewModel vm)
        {

            if (ModelState.IsValid)
            {
                if (vm.GameTime)
                {

                    //vm.Game.StoppedAt = DateTime.Now;
                    //this._uow.Games.Update(vm.Game);
                    //return RedirectToAction("Index", "Admin"); //Redirect to current game details?
                    View(vm);
                }
                
                vm.Game.UserIntId = User.Identity.GetUserId<int>();
                vm.Game.StartedAt = DateTime.Now;

                vm.Game = this._uow.Games.AddGameWithReturn(vm.Game);

                vm.GameRowTypesForGivenGame = this._uow.GameRowTypes.GetRowTypesByGameType(vm.Game.GameTypeId);

                vm.Game.GameRows = new List<GameRow>();

                var firstGameRow = new GameRow()
                {
                    GameId = vm.Game.GameId,
                    GameRowTypeId = vm.GameRowTypesForGivenGame.FirstOrDefault().GameRowTypeId,
                    GameRowType = this._uow.GameRowTypes.GetById(vm.GameRowTypesForGivenGame.FirstOrDefault().GameRowTypeId)
                };
                
                vm.Game.GameRows.Add(firstGameRow);
                //vm.Game.GameRows = this._uow.GameRowTypes.GetRowTypesByGameType(this._uow.GameTypes.GetById(vm.Game.GameTypeId)).Select(x => new GameRow(vm.Game, x)).ToList();
                vm.GameTime = true;
            }

            //ToDo: hakata lisama siin ridu ja m'ngijaid. atm lisataks üks UserGameRow, mille GameRow on alustatud mängu esimene rida ja mängija on mängu looja. HowDo??
            //vm.UserGameRows = this._uow.UserGameRows.MapUserGameRows(vm.Game.GameRows.ToList(), vm.Game.UserIntId);
            //vm.UserGameRows = new List<UserGameRow>();

            vm.Game.GameRows.FirstOrDefault()?.UserGameRows.Add(new UserGameRow()
            {
                GameRow = vm.Game.GameRows.FirstOrDefault(),
                UserInt = this._uow.UsersInt.GetById(vm.Game.UserIntId),
                UserIntId = vm.Game.UserIntId
            });

            vm.GameTypeSelectList = new SelectList(this._uow.GameTypes.All, nameof(GameType.GameTypeId), nameof(GameType.Name));

            return View(vm);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [System.Web.Mvc.HttpGet]
        public ActionResult AddUserGameRow(int userid)
        {

            var row = new GameRow();
            row.UserGameRows.Add(new UserGameRow()
            {
                UserIntId = userid,
            });

            return View(row);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult AddUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return Json(new string[0]);
            }

            var user = this._uow.UsersInt.GetUserByUserName(username);

            if (user == null)
            {
                this._uow.UsersInt.Add(new UserInt()
                {
                    UserName = username
                });
                this._uow.Commit();

                user = this._uow.UsersInt.GetUserByUserName(username);
            }
            return Json(new { id = user.Id});
        }

        //[HttpGet]
        //public ActionResult AddRow(int gameRowTypeId)
        //{

        //    var game = new Game();
        //    var row = new GameRow();
        //    row.UserGameRows.Add(new UserGameRow());
        //    game.GameRows.Add(row);

        //    return View(game);
        //}

        [System.Web.Mvc.HttpPost]
        public ActionResult AddUpdateGameRow([FromBody] GameAddRowViewModel vm)
        {

            //ToDo: Check input

            vm.GameRow.UserGameRows = vm.UserGameRows;

            if (vm.GameRow.GameRowId != 0) 
                this._uow.GameRows.Update(vm.GameRow);

            var gameRow = this._uow.GameRows.AddGameRowWithReturn(vm.GameRow);


            return Json(new { GameRowId = gameRow.GameRowId });
        }


        //// GET: Games/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Game game = this._uow.Games.GetById(id);
        //    if (game == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.GameTypeId = new SelectList(db.GameTypes, "GameTypeId", "Name", game.GameTypeId);
        //    ViewBag.UserIntId = new SelectList(db.UsersInt, "Id", "PersonName", game.UserIntId);
        //    return View(game);
        //}

        //// POST: Games/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "GameId,GameName,StartedAt,StoppedAt,GameTypeId,UserIntId,CreatedAtDT,CreatedBy,ModifiedAtDT,ModifiedBy")] Game game)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(game).State = EntityState.Modified;
        //        this._uow.Commit();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.GameTypeId = new SelectList(db.GameTypes, "GameTypeId", "Name", game.GameTypeId);
        //    ViewBag.UserIntId = new SelectList(db.UsersInt, "Id", "PersonName", game.UserIntId);
        //    return View(game);
        //}



        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = this._uow.Games.GetById(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = this._uow.Games.GetById(id);
            this._uow.Games.Delete(game);
            this._uow.Commit();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }
    }
}
