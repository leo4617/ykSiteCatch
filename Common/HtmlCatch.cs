using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Caching;

namespace Common
{
    public class HtmlCatch
    {
        #region 获取网页源代码
        /// <summary>
        /// 传入URL返回网页的html代码
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="encode">设置编码</param>
        /// <returns></returns>
        public static string GetHtml(string url, string encode)
        {
            try
            {
                string content = string.Empty;
                string key = "Cache_Html_" + url;
                if (HttpRuntime.Cache[key] != null)
                {
                    return (string)HttpRuntime.Cache[key];
                }
                else
                {
                    CacheHelper.RemoveCache(key);
                    if (url != null || url.Trim() != "")
                    {
                        WebClient webClient = new WebClient();
                        webClient.Credentials = CredentialCache.DefaultCredentials;
                        byte[] myDataBuffer = webClient.DownloadData(url);
                        //使用默认的编码获取内容
                        content = Encoding.Default.GetString(myDataBuffer);
                        //获取网页字符编码描述信息
                        Match charSetMatch = Regex.Match(content, "<meta([^<]*)charset=([^<]*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string charSet = charSetMatch.Groups[2].Value;
                        //如果未获取到编码，则设置默认编码
                        if (charSet == null || charSet == "")
                        {
                            if (encode != "")
                            {
                                charSet = encode;
                            }
                            else
                            {
                                charSet = "UTF-8";
                            }
                        }
                        //重新用编码获取页面内容
                        content = Encoding.GetEncoding(charSet).GetString(myDataBuffer);
                        CacheHelper.AddCache(key, content, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                    }
                }
                return content;
            }
            catch (Exception ex)
            {
                return ex.Message; ;
            }
        }
        #endregion

        public static string GetHTMLByUrl(string url, string sMethod, string Param, bool bAutoRedirect, System.Text.Encoding ecode)
        {
            sMethod = sMethod.ToUpper();
            sMethod = sMethod != "POST" ? "GET" : sMethod;
            string res = "";
            HttpWebRequest re = (HttpWebRequest)HttpWebRequest.Create(url);
            re.Method = sMethod;
            re.AllowAutoRedirect = bAutoRedirect;
            re.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; MyIE2; .NET CLR 1.1.4322)";
            re.Timeout = 10000;
            if (sMethod == "POST") // Post data to Server
            {
                re.ContentType = "application/x-www-form-urlencoded";

                byte[] b = System.Text.Encoding.UTF8.GetBytes(Param);
                re.ContentLength = b.Length;
                try
                {
                    Stream oSRe = re.GetRequestStream();
                    oSRe.Write(b, 0, b.Length);
                    oSRe.Close();
                    oSRe = null;
                }
                catch (Exception)
                {
                    re = null;
                    return "-1";
                }
            }
            HttpWebResponse rep = null;
            Stream oResponseStream = null;
            StreamReader oSReader = null;
            try
            {
                rep = (HttpWebResponse)re.GetResponse();
                oResponseStream = rep.GetResponseStream();
                oSReader = new StreamReader(oResponseStream, ecode);
                res = oSReader.ReadToEnd();
            }
            catch (System.Net.WebException e)
            {
                res = e.ToString();
            }
            if (rep != null)
            {
                rep.Close();
                rep = null;
            }
            if (oResponseStream != null)
            {
                oResponseStream.Close();
                oResponseStream = null;
            }
            if (oSReader != null)
            {
                oSReader.Close();
                oSReader = null;
            }
            re = null;
            return res;
        }
        /// <summary>
        /// 传入URL返回网页的html代码【HttpWebRequest】
        /// </summary>
        /// <param name="url">网址--如：http://www.yunksoft.com</param>
        /// <returns>页面的源代码</returns>
        public static string GetPageHtml(string url, string encode)
        {
            try
            {
                string content = string.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //伪造浏览器数据，避免被防采集程序过滤
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0; .NET CLR 1.1.4322; .NET CLR 2.0.50215; CrazyCoder.cn;www.aligong.com)";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                // 获取输入流
                System.IO.Stream respStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.Default);
                content = reader.ReadToEnd();
                //获取网页字符编码描述信息
                Match charSetMatch = Regex.Match(content, "<meta([^<]*)charset=([^<]*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                string charSet = charSetMatch.Groups[2].Value;
                //如果未获取到编码，则设置默认编码
                if (charSet == null || charSet == "")
                {
                    if (encode != "")
                    {
                        charSet = encode;
                    }
                    else
                    {
                        charSet = "UTF-8";
                    }
                }
                //重新用编码获取页面内容
                respStream = response.GetResponseStream();
                reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding(charSet));
                content = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                return content;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public static string NoHTML(string Htmlstring)
        {
            //删除脚本<script[^>]*?>.*?</script>
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>([\s|\S])*?</script>", "",
              RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "",
              RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = Htmlstring.Trim();
            return Htmlstring;
        }
    }
}
