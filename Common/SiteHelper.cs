using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Common
{
    public class SiteHelper
    {
        /// <summary>
        ///  站点收录信息
        /// </summary>
        public static string GetRecordInfo(EnumSearchEngine _engine, string Url)
        {
            CutRegexInfo model = SeoHelper.GetCutRegexInfo(_engine, Url, true);
            string Html = HtmlCatch.GetHtml(model.siteUrl, model.encoding);
            string Result = SeoHelper.GetMetaString(Html, model.regStart, model.regEnd, true);
            return HtmlCatch.NoHTML(Result);
        }
        /// <summary>
        ///  反向链接信息
        /// </summary>
        public static string GetBackLinkInfo(EnumSearchEngine _engine, string Url)
        {
            CutRegexInfo model = SeoHelper.GetCutRegexInfo(_engine, Url, false);
            string Html = HtmlCatch.GetHtml(model.siteUrl, model.encoding);
            string Result = SeoHelper.GetMetaString(Html, model.regStart, model.regEnd, true);
            return HtmlCatch.NoHTML(Result);
        }
        /// <summary>
        /// 获取Title,Keywords,Description
        /// </summary>
        public static string[] GetMeta(string Url)
        {
            string Html = HtmlCatch.GetHtml("http://"+Url, "utf-8");
            return SeoHelper.GetMeta(Html);
        }
        /// <summary>
        ///  获取某站点关键字在某搜索引擎的排名
        /// </summary>
        public static string GetKeyWordInfo(EnumSearchEngine _engine, string Url, string KeyWord)
        {
            System.Text.RegularExpressions.MatchCollection mc;
            System.Text.RegularExpressions.MatchCollection mcOther;
            string Html = "";
            string resultText = "";
            string searchUrl = "";
            Regex reg;
            Regex regOther;
            switch (_engine)
            {
                case EnumSearchEngine.Baidu:
                    searchUrl = "http://www.baidu.com/s?tn=baiduadv&rn=100&q1=";
                    Html = HtmlCatch.GetHtml(searchUrl + HttpUtility.UrlEncode(KeyWord, System.Text.Encoding.GetEncoding("gb2312")), "gb2312");
                    // Url = Url.Replace("\\", "");
                    resultText = SeoHelper.GetMetaString(Html, "<div id=\"wrapper\">", "<font color=\"#008000\">" + Url + "", true);
                    reg = new Regex("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"");
                    regOther = new Regex("<table cellpadding=\"0\" cellspacing=\"0\"");
                    mc = reg.Matches(resultText);
                    mcOther = regOther.Matches(resultText);
                    return (mc.Count + mcOther.Count).ToString();
                    break;
                case EnumSearchEngine.Google:
                    Html = HtmlCatch.GetHTMLByUrl("http://www.google.com.hk/search?hl=zh-CN&source=hp&num=100&q=" + HttpUtility.UrlEncode(KeyWord) + "", "Get", "", false, System.Text.Encoding.UTF8);
                    resultText = SeoHelper.GetMetaString(Html, "<title>", "<br><cite>\\S*?" + Url, true);
                    reg = new Regex("<li class=g>");
                    mc = reg.Matches(resultText);
                    return mc.Count.ToString();
                    break;
                default:
                    break;
            }
            return "0";

        }
        /// <summary>
        ///  获取某站点系列关键字在某搜索引擎的排名
        /// </summary>
        /// <returns></returns>
        public static List<KeyWords> GetKeyWordInfo(EnumSearchEngine _engine, string Url, List<string> list)
        {
            List<KeyWords> KeyList = new List<KeyWords>();
            foreach (string keyword in list)
            {
                KeyWords Model = new KeyWords();
                Model.Rank = GetKeyWordInfo(_engine, Url, keyword);
                Model.Keyname = keyword;
                KeyList.Add(Model);
            }
            return KeyList;
        }
        /// <summary>
        /// 获取某站点特定搜索引擎信息
        /// </summary>
        public static SearchEngineInfo SeoModel(string Url, EnumSearchEngine _engine)
        {
            string[] meta = GetMeta(Url);
            SearchEngineInfo Model = new SearchEngineInfo();
            Model.SearchEngine = _engine;
            Model.SiteUrl = Url;

            Model.Title = meta[0];
            Model.Keywords = meta[1];
            Model.Description = meta[2];
            Model.Record = GetRecordInfo(_engine, Url).Replace("数","").Replace("约","") ;
            Model.BackLink = GetBackLinkInfo(_engine, Url).Replace("数", "").Replace("约", "");
            if (_engine == EnumSearchEngine.Google)
            {
                Model.PR = PRCrack.PageRank.CheckPR("http://" + Url);
            }
            return Model;
        }
        /// <summary>
        ///  获取某站点所有搜索引擎信息
        /// </summary>
        public static List<SearchEngineInfo> GetList(string Url)
        {
            return ListAdd(Url);
        }
        /// <summary>
        ///  根据站点和搜索引擎列表获取相关信息
        /// </summary>
        public static List<SearchEngineInfo> GetList(string Url, List<EnumSearchEngine> list)
        {
            List<SearchEngineInfo> SeoList = new List<SearchEngineInfo>();
            foreach (EnumSearchEngine model in list)
            {
                SeoList.Add(SeoModel(Url, model));
            }
            return SeoList;
        }
        /// <summary>
        ///  某站点在某搜索引擎下所有信息
        /// </summary>
        public static SiteSeoInfo SeoinfoAppendModelInfo(EnumSearchEngine _engine, string Url, List<string> list)
        {
            SiteSeoInfo Model = new SiteSeoInfo();
            Model.SearchEngine = _engine;
            Model.SiteUrl = Url;
            Model.Record = GetRecordInfo(_engine, Url).Replace("数", "").Replace("约", "");
            Model.BackLink = GetBackLinkInfo(_engine, Url).Replace("数", "").Replace("约", "");
            if (_engine == EnumSearchEngine.Google)
            {
                Model.PR = PRCrack.PageRank.CheckPR("http://" + Url);
            }
            Model.listKeyword = GetKeyWordInfo(_engine, Url, list);
            return Model;
        }

        #region 内部方法
        protected static List<SearchEngineInfo> ListAdd(string Url)
        {
            List<SearchEngineInfo> list = new List<SearchEngineInfo>();
            list.Add(SeoModel(Url, EnumSearchEngine.Baidu));
            list.Add(SeoModel(Url, EnumSearchEngine.Bing));
            list.Add(SeoModel(Url, EnumSearchEngine.Google));
            list.Add(SeoModel(Url, EnumSearchEngine.Sogou));
            list.Add(SeoModel(Url, EnumSearchEngine.Soso));
            list.Add(SeoModel(Url, EnumSearchEngine.Yahoo));
            list.Add(SeoModel(Url, EnumSearchEngine.Youdao));
            return list;
        }
        #endregion
    }
}
