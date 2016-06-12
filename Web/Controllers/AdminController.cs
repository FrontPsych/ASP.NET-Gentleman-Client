using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DAL.Interfaces;
using Domain.Aggregates;
using Domain.Identity;
using Domain.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using PagedList;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {

        private readonly IUOW _uow;

        public AdminController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: Admin
        public ActionResult Index()
        {
            var vm = new AdminIndexViewModel()
            {
                Friends = this._uow.UsersInt.GetFriends(),
                FriendRequests = this._uow.UsersInt.GetUserFriendRequest(),
                Games = this._uow.Games.GetLatestPlayedGames()
            };

            return View(vm);
        }

        // POST: Admin
        [HttpPost]
        public ActionResult Index(AdminIndexViewModel vm)
        {
            if (!vm.SearchTxt.IsNullOrWhiteSpace())
            {
                this._uow.UsersInt.AddFriend(vm.SearchTxt);
                return RedirectToAction("Index", "Admin");
            }

            vm.Friends = this._uow.UsersInt.GetFriends();
            vm.Games = this._uow.Games.GetLatestPlayedGames();
            return View(vm);
        }


        [AllowAnonymous]
        public async Task<JsonResult> SearchUsersAsync(string searchTxt)
        {
            if (searchTxt.IsNullOrWhiteSpace())
            {
                return Json(new string[0], JsonRequestBehavior.AllowGet);
            }

            var result = await this._uow.UsersInt.SearchUserInts(searchTxt);

            return Json(result.ToArray(), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult FriendRequestAction(bool accept, int friendId)
        {
            if (!accept) { 
                this._uow.Friends.Delete(friendId);
            }
            else { 
                this._uow.Friends.UpdateFriendStatus(accept, friendId);
            }

            return RedirectToAction("Index", "Admin");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult SystemLogs(string logFileName)
        {
            var logPath = Server.MapPath("~") + @"\logs\";

            var fileEntries =
                Directory.GetFiles(logPath)
                    .ToList()
                    .Select(p => new { FileNameId = Path.GetFileName(p), FileName = Path.GetFileName(p) })
                    .OrderByDescending(a => a.FileName)
                    .ToList();
            var e = fileEntries.FirstOrDefault();
            var vm = new SystemLogIndexViewModel();
            vm.LogFileSelectList = logFileName == null
                ? new SelectList(fileEntries, nameof(e.FileNameId), nameof(e.FileName))
                : new SelectList(fileEntries, nameof(e.FileNameId), nameof(e.FileName), logFileName);

            logFileName = logPath + logFileName?.Trim();

            if (System.IO.File.Exists(logFileName))
            {
                vm.LogFileContent = System.IO.File.ReadAllText(logFileName);
            }

            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAllLogFiles()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("DeleteAllLogFiles")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAllLogFilesConfirmed()
        {

            var logPath = Server.MapPath("~") + @"\logs\";

            var fileEntries =
                Directory.GetFiles(logPath);

            foreach (var fileEntry in fileEntries)
            {
                System.IO.File.Delete(fileEntry);
            }

            return Redirect(nameof(Index));
        }

        public ActionResult About()
        {
            var vm = new AdminAboutViewModel()
            {
                AboutArticle = this._uow.Articles.FindArticleByName("AboutIndex")
            };

            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UserList(AdminUserListViewModel vm)
        {

            if (vm == null) vm = new AdminUserListViewModel(){};
            vm.PageNumber = vm.PageNumber ?? 1;
            vm.PageSize = vm.PageSize ?? 5;
            vm.SortProperty = vm.SortProperty;
            
            
            List<UserWithRole> res = _uow.UsersInt.GetAllForUser(User.Identity.GetUserId<int>(), vm.Filter, vm.SortProperty,
                vm.PageNumber.Value - 1, vm.PageSize.Value);

            //foreach (var userInt in res)
            //{

            //    var listOfRoles = new List<UserRoleInt>();
            //    foreach (var role in userInt.Roles)
            //    {
            //        var role2 = _uow.RolesInt.GetById(role.RoleId);

            //    }
            //}



            vm.Users = new StaticPagedList<UserWithRole>(res, vm.PageNumber.Value, vm.PageSize.Value, _uow.UsersInt.All.Count);
            //vm.Users.Where(a => a.UserName == "lebo@lebo.ee").FirstOrDefault();
            var x = vm;
            return View(vm);
        }


    }
}