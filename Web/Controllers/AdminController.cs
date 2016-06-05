using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Interfaces;
using Domain.Identity;
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

            };

            return View(vm);
        }

        public ActionResult About()
        {
            var vm = new AdminAboutViewModel()
            {
                AboutArticle = this._uow.Articles.FindArticleByName("AboutIndex")
            };

            return View(vm);
        }

        public ActionResult UserList(AdminUserListViewModel vm)
        {

            if (vm == null) vm = new AdminUserListViewModel(){};
            vm.PageNumber = vm.PageNumber ?? 1;
            vm.PageSize = vm.PageSize ?? 1;
            vm.SortProperty = "firstname";
            //Tuple<List<UserInt>, int, string> res = new Tuple<List<UserInt>, int, string>(new List<UserInt>(),2,"TereMari");
            //if (vm.FilterByDTBoolean)
            //{
            //    res = _uow.UsersInt.GetAllForUser(User.Identity.GetUserId<int>(), vm.Filter, vm.FilterFromDT,
            //        vm.FilterToDT, vm.SortProperty, vm.PageNumber.Value - 1, vm.PageSize.Value);
            //}
            //else
            //{
            //    res = _uow.UsersInt.GetAllForUser(User.Identity.GetUserId<int>(), vm.Filter, vm.SortProperty,
            //        vm.PageNumber.Value - 1, vm.PageSize.Value);

            //    vm.FilterFromDT = vm.FilterFromDT ?? DateTime.Now.Subtract(TimeSpan.FromDays(5 * 365));
            //    vm.FilterToDT = vm.FilterToDT ?? DateTime.Now;
            //}
            List<UserInt> res = _uow.UsersInt.GetAllForUser(User.Identity.GetUserId<int>(), vm.Filter, vm.SortProperty,
                vm.PageNumber.Value - 1, vm.PageSize.Value);

            //vm.FilterFromDT = vm.FilterFromDT ?? DateTime.Now.Subtract(TimeSpan.FromDays(5 * 365));
            //vm.FilterToDT = vm.FilterToDT ?? DateTime.Now;


            
            vm.Users = new StaticPagedList<UserInt>(res, vm.PageNumber.Value, vm.PageSize.Value, _uow.UsersInt.All.Count);

            var x = vm;
            return View(vm);
        }
    }
}