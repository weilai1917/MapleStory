using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMaple
{
    public class ConstStr
    {
        public const string GameFold = "Maple";//MapleT
        public const string GameName = "MapleStory.exe";//MapleStoryT.exe
        public const string WebSiteHome = "Testworld";//Home
        public const string urlLogin = "https://nid.naver.com/nidlogin.login";
        public const string urlLoginWithNumber = "https://nid.naver.com/nidlogin.login?mode=number";
        public const string loginParam = "localechange=&mode=number&svctype=1&smart_LEVEL=1&bvsd=&locale=zh-Hans_CN&url=http%3A%2F%2Fwww.naver.com&nvlong=on";
        public const string urlCheckLogin = "https://mail.naver.com/json/option/folderView/set/";
        public const string naverLogin = "https://game.naver.com";
        public const string mapleLogin = "http://nxgamechanneling.nexon.game.naver.com/login/loginproc.aspx?redirect=https%3a%2f%2fmaplestory.nexon.game.naver.com%2fHome%2fMain&ts=20190629215725&gamecode=589824";
        public const string mapleHome = "https://maplestory.nexon.game.naver.com/Home/Main";
        public const string mapleStart = "https://maplestory.nexon.game.naver.com/authentication/swk?h=";
        public const string updateCookie = "https://sso.nexon.game.naver.com/Ajax/Default.aspx?_vb=UpdateSession&_cs=13543088";
        public const string IDList = "https://maplestory.nexon.game.naver.com/Authentication/Email/IDList";
        public const string changeMapleId = "https://maplestory.nexon.game.naver.com/Authentication/Email/ChangeID";
        public const string ngmArgument = "-dll:platform.nexon.com/NGM/Bin/NGMDll.dll:1 -locale:KR -mode:launch -game:589825:0 -token:'{0}:3' -passarg:'WebStart' -timestamp:{1} -position:'GameWeb|https://maplestory.nexon.game.naver.com/Home/Main' -service:6 -architectureplatform:'none'";
        public const string ngmStart = "ngm://launch/%20";
        public static string[] mapleRegPath = new string[] { "SOFTWARE", "Wizet", "Maple" };
        public const string mapleRegValueKey = "RootPath";
        public static string[] ngmRegPath = new string[] { "ngm", "Shell", "Open", "Command" };

    }
}
