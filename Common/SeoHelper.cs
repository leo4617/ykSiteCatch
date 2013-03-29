using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    /// <summary>
    /// SEO帮助类
    /// </summary>
    public static class SeoHelper
    {
        /// <summary>
        ///  通过正则表达式从字符串截获网站【收录数】及【反链数】
        /// </summary>
        /// <param name="html">页面HTML</param>
        /// <param name="regStart">匹配前缀</param>
        /// <param name="regEnd">匹配后缀</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public static string GetMetaString(string html, string regStart, string regEnd, bool ignoreCase)
        {
            string regString = string.Format("{0}(?<getcontent>[\\s|\\S]+?){1}", regStart, regEnd);
            Regex reg;
            if (ignoreCase)
            {
                reg = new Regex(regString, RegexOptions.IgnoreCase);
            }
            else
            {
                reg = new Regex(regString);
            }
            return reg.Match(html).Groups["getcontent"].Value;
        }
        /// <summary>
        ///  正则表达式信息设置
        /// </summary>
        /// <param name="_engine"></param>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static CutRegexInfo GetCutRegexInfo(EnumSearchEngine _engine, string Url, bool isRecord)
        {
            CutRegexInfo Model = new CutRegexInfo();
            switch (_engine)
            {
                case EnumSearchEngine.Google:
                    Model.regStart = "获得约";
                    Model.regEnd = "条结果";
                    Model.encoding = "gb2312";
                    if (isRecord)
                        Model.siteUrl = "http://www.google.com.hk/search?hl=zh-CN&source=hp&q=site%3A" + Url;
                    else
                        Model.siteUrl = "http://www.google.com.hk/search?hl=zh-CN&source=hp&q=link:" + Url;
                    break;
                case EnumSearchEngine.Baidu:
                    Model.regStart = "找到相关网页约";
                    Model.regEnd = "篇";
                    Model.encoding = "gb2312";
                    if (isRecord)
                        Model.siteUrl = "http://www.baidu.com/s?wd=site%3A" + Url;
                    else
                        Model.siteUrl = "http://www.baidu.com/s?cl=3&wd=domain:" + Url;
                    break;
                case EnumSearchEngine.Yahoo:
                    Model.regStart = "找到相关网页约";
                    Model.regEnd = "条";
                    Model.encoding = "utf-8";                    
                    if (isRecord)
                        Model.siteUrl = "http://one.cn.yahoo.com/s?p=site%3A" + Url;
                    else
                        Model.siteUrl = "http://sitemap.cn.yahoo.com/search?p=" + Url + "&bwm=i";
                    break;
                case EnumSearchEngine.Sogou:
                    Model.regStart = "找到";
                    Model.regEnd = "个网页";
                    Model.encoding = "gb2312";                    
                    if (isRecord)
                        Model.siteUrl = "http://www.sogou.com/web?query=site%3A" + Url;
                    else
                        Model.siteUrl = "http://www.sogou.com/web?query=link:" + Url;
                    break;
                case EnumSearchEngine.Soso:
                    Model.regStart = "搜索到约";
                    Model.regEnd = "项结果";
                    Model.encoding = "gb2312";                    
                    if (isRecord)
                        Model.siteUrl = "http://www.soso.com/q?pid=s.idx&w=site%3A" + Url;
                    else
                        Model.siteUrl = "http://www.soso.com/q?pid=s.idx&w=link:" + Url;
                    break;
                case EnumSearchEngine.Bing:
                    Model.regStart = "共";
                    Model.regEnd = "条";
                    Model.encoding = "utf-8";                    
                    if (isRecord)
                        Model.siteUrl = "http://cn.bing.com/search?form=QBLH&filt=all&q=site%3A" + Url;
                    else
                        Model.siteUrl = "http://cn.bing.com/search?form=QBLH&filt=all&q=link:" + Url;
                    break;
                case EnumSearchEngine.Youdao:
                    Model.regStart = "共约";
                    Model.regEnd = "条结果";
                    Model.encoding = "utf-8";                    
                    if (isRecord)
                        Model.siteUrl = "http://www.youdao.com/search?ue=utf8&keyfrom=web.index&q=site:" + Url;
                    else
                        Model.siteUrl = "http://www.youdao.com/search?ue=utf8&keyfrom=web.index&q=link:" + Url;
                    break;
            }
            return Model;
        }
    }
}