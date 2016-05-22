using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
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
    [Authorize]
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
                GameTypeSelectList = new SelectList(this._uow.GameTypes.All, "GameTypeId", "Name")
            };

            return View(vm);
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GameCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Game.UserInt = this._uow.UsersInt.GetById(int.Parse(User.Identity.GetUserId()));
                vm.Game = this._uow.Games.Add(vm.Game);
                this._uow.Commit();

                vm.GameTime = true;
            }

            vm.GameRows = this._uow.GameRowTypes.GetRowTypesByGameType(this._uow.GameTypes.GetById(vm.Game.GameTypeId)).Select(x => new GameRow(vm.Game, x)).ToList();
            vm.GameTypeSelectList = new SelectList(this._uow.GameTypes.All, "GameTypeId", "Name");

            return View(vm);
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
        [HttpPost, ActionName("Delete")]
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
