using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    public class HtmlCatch
    {
        #region 获取网页源代码
        ///<summary> 
        ///获取网页源代码 
        ///</summary> 
        ///<paramname="url">URL路径</param> 
        ///<paramname="encoding">编码方式</param> 
        public static string GetHTML(string url, string encoding)
        {
            try
            {
                WebClient web = new WebClient();
                byte[] buffer = web.DownloadData(url);
                web.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于对向Internet资源的请求进行身份验证的网络凭据。
                return System.Text.Encoding.GetEncoding(encoding).GetString(buffer);
            }
            catch (Exception e)
            {
                return "";
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
