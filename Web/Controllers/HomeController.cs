using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUOW _uow;

        public HomeController(IUOW uow)
        {
            _uow = uow;
        }

        public ActionResult Index()
        {
            var vm = new HomeSinglePageViewModel()
            {
                Article = _uow.Articles.FindArticleByName("HomeIndex"),
                AboutArticle = _uow.Articles.FindArticleByName("HomeIndexAbout"),
                AboutArticleColumnOne = _uow.Articles.FindArticleByName("HomeIndexAboutColumnOne")
            };

            return View(vm);
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}