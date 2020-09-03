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
        public static async Task<string> LoginMaple(CookieContainer mapleCookie, bool dev = false)
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
                Util.LogTxt($"LoginMaple:{mapleResult.StatusCode.ToString()},{mapleResult.ResponseUri},{mapleResult.Html.Length}", dev);
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

        public static async Task<List<string>> LoadMapleIds(CookieContainer mapleCookie, bool dev = false)
        {
            HttpHelper httph = new HttpHelper();
            HttpItem item = new HttpItem();
            item.URL = ConstStr.mapleHome;
            item.Allowautoredirect = true;
            item.CookieContainer = mapleCookie;
            var mapleHomePage = httph.GetHtml(item);
            Util.LogTxt($"LoadMapleIds:{mapleHomePage.Html}", dev);

            int tokenIndex = mapleHomePage.Html.IndexOf("name=\"__RequestVerificationToken\"");
            int tokenValueIndex = mapleHomePage.Html.IndexOf("value=\"", tokenIndex) + 7;
            int tokenValueEnd = mapleHomePage.Html.IndexOf("\"", tokenValueIndex);
            string aToken = mapleHomePage.Html.Substring(tokenValueIndex, tokenValueEnd - tokenValueIndex);
            Util.LogTxt($"LoadMapleIdsToken:{aToken}", dev);

            item.URL = ConstStr.IDList;
            item.Method = "POST";
            item.Referer = ConstStr.mapleHome;
            item.Postdata = string.Format("__RequestVerificationToken={0}", aToken);
            item.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            item.Header.Add("DNT", "1");
            item.Header.Add("X-Requested-With", "XMLHttpRequest");
            item.CookieContainer = mapleCookie;
            var idLists = httph.GetHtml(item);
            List<string> ids = new List<string>();
            Util.LogTxt($"LoadMapleIds:{idLists.StatusCode.ToString()},{idLists.ResponseUri},{idLists.Html.Length}", dev);

            if (idLists.StatusCode == HttpStatusCode.OK)
            {
                var opHtml = idLists.Html;
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

        public static async Task<string> UpdateMapleCookie(CookieContainer mapleCookie, bool dev = false)
        {
            HttpHelper httph = new HttpHelper();
            HttpItem item1 = new HttpItem()
            {
                URL = ConstStr.updateCookie,
                CookieContainer = mapleCookie
            };
            var update = httph.GetHtml(item1);
            await Task.FromResult(string.Empty);
            Util.LogTxt($"UpdateMapleCookie:{update.StatusCode.ToString()},{update.ResponseUri},{update.Html.Length}", dev);
            return "";
        }

        public static async Task ChangeMapleIds(CookieContainer mapleCookie, string id, bool dev = false)
        {
            HttpHelper httph = new HttpHelper();
            HttpItem item = new HttpItem();
            item.URL = ConstStr.changeMapleId;
            item.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3";
            item.Method = "POST";
            item.Postdata = string.Format("id={0}&master=0&redirectTo=https%3A%2F%2Fmaplestory.nexon.game.naver.com%2FHome%2FMain", id);
            item.Referer = ConstStr.mapleHome;
            item.ContentType = "application/x-www-form-urlencoded";
            item.CookieContainer = mapleCookie;
            item.Header.Add("DNT", "1");
            item.Header.Add("Upgrade-Insecure-Requests", "1");
            var result = httph.GetHtml(item);
            Util.LogTxt(result.Html, dev);
            await Task.FromResult(string.Empty);
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
            Util.LogTxt($"NgmPath:{mapleConfig.NgmPath}", mapleConfig.DeveloperMode);
            Util.LogTxt($"EncPwd:{encPwd.Length}", mapleConfig.DeveloperMode);
            return ret;
        }
    }
}
