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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private EasyMapleConfig easyconfig = new EasyMapleConfig();
        private HttpHelper httph = new HttpHelper();
        private CookieContainer mCookies = new CookieContainer();

        private bool alertOne = false;
        private string ngmPath = string.Empty;
        private string encPwd = string.Empty;
        private string naverStr = string.Empty;
        public string userKey = string.Empty;

        private void Log(string logdata)
        {
            this.richTextBox1.BeginInvoke(new Action(() =>
            {
                //yyyy-MM-dd hh:mm:ss fff
                this.richTextBox1.Text = this.richTextBox1.Text.Insert(0, "[" + DateTime.Now.ToString("HH:mm:ss") + "]" + logdata + "\n");
            }));
        }

        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.notifyIcon1.Visible = false;
            System.Environment.Exit(0);
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(easyconfig.NaverCookie))
            {
                OneKeyForm form = new OneKeyForm();
                form.ShowDialog(this);
                if (!string.IsNullOrWhiteSpace(userKey))
                {
                    Log(string.Format("密钥获取成功\"{0}\"，准备登录。", userKey));
                }
                else
                {
                    Log("请输入一次性密钥！");
                    return;
                }
            }
            var Login = new Task(() =>
            {
                mCookies = new CookieContainer();
                if (easyconfig.DeveloperMode) Log(string.Format("Cookie已重置{0}", mCookies.Count));
                HttpItem item = new HttpItem();
                if (string.IsNullOrWhiteSpace(easyconfig.NaverCookie))
                {
                    item.URL = ConstStr.urlLogin;
                    item.Method = "POST";
                    item.Postdata = string.Format("localechange=&mode=number&svctype=1&smart_LEVEL=1&bvsd=&locale=zh-Hans_CN&url=http%3A%2F%2Fwww.naver.com&key={0}&nvlong=on", userKey);
                    item.Referer = "https://nid.naver.com/nidlogin.login?mode=number";
                    item.ContentType = "application/x-www-form-urlencoded";
                    item.Allowautoredirect = true;
                    item.CookieContainer = mCookies;
                    var result = httph.GetHtml(item);
                    naverStr = result.Cookie;
                    if (easyconfig.DeveloperMode) Log(result.Html);
                    //获取冒险岛登录参数,获取的到，则说明登录成功
                    if (result.CookieCollection == null || result.CookieCollection.Count <= 0)
                    {
                        Log("Naver登录失败，请重试。");
                        throw new Exception();
                    }
                }
                else
                {
                    item.URL = ConstStr.urlCheckLogin;
                    item.Cookie = naverStr;
                    var result = httph.GetHtml(item);
                    if (easyconfig.DeveloperMode) Log(result.Html);
                    if (result.Html.Trim().IndexOf("NOLOGIN") > -1)
                    {
                        Log("Naver登录信息失效，请重新登录。");
                        easyconfig.NaverCookie = "";
                        easyconfig.Save();
                        throw new Exception();
                    }

                    #region 将cookie塞到CookiContainer中

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

                    #endregion

                    if (easyconfig.DeveloperMode) Log(string.Format("Cookie已记录{0}", mCookies.Count));
                }

                Log("Naver登录成功，开始进入Naver Game。");
                if (easyconfig.DeveloperMode) Log("Naver信息已保存，重新登录无需输入一次性密钥。请直接点击登录。");
                easyconfig.NaverCookie = naverStr;
                easyconfig.Save();

                #region Naver Game

                item = new HttpItem();
                item.URL = ConstStr.naverLogin;
                item.CookieContainer = mCookies;
                var naverGameResult = httph.GetHtml(item);
                if (easyconfig.DeveloperMode) Log(naverGameResult.Html);
                if (naverGameResult.Cookie == null || naverGameResult.Cookie.IndexOf("GDP_LOGIN") == -1 || naverGameResult.Cookie.IndexOf("PN_LOGIN") == -1)
                {
                    Log("Naver Game登录失败，请重试。");
                    throw new Exception();
                }
                #endregion

                #region Nexon Game

                Log("Naver Game登录成功，开始登录冒险岛。");
                item = new HttpItem();
                item.URL = ConstStr.mapleLogin;
                item.CookieContainer = mCookies;
                item.Header.Add("DNT", "1");
                var mapleResult = httph.GetHtml(item);
                if (easyconfig.DeveloperMode) Log(mapleResult.Html);
                if (mapleResult.Cookie == null || mapleResult.Cookie.IndexOf("ENC") == -1 || mapleResult.Cookie.IndexOf("NPP") == -1)
                {
                    Log("Nexon Game登录失败，请重试。");
                    throw new Exception();
                }

                #endregion

                #region MapleStory

                item = new HttpItem();
                item.URL = ConstStr.mapleHome;
                item.Referer = ConstStr.mapleLogin;
                item.CookieContainer = mCookies;
                item.Header.Add("DNT", "1");
                var homeResult = httph.GetHtml(item);
                if (easyconfig.DeveloperMode) Log(homeResult.Html);
                encPwd = Util.GetCookie("MSGENCT", mCookies);
                if (string.IsNullOrEmpty(encPwd))
                {
                    Log("冒险岛登录失败，请重试。");
                    throw new Exception();
                }
                Log("冒险岛登录成功！");

                #endregion

            });
            Login.Start();
            Login.ContinueWith(t =>
            {
                HttpItem item = new HttpItem();
                item.URL = ConstStr.IDList;
                item.Referer = ConstStr.mapleHome;
                item.ContentType = "";
                item.Header.Add("DNT", "1");
                item.Header.Add("X-Requested-With", "XMLHttpRequest");
                item.CookieContainer = mCookies;
                var idLists = httph.GetHtml(item);
                if (idLists.StatusCode == HttpStatusCode.OK)
                {
                    var opHtml = idLists.Html;
                    if (easyconfig.DeveloperMode) Log(opHtml);
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
                    this.BeginInvoke(new Action(() =>
                    {
                        this.changeMapleId.DropDownItems.Clear();
                        foreach (var idItem in ids)
                        {
                            this.changeMapleId.DropDownItems.Add(idItem);
                        }
                    }));
                    Log("子号获取成功，可以快速切换Id。");
                }
                else
                {
                    Log("冒险岛子号列表获取失败，不切换子号无需关注，不影响启动游戏。");
                }

            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        private void BtnSetting_Click(object sender, EventArgs e)
        {
            SettingForm form = new SettingForm();
            form.ShowDialog();

            easyconfig = new EasyMapleConfig();
        }

        private void BtnStartGame_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        public void StartGame()
        {
            bool isShow = this.WindowState == FormWindowState.Normal;
            if (string.IsNullOrWhiteSpace(this.easyconfig.MaplePath) || string.IsNullOrWhiteSpace(this.easyconfig.LEPath))
            {
                Log("请填写冒险岛路径或LE路径。");
                if (!isShow) this.notifyIcon1.ShowBalloonTip(30, "注意", "请填写冒险岛路径或LE路径。", ToolTipIcon.Error);
                SettingForm form = new SettingForm();
                form.ShowDialog();
                return;
            }
            if (string.IsNullOrEmpty(ngmPath))
            {
                Log("请安装NGM。http://platform.nexon.com/NGM/Bin/Setup.exe");
                if (!isShow) this.notifyIcon1.ShowBalloonTip(30, "注意", "请安装NGM。http://platform.nexon.com/NGM/Bin/Setup.exe", ToolTipIcon.Error);
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
                Log("开始请求游戏启动密钥。");
                HttpItem item = new HttpItem();
                item.URL = ConstStr.updateCookie;
                item.CookieContainer = mCookies;
                var update = httph.GetHtml(item);

                item = new HttpItem();
                item.URL = ConstStr.mapleStart;
                item.Method = "POST";
                item.Referer = ConstStr.mapleHome;
                item.ContentType = "application/x-www-form-urlencoded";
                item.CookieContainer = mCookies;
                var result = httph.GetHtml(item);
                if (easyconfig.DeveloperMode) Log(encPwd + mCookies.Count.ToString());
                encPwd = string.Empty;
                encPwd = Util.GetCookie("MSGENC", mCookies);
                if (string.IsNullOrEmpty(encPwd))
                {
                    Log("糟糕，启动密钥获取失败。重新登录吧。");
                    if (!isShow) this.notifyIcon1.ShowBalloonTip(30, "注意", "糟糕，启动密钥获取失败。重新登录吧。", ToolTipIcon.Error);
                    return;
                }
                string argument = string.Format("-dll:platform.nexon.com/NGM/Bin/NGMDll.dll:1 -locale:KR -mode:launch -game:589825:0 -token:'{0}:3' -passarg:'WebStart' -timestamp:{1} -position:'GameWeb|https://maplestory.nexon.game.naver.com/Home/Main' -service:6 -architectureplatform:'none'", encPwd, Util.GetTimeStamp(DateTime.Now.AddHours(1)));
                string protocolUrl = "ngm://launch/%20" + HttpUtility.UrlEncode(argument).Replace("%27", "'").Replace("+", "%20");

                if (easyconfig.DeveloperMode) Log(argument);
                if (easyconfig.DeveloperMode) Log(protocolUrl);
                if (easyconfig.DeveloperMode) Log(string.Format("call \"{0}\" -run \"{1}\" ", easyconfig.LEPath, easyconfig.MaplePath));
                if (this.easyconfig.ProxyIsOther)
                {
                    if (easyconfig.DeveloperMode) Log("使用内置IE启动游戏");
                    this.webBrowser1.Navigate(protocolUrl);
                }
                else
                {
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
                }
                Log("密钥获取成功，开始启动冒险岛。");
                if (!isShow) this.notifyIcon1.ShowBalloonTip(30, "注意", "密钥获取成功，开始启动冒险岛。", ToolTipIcon.Info);
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }));
            Login.Start();
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            HelpForm form = new HelpForm();
            form.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            naverStr = easyconfig.NaverCookie;

            #region 冒险岛注册表路径调整

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
                else
                {
                    Log("冒险岛启动路径没有正常修改，请以管理员重新运行程序。");
                }
            }
            else
            {
                Log("未找到冒险岛目录，请安装游戏。");
                return;
            }

            #endregion

            #region NGM路径判断

            bool isInstallNGM = true;
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
                        isInstallNGM = false;

                }
                catch
                {
                    isInstallNGM = false;
                }
            }
            else
                isInstallNGM = false;


            if (!isInstallNGM)
            {
                Log("未找到NGM，请先安装。http://platform.nexon.com/NGM/Bin/Setup.exe");
                return;
            }

            #endregion

            Log("欢迎使用Naver账号快捷登录，有疑问请先点击帮助按钮。\n 程序名称必须是MapleStory.exe，不要把软件放在游戏目录。\n 软件交流群：908378560");

            #region 登录

            if (!string.IsNullOrEmpty(naverStr))
            {
                this.BtnLogin_Click(null, null);
            }

            #endregion

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void MainForm_MinimumSizeChanged(object sender, EventArgs e)
        {
            this.Hide();
            this.notifyIcon1.ShowBalloonTip(30, "注意", "程序已最小化，右键可以快速启动游戏。", ToolTipIcon.Info);
        }

        private void ChangeMapleId_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Log(string.Format("开始切换子号：{0}，请稍后。", e.ClickedItem.Text));
            var Change = new Task(new Action(() =>
            {
                HttpItem item = new HttpItem();
                item.URL = ConstStr.changeMapleId;
                item.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3";
                item.Method = "POST";
                item.Postdata = string.Format("id={0}&master=0&redirectTo=https%3A%2F%2Fmaplestory.nexon.game.naver.com%2FHome%2FMain", e.ClickedItem.Text);
                item.Referer = ConstStr.mapleHome;
                item.ContentType = "application/x-www-form-urlencoded";
                item.CookieContainer = mCookies;
                item.Header.Add("DNT", "1");
                item.Header.Add("Upgrade-Insecure-Requests", "1");
                var result = httph.GetHtml(item);
                if (easyconfig.DeveloperMode) Log(result.Html);
                Log("子号切换成功，可以登录游戏。");
            }));
            Change.Start();
        }

        private void ResetLogin_Click(object sender, EventArgs e)
        {
            easyconfig.NaverCookie = "";
            easyconfig.Save();
            encPwd = string.Empty;
            userKey = string.Empty;
            mCookies = new CookieContainer();
            Log("登录内容已经重置");
        }
    }

}
