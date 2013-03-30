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
        [HttpPost]
        public ActionResult Google(SearchEngineInfo model)
        {
            searchEngineInfo = SiteHelper.SeoModel(model.SiteUrl, EnumSearchEngine.Google);
            return View(searchEngineInfo);
        }
        public ActionResult Yahoo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Yahoo(SearchEngineInfo model)
        {
            searchEngineInfo = SiteHelper.SeoModel(model.SiteUrl, EnumSearchEngine.Yahoo);
            return View(searchEngineInfo);
        }
        public ActionResult Sogou()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Sogou(SearchEngineInfo model)
        {
            searchEngineInfo = SiteHelper.SeoModel(model.SiteUrl, EnumSearchEngine.Sogou);
            return View(searchEngineInfo);
        }
        public ActionResult Soso()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Soso(SearchEngineInfo model)
        {
            searchEngineInfo = SiteHelper.SeoModel(model.SiteUrl, EnumSearchEngine.Soso);
            return View(searchEngineInfo);
        }
        public ActionResult Bing()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Bing(SearchEngineInfo model)
        {
            searchEngineInfo = SiteHelper.SeoModel(model.SiteUrl, EnumSearchEngine.Bing);
            return View(searchEngineInfo);
        }
        public ActionResult Youdao()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Youdao(SearchEngineInfo model)
        {
            searchEngineInfo = SiteHelper.SeoModel(model.SiteUrl, EnumSearchEngine.Yahoo);
            return View(searchEngineInfo);
        }

    }
}
