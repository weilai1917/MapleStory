using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EasyMaple
{
    public class MapleIdService
    {
        public static string LoginMaple(CookieContainer mapleCookie)
        {
            HttpHelper httph = new HttpHelper();
            var naverGameResult = httph.GetHtml(new HttpItem()
            {
                URL = ConstStr.naverLogin,
                CookieContainer = mapleCookie,
            });
            if (naverGameResult.Cookie == null || naverGameResult.Cookie.IndexOf("GDP_LOGIN") == -1 || naverGameResult.Cookie.IndexOf("PN_LOGIN") == -1)
            {
                //Util.LogTxt(naverGameResult.Html, this.MapleConfig.DeveloperMode);
                return string.Empty;
            }

            HttpItem item2 = new HttpItem()
            {
                URL = ConstStr.mapleLogin,
                CookieContainer = mapleCookie,
            };
            item2.Header.Add("DNT", "1");
            var mapleResult = httph.GetHtml(item2);
            if (mapleResult.Cookie == null || mapleResult.Cookie.IndexOf("ENC") == -1 || mapleResult.Cookie.IndexOf("NPP") == -1)
            {
                //Util.LogTxt(mapleResult.Html, this.MapleConfig.DeveloperMode);
                return string.Empty;
            }

            HttpItem item3 = new HttpItem()
            {
                URL = ConstStr.mapleHome,
                Referer = ConstStr.mapleLogin,
                CookieContainer = mapleCookie,
            };
            item3.Header.Add("DNT", "1");
            var homeResult = httph.GetHtml(item3);
            string encPwd = Util.GetCookie("MSGENCT", mapleCookie);
            if (string.IsNullOrEmpty(encPwd))
            {
                //Util.LogTxt(homeResult.Html, this.MapleConfig.DeveloperMode);
                return string.Empty;
            }
            return encPwd;
        }

        public static List<string> LoadMapleIds(CookieContainer mapleCookie)
        {
            HttpHelper httph = new HttpHelper();
            HttpItem item = new HttpItem();
            item.URL = ConstStr.IDList;
            item.Referer = ConstStr.mapleHome;
            item.ContentType = "";
            item.Header.Add("DNT", "1");
            item.Header.Add("X-Requested-With", "XMLHttpRequest");
            item.CookieContainer = mapleCookie;
            var idLists = httph.GetHtml(item);
            if (idLists.StatusCode == HttpStatusCode.OK)
            {
                var opHtml = idLists.Html;
                //Util.LogTxt(opHtml, this.MapleConfig.DeveloperMode);
                List<string> ids = new List<string>();
                //<ul> < li > < a href = "javascript:void(0)" > 274355068 </ a >  < input type = "radio" name = "login_id_sel" value="274355068" /> < > </ ul >
                foreach (var op in opHtml.Split(new string[] { "input" }, StringSplitOptions.None))
                {
                    if (!op.Contains("value"))
                        continue;
                    string splitStr = op.Replace(" ", "");
                    int indexvalue = splitStr.IndexOf("value=\"") + 7;
                    int endindex = splitStr.IndexOf("\"", indexvalue);
                    ids.Add(splitStr.Substring(indexvalue, endindex - indexvalue));
                }
                return ids;
            }
            return null;
        }

        public static void UpdateMapleCookie(CookieContainer mapleCookie)
        {
            HttpHelper httph = new HttpHelper();
            HttpItem item1 = new HttpItem()
            {
                URL = ConstStr.updateCookie,
                CookieContainer = mapleCookie
            };
            var update = httph.GetHtml(item1);
            //Util.LogTxt(update.Html, this.MapleConfig.DeveloperMode);
        }
    }
}
