using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        private EasyMapleConfig easyconfig = new EasyMapleConfig();
        private HttpHelper http = new HttpHelper();
        private const string urlLogin = "https://nid.naver.com/nidlogin.login";
        private const string naverLogin = "https://game.naver.com/";
        private const string mapleLogin = "https://maplestory.nexon.game.naver.com/Home/Main";
        private const string mapleKey = "589825";
        private const string mapleStartType = "WebStart";
        private string mapleSessionId = string.Empty;

        private void Button1_Click(object sender, EventArgs e)
        {
            string userKey = this.textBox1.Text;
            if (string.IsNullOrWhiteSpace(userKey))
            {
                Log("请输入一次性登录密钥。");
                return;
            }
            Log("准备登录到Naver游戏。");
            var Login = new Task(new Action(() =>
            {
                HttpItem item = new HttpItem();
                item.URL = urlLogin;
                item.Method = "POST";
                item.Postdata = string.Format("localechange=&mode=number&svctype=1&smart_LEVEL=1&bvsd=&locale=zh-Hans_CN&url=http%3A%2F%2Fwww.naver.com&key={0}", userKey);
                item.Referer = "https://nid.naver.com/nidlogin.login?mode=number";
                item.ContentType = "application/x-www-form-urlencoded";
                item.Allowautoredirect = true;
                var result = http.GetHtml(item);
                //获取冒险岛登录参数,获取的到，则说明登录成功
                if (string.IsNullOrEmpty(result.Cookie))
                {
                    Log("Naver登录失败。");
                    return;
                }

                item = new HttpItem();
                item.URL = naverLogin;
                item.Cookie = result.Cookie;
                var naverGameResult = http.GetHtml(item);
                Log("成功登录到Naver，准备登录冒险岛。");

                this.webBrowser1.BeginInvoke(new Action(() => {

                    this.webBrowser1.IsWebBrowserContextMenuEnabled = false; // 禁用右键菜单  
                    this.webBrowser1.WebBrowserShortcutsEnabled = false; //禁用快捷键  
                    this.webBrowser1.AllowWebBrowserDrop = false; // 禁止文件拖动  

                    this.webBrowser1.Navigate(new Uri(mapleLogin));
                }));

                

                return;


                item = new HttpItem();
                item.URL = mapleLogin;
                item.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3";
                item.Cookie = naverGameResult.Cookie + "," + result.Cookie;
                item.ResultCookieType = ResultCookieType.CookieCollection;
                item.Allowautoredirect = true;
                var mapleResult = http.GetHtml(item);
                foreach (Cookie cookieItem in mapleResult.CookieCollection)
                {
                    if (cookieItem.Name.Equals("MSGENC", StringComparison.InvariantCultureIgnoreCase))
                    {
                        this.mapleSessionId = cookieItem.Value;
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(mapleSessionId))
                    Log("登录成功。");
                else
                    Log("无法获取冒险岛登录信息，请重新登录。" + mapleResult.Cookie);
            }));
            Login.Start();
        }

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);

        public CookieCollection GetAllCookiesFromHeader(string strHeader, string strHost)
        {
            ArrayList al = new ArrayList();
            CookieCollection cc = new CookieCollection();
            if (strHeader != string.Empty)
            {
                al = ConvertCookieHeaderToArrayList(strHeader);
                cc = ConvertCookieArraysToCookieCollection(al, strHost);
            }
            return cc;
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

            Log("正在加大马力启动游戏，请稍后。");
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            string str = string.Format("call {0} -run {1} {2} {3} {4}", maplepath, lepath, "", "", "");
            process.StandardInput.WriteLine(str + "&exit");
            process.StandardInput.AutoFlush = true;
            string text = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();
            Log("运行结束。");
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
        }

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
        }

        private static ArrayList ConvertCookieHeaderToArrayList(string strCookHeader)
        {
            strCookHeader = strCookHeader.Replace("\r", "");
            strCookHeader = strCookHeader.Replace("\n", "");
            string[] strCookTemp = strCookHeader.Split(',');
            ArrayList al = new ArrayList();
            int i = 0;
            int n = strCookTemp.Length;
            while (i < n)
            {
                if (strCookTemp[i].IndexOf("expires=", StringComparison.OrdinalIgnoreCase) > 0)
                {
                    al.Add(strCookTemp[i] + "," + strCookTemp[i + 1]);
                    i = i + 1;
                }
                else
                {
                    al.Add(strCookTemp[i]);
                }
                i = i + 1;
            }
            return al;
        }

        private static CookieCollection ConvertCookieArraysToCookieCollection(ArrayList al, string strHost)
        {
            CookieCollection cc = new CookieCollection();

            int alcount = al.Count;
            string strEachCook;
            string[] strEachCookParts;
            for (int i = 0; i < alcount; i++)
            {
                strEachCook = al[i].ToString();
                strEachCookParts = strEachCook.Split(';');
                int intEachCookPartsCount = strEachCookParts.Length;
                string strCNameAndCValue = string.Empty;
                string strPNameAndPValue = string.Empty;
                string strDNameAndDValue = string.Empty;
                string[] NameValuePairTemp;
                Cookie cookTemp = new Cookie();

                for (int j = 0; j < intEachCookPartsCount; j++)
                {
                    if (j == 0)
                    {
                        strCNameAndCValue = strEachCookParts[j];
                        if (strCNameAndCValue != string.Empty)
                        {
                            int firstEqual = strCNameAndCValue.IndexOf("=");
                            string firstName = strCNameAndCValue.Substring(0, firstEqual);
                            string allValue = strCNameAndCValue.Substring(firstEqual + 1, strCNameAndCValue.Length - (firstEqual + 1));
                            cookTemp.Name = firstName;
                            cookTemp.Value = allValue;
                        }
                        continue;
                    }
                    if (strEachCookParts[j].IndexOf("path", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        strPNameAndPValue = strEachCookParts[j];
                        if (strPNameAndPValue != string.Empty)
                        {
                            NameValuePairTemp = strPNameAndPValue.Split('=');
                            if (NameValuePairTemp[1] != string.Empty)
                            {
                                cookTemp.Path = NameValuePairTemp[1];
                            }
                            else
                            {
                                cookTemp.Path = "/";
                            }
                        }
                        continue;
                    }

                    if (strEachCookParts[j].IndexOf("domain", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        strPNameAndPValue = strEachCookParts[j];
                        if (strPNameAndPValue != string.Empty)
                        {
                            NameValuePairTemp = strPNameAndPValue.Split('=');

                            if (NameValuePairTemp[1] != string.Empty)
                            {
                                cookTemp.Domain = NameValuePairTemp[1];
                            }
                            else
                            {
                                cookTemp.Domain = strHost;
                            }
                        }
                        continue;
                    }
                }

                if (cookTemp.Path == string.Empty)
                {
                    cookTemp.Path = "/";
                }
                if (cookTemp.Domain == string.Empty)
                {
                    cookTemp.Domain = strHost;
                }
                cc.Add(cookTemp);
            }
            return cc;
        }
    }
}
