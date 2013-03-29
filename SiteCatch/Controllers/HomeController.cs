using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace SiteCatch.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        SearchEngineInfo searchEngineInfo = new SearchEngineInfo();
        public ActionResult Index()
        {            
            return View();
        }
        public ActionResult Baidu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Baidu(SearchEngineInfo model)
        {
            searchEngineInfo = SiteHelper.SeoModel(model.SiteUrl, EnumSearchEngine.Baidu);
            return View(searchEngineInfo);
        }
        public ActionResult Google()
        {
            return View();
        }
        public ActionResult Yahoo()
        {

            return View();
        }
        public ActionResult Sogou()
        {

            return View();
        }
        public ActionResult Soso()
        {

            return View();
        }
        public ActionResult Bing()
        {

            return View();
        }
        public ActionResult Youdao()
        {

            return View();
        }

    }
}
