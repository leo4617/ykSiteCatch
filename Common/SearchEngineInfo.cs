using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class SearchEngineInfo
    {
        private EnumSearchEngine _SearchEngine;
        protected string _SiteUrl;
        private string _Record;
        private string _BackLink;
        private string _PR;
       /// <summary>
       ///  搜索引擎类型
       /// </summary>       
       public EnumSearchEngine SearchEngine
       {
           get { return _SearchEngine; }
           set { _SearchEngine = value; }
       }             
       /// <summary>
       ///  站点地址
       /// </summary>       
       public string SiteUrl
       {
           get { return _SiteUrl; }
           set { _SiteUrl = value; }
       }
       /// <summary>
       ///  收录
       /// </summary>       
       public string Record
       {
           get { return _Record; }
           set { _Record = value; }
       }             
       /// <summary>
       ///  反向链接
       /// </summary>
       public string BackLink
       {
           get { return _BackLink; }
           set { _BackLink = value; }
       }       
       /// <summary>
       ///  PR值
       /// </summary>
       public string PR
       {
           get { return _PR; }
           set { _PR = value; }
       }
    }

    /// <summary>
    ///  关键字信息
    /// </summary>
    public class SiteSeoInfo : SearchEngineInfo
    {
       /// <summary>
       ///  关键字
       /// </summary>
       private List<KeyWords> _listKeyword;
       /// <summary>
       ///  关键字
       /// </summary>
       public List<KeyWords> listKeyword
       {
           get { return _listKeyword; }
           set { _listKeyword = value; }
       }
    }
   /// <summary>
   ///  关键字实体
   /// </summary>
    public class KeyWords
    {
        private string _Keyname;
        private string _Rank;
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyname
        {
            get { return _Keyname; }
            set { _Keyname = value; }
        }
        /// <summary>
        /// 指数
        /// </summary>
        public string Rank
        {
            get { return _Rank; }
            set { _Rank = value; }
        }
    }

    /// <summary>
    ///  【搜索引擎】类别
    /// </summary>
    public enum EnumSearchEngine
    {
        Google = 1,//谷歌
        Baidu = 2,//百度
        Yahoo,//雅虎
        Sogou,//搜狗
        Soso,//搜搜
        Bing,//Bing
        Youdao//有道
    }
    /// <summary>
    ///  截取信息的正则表达式信息
    /// </summary>
    public class CutRegexInfo
    {
        //匹配前缀
        private string _regStart;
        public string regStart
        {
            get { return _regStart; }
            set { _regStart = value; }
        }
        //匹配后缀
        private string _regEnd;
        public string regEnd
        {
            get { return _regEnd; }
            set { _regEnd = value; }
        }
        //站点地址
        private string _siteUrl;
        public string siteUrl
        {
            get { return _siteUrl; }
            set { _siteUrl = value; }
        }
        //网页编码
        private string _enCoding;
        public string encoding
        {
            get { return _enCoding; }
            set { _enCoding = value; }
        }


    }
}
