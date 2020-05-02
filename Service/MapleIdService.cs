using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EasyMaple
{
    public class MapleIdService
    {
        public static async Task<string> LoginMaple(CookieContainer mapleCookie)
        {
            HttpHelper httph = new HttpHelper();
            var naverGameResult = httph.GetHtml(new HttpItem()
            {
                URL = ConstStr.naverLogin,
                CookieContainer = mapleCookie,
            });
            if (naverGameResult.Cookie == null || naverGameResult.Cookie.IndexOf("GDP_LOGIN") == -1 || naverGameResult.Cookie.IndexOf("PN_LOGIN") == -1)
            {
                Util.LogTxt(naverGameResult.Html);
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
                Util.LogTxt(mapleResult.Html);
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
            await Task.FromResult(encPwd);
            return encPwd;
        }

        public static async Task<List<string>> LoadMapleIds(CookieContainer mapleCookie)
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
            List<string> ids = new List<string>();
            if (idLists.StatusCode == HttpStatusCode.OK)
            {
                var opHtml = idLists.Html;
                //Util.LogTxt(opHtml, this.MapleConfig.DeveloperMode);            
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
                await Task.FromResult(ids);
            }
            return ids;
        }

        public static async Task UpdateMapleCookie(CookieContainer mapleCookie)
        {
            HttpHelper httph = new HttpHelper();
            HttpItem item1 = new HttpItem()
            {
                URL = ConstStr.updateCookie,
                CookieContainer = mapleCookie
            };
            var update = httph.GetHtml(item1);
            await Task.FromResult(string.Empty);
            Util.LogTxt(update.Html);
        }

        public static async Task<string> StartGame(CookieContainer mapleCookie, EasyMapleConfig mapleConfig)
        {
            HttpHelper httph = new HttpHelper();
            HttpItem item1 = new HttpItem()
            {
                URL = ConstStr.mapleStart,
                Method = "POST",
                Referer = ConstStr.mapleHome,
                ContentType = "application/x-www-form-urlencoded",
                CookieContainer = mapleCookie
            };
            var result = httph.GetHtml(item1);
            string encPwd = Util.GetCookie("MSGENC", mapleCookie);
            var ret = await Task.FromResult(encPwd);
            //this.MapleEncPwd = encPwd;
            string protocolUrl = "ngm://launch/%20" + HttpUtility.UrlEncode(string.Format(ConstStr.ngmArgument, encPwd, Util.GetTimeStamp(DateTime.Now.AddHours(1)))).Replace("%27", "'").Replace("+", "%20");
            Util.ProcessStartByCmd($"start {mapleConfig.NgmPath} {protocolUrl} ");
            Util.LogTxt(protocolUrl, mapleConfig.DeveloperMode);
            Util.LogTxt($"start {mapleConfig.NgmPath} {protocolUrl} ", mapleConfig.DeveloperMode);
            return ret;
        }
    }
}
