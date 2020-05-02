using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EasyMaple
{
    public class NaverIdService
    {
        public static async Task<string> LoginNaver(string oneCodeKey)
        {
            HttpHelper httph = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = ConstStr.urlLogin,
                Method = "POST",
                Postdata = ConstStr.loginParam + $"&key={oneCodeKey}",
                Referer = ConstStr.urlLoginWithNumber,
                ContentType = "application/x-www-form-urlencoded",
                Allowautoredirect = true,
                CookieContainer = new CookieContainer()
            };
            var result = httph.GetHtml(item);
            var ret = await Task.FromResult(result.CookieCollection == null || result.CookieCollection.Count <= 0 ? string.Empty : result.Cookie);
            return ret;
        }

        public static async Task<string> ReLoginNaver(string defaultCookie)
        {
            HttpHelper httph = new HttpHelper();
            HttpItem item = new HttpItem();
            item.URL = ConstStr.urlCheckLogin;
            item.Cookie = defaultCookie;
            var result = httph.GetHtml(item);
            var ret = await Task.FromResult(result.Html.Trim().IndexOf("NOLOGIN") > -1 ? string.Empty : defaultCookie);
            return ret;
        }

        public static void ReLoadCookieContainer(string mapleCookieStr, ref CookieContainer cookie)
        {
            cookie = new CookieContainer();
            var array = mapleCookieStr.Replace(" ", "").Replace("HttpOnly", "")
                .Split(new string[] { ";," }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < array.Length; i++)
            {
                var aitem = array[i].Split(new char[] { ';' });
                Cookie cookieItem = new Cookie();
                for (int j = 0; j < aitem.Length; j++)
                {
                    if (j == 0)
                    {
                        cookieItem.Name = aitem[j].ToString().Split('=')[0];
                        cookieItem.Value = aitem[j].ToString().Split('=')[1];
                    }
                    if (aitem[j].ToString().IndexOf("expires") > -1)
                    {
                        //cookieItem.Expires = Convert.ToDateTime(aitem[j].ToString().Split('=')[1]);
                    }
                    if (aitem[j].ToString().IndexOf("path") > -1)
                    {
                        cookieItem.Path = Convert.ToString(aitem[j].ToString().Split('=')[1]);
                    }
                    if (aitem[j].ToString().IndexOf("domain") > -1)
                    {
                        cookieItem.Domain = Convert.ToString(aitem[j].ToString().Split('=')[1]);
                    }
                }
                cookie.Add(cookieItem);
            }
        }
    }
}
