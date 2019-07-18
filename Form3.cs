using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace EasyMaple
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private EasyMapleConfig easyconfig = new EasyMapleConfig();
        private HttpHelper httph = new HttpHelper();
        private const string urlLogin = "https://nid.naver.com/nidlogin.login";
        private const string urlCheckLogin = "https://mail.naver.com/json/option/folderView/set/";
        private const string naverLogin = "https://game.naver.com";
        private const string mapleLogin = "http://nxgamechanneling.nexon.game.naver.com/login/loginproc.aspx?redirect=https%3a%2f%2fmaplestory.nexon.game.naver.com%2fHome%2fMain&ts=20190629215725&gamecode=589824";
        private const string mapleHome = "https://maplestory.nexon.game.naver.com/Home/Main";
        private const string mapleStart = "https://maplestory.nexon.game.naver.com/authentication/swk?h=";
        private const string updateCookie = "https://sso.nexon.game.naver.com/Ajax/Default.aspx?_vb=UpdateSession&_cs=13543088";
        private string ngmPath = "";
        private CookieContainer mCookies = new CookieContainer();
        private string encPwd = "";
        private bool alertOne = false;
        private string naverStr = "";

        private void Button1_Click(object sender, EventArgs e)
        {
            string userKey = this.textBox1.Text.Trim();
            if (!string.IsNullOrWhiteSpace(userKey))
            {
                Log(string.Format("密钥获取成功\"{0}\"，准备登录。", userKey));
            }
            this.textBox1.Text = "";

            var Login = new Task(new Action(() =>
            {
                mCookies = new CookieContainer();
                HttpItem item = new HttpItem();
                if (!string.IsNullOrWhiteSpace(userKey))
                {
                    item.URL = urlLogin;
                    item.Method = "POST";
                    item.Postdata = string.Format("localechange=&mode=number&svctype=1&smart_LEVEL=1&bvsd=&locale=zh-Hans_CN&url=http%3A%2F%2Fwww.naver.com&key={0}&nvlong=on", userKey);
                    item.Referer = "https://nid.naver.com/nidlogin.login?mode=number";
                    item.ContentType = "application/x-www-form-urlencoded";
                    item.Allowautoredirect = true;
                    item.CookieContainer = mCookies;
                    var result = httph.GetHtml(item);
                    naverStr = result.Cookie;
                    //获取冒险岛登录参数,获取的到，则说明登录成功
                    if (result.CookieCollection == null || result.CookieCollection.Count <= 0)
                    {
                        Log("Naver登录失败，请重试。");
                        return;
                    }
                }
                else
                {
                    item.URL = urlCheckLogin;
                    item.Cookie = naverStr;
                    var result = httph.GetHtml(item);
                    if (result.Html.Trim().IndexOf("NOLOGIN") > -1)
                    {
                        Log("Naver登录信息失效，请重新登录。");
                        easyconfig.NaverCookie = "";
                        easyconfig.Save();
                        return;
                    }
                    //将cookie塞到CookiContainer中
                    var array = naverStr.Replace(" ", "").Replace("HttpOnly", "").Split(new string[] { ";," }, StringSplitOptions.RemoveEmptyEntries);
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
                        mCookies.Add(cookieItem);
                    }
                }
                Log("Naver登录成功，开始进入Naver Game。");
                Log("Naver信息已保存，重新登录无需输入一次性密钥。请直接点击登录。");
                easyconfig.NaverCookie = naverStr;
                easyconfig.Save();
                item = new HttpItem();
                item.URL = naverLogin;
                item.CookieContainer = mCookies;
                var naverGameResult = httph.GetHtml(item);
                if (naverGameResult.Cookie == null || naverGameResult.Cookie.IndexOf("GDP_LOGIN") == -1 || naverGameResult.Cookie.IndexOf("PN_LOGIN") == -1)
                {
                    Log("Naver Game登录失败，请重试。");
                    return;
                }
                Log("Naver Game登录成功，开始登录冒险岛。");
                item = new HttpItem();
                item.URL = mapleLogin;
                item.CookieContainer = mCookies;
                item.Header.Add("DNT", "1");
                var mapleResult = httph.GetHtml(item);
                if (mapleResult.Cookie == null || mapleResult.Cookie.IndexOf("ENC") == -1 || mapleResult.Cookie.IndexOf("NPP") == -1)
                {
                    Log("Nexon Game登录失败，请重试。");
                    return;
                }
                item = new HttpItem();
                item.URL = mapleHome;
                item.Referer = mapleLogin;
                item.CookieContainer = mCookies;
                item.Header.Add("DNT", "1");
                var homeResult = httph.GetHtml(item);
                encPwd = GetCookie("MSGENCT", mCookies);
                if (string.IsNullOrEmpty(encPwd))
                {
                    Log("冒险岛登录失败，请重试。");
                    return;
                }
                Log("冒险岛登录成功！");
            }));
            Login.Start();
        }

        public string GetCookie(string cookieName, CookieContainer cc)

        {
            List<Cookie> lstCookies = new List<Cookie>();
            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.Instance, null, cc, new object[] { });
            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                    | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c1 in colCookies) lstCookies.Add(c1);
            }

            var model = lstCookies.Find(p => p.Name == cookieName);
            if (model != null)
            {
                return model.Value;
            }
            return string.Empty;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            bool isShow = this.WindowState == FormWindowState.Normal;
            if (string.IsNullOrWhiteSpace(this.easyconfig.MaplePath) || string.IsNullOrWhiteSpace(this.easyconfig.LEPath))
            {
                Log("请填写冒险岛路径或LE路径。");
                if (!isShow) this.notifyIcon1.ShowBalloonTip(30, "注意", "请填写冒险岛路径或LE路径。", ToolTipIcon.Error);
                Form4 form = new Form4();
                form.ShowDialog();
                return;
            }
            if (string.IsNullOrEmpty(ngmPath))
            {
                Log("请安装NGM。");
                if (!isShow) this.notifyIcon1.ShowBalloonTip(30, "注意", "请安装NGM。", ToolTipIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(encPwd))
            {
                Log("请先登录。");
                if (!isShow) this.notifyIcon1.ShowBalloonTip(30, "注意", "请先登录。", ToolTipIcon.Error);
                return;
            }
            var Login = new Task(new Action(() =>
            {
                Log("正在请求游戏启动密钥。");
                //if (!isShow) this.notifyIcon1.ShowBalloonTip(30, "注意", "正在请求游戏密钥。", ToolTipIcon.Info);
                HttpItem item = new HttpItem();
                item.URL = updateCookie;
                item.CookieContainer = mCookies;
                var update = httph.GetHtml(item);

                item = new HttpItem();
                item.URL = mapleStart;
                item.Method = "POST";
                item.Referer = mapleHome;
                item.ContentType = "application/x-www-form-urlencoded";
                item.CookieContainer = mCookies;
                encPwd = string.Empty;
                var result = httph.GetHtml(item);
                encPwd = GetCookie("MSGENC", mCookies);
                if (string.IsNullOrEmpty(encPwd))
                {
                    Log("糟糕，启动密钥获取失败。重新登录吧。");
                    if (!isShow) this.notifyIcon1.ShowBalloonTip(30, "注意", "糟糕，启动密钥获取失败。重新登录吧。", ToolTipIcon.Error);
                    this.Button1_Click(null, null);
                    return;
                }
                string argument = string.Format("-dll:platform.nexon.com/NGM/Bin/NGMDll.dll:1 -locale:KR -mode:launch -game:589825:0 -token:'{0}:3' -passarg:'WebStart' -timestamp:{1} -position:'GameWeb|https://maplestory.nexon.game.naver.com/Home/Main' -service:6 -architectureplatform:'none'", encPwd, GetTimeStamp(DateTime.Now.AddHours(1)));
                string protocolUrl = "ngm://launch/%20" + HttpUtility.UrlEncode(argument).Replace("%27", "'").Replace("+", "%20");

                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                string inputTxt = string.Format("start {0} {1} ", ngmPath, protocolUrl);
                process.StandardInput.WriteLine(inputTxt + "&exit");
                process.StandardInput.AutoFlush = true;
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                process.Close();
                Log("密钥获取成功，启动冒险岛中。🚀");
                if (!isShow) this.notifyIcon1.ShowBalloonTip(30, "注意", "密钥获取成功，启动冒险岛中。🚀", ToolTipIcon.Info);
            }));
            Login.Start();
        }

        public sealed class EasyMapleConfig : ApplicationSettingsBase
        {
            [UserScopedSetting]
            public string MaplePath
            {
                get { return Convert.ToString(this["MaplePath"]); }
                set { this["MaplePath"] = value; }
            }

            [UserScopedSetting]
            public string LEPath
            {
                get { return Convert.ToString(this["LEPath"]); }
                set { this["LEPath"] = value; }
            }

            [UserScopedSetting]
            public string NaverCookie
            {
                get { return Convert.ToString(this["NaverCookie"]); }
                set { this["NaverCookie"] = value; }
            }
        }

        public static string GetTimeStamp(System.DateTime time)
        {
            long ts = ConvertDateTimeToInt(time);
            return ts.ToString();
        }
        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }

        private void Log(string logdata)
        {
            this.richTextBox1.BeginInvoke(new Action(() =>
            {
                //yyyy-MM-dd hh:mm:ss fff
                this.richTextBox1.Text = this.richTextBox1.Text.Insert(0, "[" + DateTime.Now.ToString("HH:mm:ss") + "]" + logdata + "\n");
            }));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //修改注册表值
            RegistryKey RegistryRoot = Registry.LocalMachine;
            string[] path = new string[] { "SOFTWARE", "Wizet", "Maple" };
            foreach (string p in path)
            {
                if (RegistryRoot != null)
                    RegistryRoot = RegistryRoot.OpenSubKey(p, true);
            }
            if (RegistryRoot != null)
            {
                object value = RegistryRoot.GetValue("RootPath");
                System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
                if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                {
                    RegistryRoot.SetValue("RootPath", Environment.CurrentDirectory);
                }
            }
            else
            {
                Log("未找到冒险岛目录，请安装游戏。");
            }
            RegistryRoot = Registry.ClassesRoot;
            path = new string[] { "ngm", "Shell", "Open", "Command" };
            foreach (string p in path)
            {
                if (RegistryRoot != null)
                    RegistryRoot = RegistryRoot.OpenSubKey(p, true);
            }
            if (RegistryRoot != null)
            {
                object value = RegistryRoot.GetValue("");
                try
                {
                    ngmPath = Convert.ToString(value);
                    ngmPath = ngmPath.Split(' ')[0].Replace("\"", "");
                    if (string.IsNullOrWhiteSpace(ngmPath))
                    {
                        Log("未找到NGM，请先安装。http://platform.nexon.com/NGM/Bin/Setup.exe");
                    }
                }
                catch
                {
                    Log("未找到NGM，请先安装。http://platform.nexon.com/NGM/Bin/Setup.exe");
                }
            }
            else
            {
                Log("未找到NGM，请先安装。http://platform.nexon.com/NGM/Bin/Setup.exe");
            }
            //RegisterHotKey(Handle, 12138, KeyModifiers.None, Keys.F3);

            naverStr = easyconfig.NaverCookie;
            if (!string.IsNullOrEmpty(naverStr))
            {
                //
                this.Button1_Click(null, null);
            }
        }

        //private void FrameEndFunc(object sender, FrameLoadEndEventArgs e)
        //{
        //    isInitOver = true;
        //    var url = this.chromiumWebBrowser1.GetBrowser().MainFrame.Url;
        //    if (url.Equals(mapleLogin))
        //    {
        //        isInitMaple = true;
        //        Log("登录冒险岛成功。");
        //    }
        //    //if (isInitMaple)
        //    //{
        //    //    //CookieVisitor visitor = new CookieVisitor();
        //    //    //visitor.SendCookie += visitor_SendCookie;
        //    //    //this.chromiumWebBrowser1.GetCookieManager().VisitAllCookies(visitor);
        //    //}
        //    //Log(this.chromiumWebBrowser1.GetBrowser().MainFrame.Url);
        //}

        //private void visitor_SendCookie(CefSharp.Cookie obj)
        //{
        //    if (obj.Name.Equals("MSGENCT"))
        //    {
        //        Process process = new Process();
        //        process.StartInfo.FileName = "cmd.exe";
        //        process.StartInfo.UseShellExecute = false;
        //        process.StartInfo.RedirectStandardInput = true;
        //        process.StartInfo.RedirectStandardOutput = true;
        //        process.StartInfo.RedirectStandardError = true;
        //        process.StartInfo.CreateNoWindow = true;
        //        process.Start();
        //        process.StandardInput.WriteLine(string.Format("call {0} launch/ -dll%3Aplatform.nexon.com%2FNGM%2FBin%2FNGMDll.dll%3A1%20-locale%3AKR%20-mode%3Alaunch%20-game%3A589825%3A0%20-token%3A'{1}%3A3'%20-passarg%3A'WebStart'%20-timestamp%3A1561746122847%20-position%3A'GameWeb%7Chttps%3A%2F%2Fmaplestory.nexon.game.naver.com%2FHome%2FMain'%20-service%3A6%20-architectureplatform%3A'none'", "C:\\ProgramData\\Nexon\\NGM\\NGM.exe", obj.Value) + "&exit");
        //        process.StandardInput.AutoFlush = true;
        //        process.WaitForExit();
        //        process.Close();
        //    }
        //}

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.qq.com/doc/DUVVjaERXd1NlQXlv");
        }

        private void ToolStripLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.qq.com/doc/DUU91SVZMbXVHc3dw");
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            Form4 form = new Form4();
            form.ShowDialog();
        }

        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {

        }

        private void Form3_MinimumSizeChanged(object sender, EventArgs e)
        {
            this.Hide();
            this.notifyIcon1.ShowBalloonTip(30, "注意", "程序已最小化，右键可以快速启动游戏。", ToolTipIcon.Info);
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //UnregisterHotKey(Handle, 12138);
            this.notifyIcon1.Visible = false;
            System.Environment.Exit(0);
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Button2_Click(null, null);
        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
        }

        private void Form3_SizeChanged(object sender, EventArgs e)
        {

        }

        private void ToolStripButton1_Click_1(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            List<Cookie> lstCookies = new List<Cookie>();
            Hashtable table = (Hashtable)mCookies.GetType().InvokeMember("m_domainTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.Instance, null, mCookies, new object[] { });
            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                    | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c1 in colCookies) lstCookies.Add(c1);
            }
            foreach (var item in lstCookies)
            {
                InternetSetCookie(item.Domain, item.Name, item.Value);
            }
            System.Diagnostics.Process.Start(mapleHome);
        }

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);

        private void 帮助LToolStripButton_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            if (!alertOne)
            {
                this.notifyIcon1.ShowBalloonTip(30, "注意", "程序已最小化，右键可以快速启动游戏。", ToolTipIcon.Info);
                this.alertOne = true;
            }
        }

        //如果函数执行成功，返回值不为0。
        //如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd,
            int id, KeyModifiers keyModifiers,
            Keys vk
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd,                //要取消热键的窗口的句柄
            int id                      //要取消热键的ID
            );

        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            //按快捷键    
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 12138:
                            this.Button2_Click(null, null);
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }
    }

    //public class CefRequest : IRequestHandler
    //{

    //    public bool GetAuthCredentials(IWebBrowser browserControl, IBrowser browser, IFrame frame, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
    //    {
    //        return false;
    //    }

    //    public bool OnBeforeBrowse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, bool isRedirect)
    //    {
    //        return false;
    //    }

    //    public bool OnBeforePluginLoad(IWebBrowser browser, string url, string policyUrl, WebPluginInfo info)
    //    {
    //        return false;
    //    }

    //    public CefReturnValue OnBeforeResourceLoad(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
    //    {
    //        var headers = request.Headers;
    //        request.Headers = headers;

    //        return CefReturnValue.Continue;
    //    }

    //    public bool OnCertificateError(IWebBrowser browser, CefErrorCode errorCode, string requestUrl)
    //    {
    //        return false;
    //    }

    //    public void OnPluginCrashed(IWebBrowser browser, string pluginPath)
    //    {
    //    }

    //    public void OnRenderProcessTerminated(IWebBrowser browserControl, IBrowser browser, CefTerminationStatus status)
    //    {
    //    }

    //    public IResponseFilter GetResourceResponseFilter(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
    //    {
    //        return null;
    //    }

    //    public bool OnCertificateError(IWebBrowser browserControl, IBrowser browser, CefErrorCode errorCode, string requestUrl, ISslInfo sslInfo, IRequestCallback callback)
    //    {
    //        return false;
    //    }

    //    public bool OnOpenUrlFromTab(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture)
    //    {
    //        return false;
    //    }

    //    public void OnPluginCrashed(IWebBrowser browserControl, IBrowser browser, string pluginPath)
    //    {
    //    }

    //    public bool OnProtocolExecution(IWebBrowser browserControl, IBrowser browser, string url)
    //    {
    //        return true;
    //    }

    //    public bool OnQuotaRequest(IWebBrowser browserControl, IBrowser browser, string originUrl, long newSize, IRequestCallback callback)
    //    {
    //        return false;
    //    }

    //    public void OnRenderViewReady(IWebBrowser browserControl, IBrowser browser)
    //    {
    //    }

    //    public void OnResourceLoadComplete(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength)
    //    {
    //    }

    //    public void OnResourceRedirect(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, ref string newUrl)
    //    {
    //    }

    //    public bool OnResourceResponse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
    //    {
    //        return false;
    //    }

    //    public bool OnBeforeBrowse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect)
    //    {
    //        return false;
    //    }

    //    public bool OnSelectClientCertificate(IWebBrowser chromiumWebBrowser, IBrowser browser, bool isProxy, string host, int port, X509Certificate2Collection certificates, ISelectClientCertificateCallback callback)
    //    {
    //        return true;
    //    }

    //    public bool CanGetCookies(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
    //    {
    //        return true;
    //    }

    //    public bool CanSetCookie(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, CefSharp.Cookie cookie)
    //    {
    //        return true;
    //    }

    //    public void OnResourceRedirect(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response, ref string newUrl)
    //    {

    //    }
    //}

    //class CookieVisitor : ICookieVisitor
    //{
    //    public event Action<CefSharp.Cookie> SendCookie;
    //    public bool Visit(CefSharp.Cookie cookie, int count, int total, ref bool deleteCookie)
    //    {
    //        deleteCookie = false;
    //        if (SendCookie != null)
    //        {
    //            SendCookie(cookie);
    //        }
    //        return true;
    //    }
    //    public void Dispose()
    //    {
    //    }
    //}
}
