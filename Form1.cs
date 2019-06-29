using Microsoft.Win32;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace EasyMaple
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //private ChromiumWebBrowser chromiumWebBrowser1;
        private bool isInitOver = false;
        private bool isInitMaple = false;
        private EasyMapleConfig easyconfig = new EasyMapleConfig();
        private const string urlLogin = "https://nid.naver.com/nidlogin.login?mode=number&url=https%3A%2F%2Fgame.naver.com%2Flogin.nhn%3FnxtUrl%3Dhttps%253A%252F%252Fmaplestory.nexon.game.naver.com%252FHome%252FMain";
        private const string mapleLogin = "https://maplestory.nexon.game.naver.com/Home/Main";

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!isInitOver)
            {
                Log("正在准备初始化参数，请稍后。");
                return;
            }
            string userKey = this.textBox1.Text;
            if (string.IsNullOrWhiteSpace(userKey))
            {
                Log("请输入一次性登录密钥。");
                return;
            }
            Log("登录到Naver，获取登录信息中，请稍后。。。");
            this.button1.Enabled = false;

            HtmlElement script = webBrowser1.Document.CreateElement("script");
            script.SetAttribute("type", "text/javascript");
            script.SetAttribute("text", "function _func(){document.getElementById('disposable_num').value = " + userKey + "; document.getElementsByClassName('int_jogin')[0].click();}");
            HtmlElement head = webBrowser1.Document.Body.AppendChild(script);
            webBrowser1.Document.InvokeScript("_func");

            this.isInitOver = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string maplepath = this.textBox4.Text;
            string lepath = this.textBox3.Text;
            if (string.IsNullOrWhiteSpace(maplepath) || string.IsNullOrWhiteSpace(lepath))
            {
                Log("请填写冒险岛路径或LE路径。");
                return;
            }
            //记录下来，便于下次登录
            this.easyconfig.MaplePath = maplepath;
            this.easyconfig.LEPath = lepath;
            this.easyconfig.Save();

            if (!isInitMaple)
            {
                Log("请先登录冒险岛，获取冒险岛参数。");
                return;
            }
            Log("准备启动冒险岛，请稍后。");
            HtmlElement script = webBrowser1.Document.CreateElement("script");
            script.SetAttribute("type", "text/javascript");
            script.SetAttribute("text", "function _func(){ PLATFORM.LaunchGame('3'); }");
            HtmlElement head = webBrowser1.Document.Body.AppendChild(script);
            webBrowser1.Document.InvokeScript("_func");

            //this.chromiumWebBrowser1.GetBrowser().MainFrame.EvaluateScriptAsync("PLATFORM.LaunchGame('3');");
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
        }

        private void Log(string logdata)
        {
            this.richTextBox1.BeginInvoke(new Action(() =>
            {
                //yyyy-MM-dd hh:mm:ss fff
                this.richTextBox1.Text = this.richTextBox1.Text.Insert(0, "[" + DateTime.Now.ToString("HH:mm:ss fff") + "]" + logdata + "\n");
            }));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.textBox4.Text = this.easyconfig.MaplePath;
            this.textBox3.Text = this.easyconfig.LEPath;

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
                Log("冒险岛注册表目录异常，请重试。");
            }

            this.webBrowser1.Navigate(urlLogin);

            //this.chromiumWebBrowser1 = new ChromiumWebBrowser(urlLogin);
            //this.chromiumWebBrowser1.RequestHandler = new CefRequest();
            //this.panel1.Controls.Add(this.chromiumWebBrowser1);
            //this.chromiumWebBrowser1.FrameLoadEnd += new EventHandler<FrameLoadEndEventArgs>(FrameEndFunc);
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
            Form2 form = new Form2();
            form.ShowDialog();

            this.webBrowser1.Navigate(urlLogin);
            this.button1.Enabled = true;
            //this.chromiumWebBrowser1 = new ChromiumWebBrowser(urlLogin);
            //this.chromiumWebBrowser1.RequestHandler = new CefRequest();
            //this.panel1.Controls.Add(this.chromiumWebBrowser1);
            //this.chromiumWebBrowser1.FrameLoadEnd += new EventHandler<FrameLoadEndEventArgs>(FrameEndFunc);
            //this.button1.Enabled = true;
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.Equals(urlLogin) && this.webBrowser1.ReadyState == WebBrowserReadyState.Complete)
            {
                this.isInitOver = true;
                Log("初始化完成，请登录。");
            }
            if (e.Url.Equals(mapleLogin) && this.webBrowser1.ReadyState == WebBrowserReadyState.Complete)
            {
                this.isInitMaple = true;
                Log("冒险岛准备就绪，可以启动游戏了。");
            }
        }

        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.notifyIcon1.Visible = false;
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
