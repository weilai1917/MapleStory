using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace EasyMaple
{
    public partial class MainForm : Form
    {
        private bool alertOne = false;
        private string ngmPath = "";

        private EasyMapleConfig MapleConfig;
        private CookieContainer MapleCookie;
        private string MapleEncPwd;
        private HttpHelper httph = new HttpHelper();

        public MainForm(EasyMapleConfig config)
        {
            InitializeComponent();
            this.MapleConfig = config;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            #region 冒险岛注册表路径调整

            //修改注册表值
            RegistryKey RegistryRoot = Registry.LocalMachine;
            string[] path = new string[] { "SOFTWARE", "Wizet", "Maple" };
            string curPath = string.Empty;
            foreach (string p in path)
            {
                curPath = p;
                if (RegistryRoot != null && RegistryRoot.OpenSubKey(p, true) != null)
                {
                    RegistryRoot = RegistryRoot.OpenSubKey(p, true);
                }
            }
            if (RegistryRoot != null)
            {
                object value = RegistryRoot.GetValue("RootPath");
                Util.LogTxt("roothPath:" + Convert.ToString(value), this.MapleConfig.DeveloperMode);
                if (Util.IsAdminRun())
                {
                    Util.LogTxt("注册表路径修改为:" + Application.StartupPath, this.MapleConfig.DeveloperMode);
                    RegistryRoot.SetValue("RootPath", Application.StartupPath);
                }
                else
                {
                    Log("冒险岛启动路径没有正常修改，请以管理员重新运行程序。");
                }
            }
            else
            {
                Log("未找到注册表信息，请正确安装游戏，如果无法正常启动，请先点击【帮助】下的【网页启动游戏】启动一次游戏即可。" + curPath);
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
                Log("请先安装NGM。下载完成，双击安装。如果无法正常启动，请先点击【帮助】下的【网页启动游戏】启动一次游戏即可。");
                System.Diagnostics.Process.Start("http://platform.nexon.com/NGM/Bin/Setup.exe");
            }
            Util.LogTxt("ngmPath:" + ngmPath, this.MapleConfig.DeveloperMode);
            #endregion

            this.LoginBtn.Enabled = false;
            this.MapleIds.Visible = false;
            this.LoadNaverIds();
            MainWorker.QueueTask(() =>
            {
                if (!string.IsNullOrWhiteSpace(this.MapleConfig.DefaultNaverCookie))
                {
                    Log($"默认账号{this.MapleConfig.DefaultNaverNickName}，准备启动。");
                    this.Login2Naver();
                    this.Login2Maple();
                    this.LoadMapleIds();
                }
            }, () => { ResetLoginBtn(); });
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

        #region 主要方法
        public void LoadNaverIds()
        {
            this.AccountList.DropDownItems.Clear();
            var count = this.MapleConfig.LoginData == null ? 0 : this.MapleConfig.LoginData.Count;
            this.AccountList.Text = $"账号列表({count})";

            if (this.MapleConfig.LoginData == null
                || this.MapleConfig.LoginData.Count <= 0)
                return;

            this.BeginInvoke(new Action(() =>
            {
                foreach (var item in this.MapleConfig.LoginData)
                {
                    var accountItem = new ToolStripMenuItem()
                    {
                        Name = item.Guid,
                        Text = item.AccountTag,
                        Tag = item.AccountCookieStr,
                        Checked = item.IsDefault,
                    };
                    accountItem.Click += AccountItem_Click;
                    this.AccountList.DropDownItems.Add(accountItem);
                }
                var dele = new ToolStripMenuItem()
                {
                    Name = "Del",
                    Text = "删除选中",
                };
                dele.Click += Dele_Click; ;
                this.AccountList.DropDownItems.Add(dele);
            }));
        }

        private void Dele_Click(object sender, EventArgs e)
        {
            this.MapleConfig.LoginData.RemoveAll(x => x.IsDefault);
            this.MapleConfig.DefaultNaverCookie = "";
            this.MapleConfig.DefaultNaverNickName = "";
            this.MapleConfig.Save();
            this.LoadNaverIds();
        }

        private void AccountItem_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            this.LoginBtn.Text = item.Text;
            this.MapleConfig.DefaultNaverCookie = Convert.ToString(item.Tag);
            this.MapleConfig.DefaultNaverNickName = Convert.ToString(item.Text);
            foreach (var accItem in this.MapleConfig.LoginData)
            {
                if (accItem.Guid == item.Name)
                    accItem.IsDefault = true;
                else
                    accItem.IsDefault = false;
            }
            this.MapleConfig.Save();
            this.LoadNaverIds();
        }

        public void Login2Naver(string oneCodeKey = "")
        {
            CookieContainer _tempCookie = new CookieContainer();
            if (!string.IsNullOrEmpty(oneCodeKey))
            {
                HttpItem item = new HttpItem()
                {
                    URL = ConstStr.urlLogin,
                    Method = "POST",
                    Postdata = ConstStr.loginParam + $"&key={oneCodeKey}",
                    Referer = ConstStr.urlLoginWithNumber,
                    ContentType = "application/x-www-form-urlencoded",
                    Allowautoredirect = true,
                    CookieContainer = _tempCookie,
                };
                var result = httph.GetHtml(item);
                if (result.CookieCollection == null || result.CookieCollection.Count <= 0)
                {
                    Log("Naver登录失败，请检查NaverId信息。");
                    Util.LogTxt(result.Html, this.MapleConfig.DeveloperMode);
                    return;
                }
                this.MapleConfig.DefaultNaverCookie = result.Cookie;
                this.MapleConfig.Save();
            }
            else
            {
                HttpItem item = new HttpItem();
                #region 将cookie塞到CookiContainer中
                item.URL = ConstStr.urlCheckLogin;
                item.Cookie = this.MapleConfig.DefaultNaverCookie;
                var result = httph.GetHtml(item);
                if (result.Html.Trim().IndexOf("NOLOGIN") > -1)
                {
                    this.MapleConfig.DefaultNaverCookie = "";
                    this.MapleConfig.Save();
                    Log("Naver登录信息失效，请重新登录。");
                    Util.LogTxt(result.Html, this.MapleConfig.DeveloperMode);
                    return;
                }
                var array = this.MapleConfig.DefaultNaverCookie.Replace(" ", "").Replace("HttpOnly", "").Split(new string[] { ";," }, StringSplitOptions.RemoveEmptyEntries);
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
                    _tempCookie.Add(cookieItem);
                }

                #endregion
            }
            this.MapleCookie = _tempCookie;
            Log("Naver登录成功，准备登录到冒险岛。");
        }

        public void Login2Maple()
        {
            HttpItem item1 = new HttpItem()
            {
                URL = ConstStr.naverLogin,
                CookieContainer = this.MapleCookie,
            };
            var naverGameResult = httph.GetHtml(item1);
            if (naverGameResult.Cookie == null || naverGameResult.Cookie.IndexOf("GDP_LOGIN") == -1 || naverGameResult.Cookie.IndexOf("PN_LOGIN") == -1)
            {
                Log("冒险岛登录失败，请重试。");
                Util.LogTxt(naverGameResult.Html, this.MapleConfig.DeveloperMode);
                return;
            }

            HttpItem item2 = new HttpItem()
            {
                URL = ConstStr.mapleLogin,
                CookieContainer = this.MapleCookie,
            };
            item2.Header.Add("DNT", "1");
            var mapleResult = httph.GetHtml(item2);
            if (mapleResult.Cookie == null || mapleResult.Cookie.IndexOf("ENC") == -1 || mapleResult.Cookie.IndexOf("NPP") == -1)
            {
                Log("冒险岛登录失败，请重试。");
                Util.LogTxt(mapleResult.Html, this.MapleConfig.DeveloperMode);
                return;
            }

            HttpItem item3 = new HttpItem()
            {
                URL = ConstStr.mapleHome,
                Referer = ConstStr.mapleLogin,
                CookieContainer = this.MapleCookie,
            };
            item3.Header.Add("DNT", "1");
            var homeResult = httph.GetHtml(item3);
            string encPwd = Util.GetCookie("MSGENCT", this.MapleCookie);
            if (string.IsNullOrEmpty(encPwd))
            {
                Log("冒险岛登录失败，请重试。");
                Util.LogTxt(homeResult.Html, this.MapleConfig.DeveloperMode);
                return;
            }
            this.MapleEncPwd = encPwd;
            Log("冒险岛登录成功，愉快的游戏吧。");
        }

        public void LoadMapleIds()
        {
            HttpItem item = new HttpItem();
            item.URL = ConstStr.IDList;
            item.Referer = ConstStr.mapleHome;
            item.ContentType = "";
            item.Header.Add("DNT", "1");
            item.Header.Add("X-Requested-With", "XMLHttpRequest");
            item.CookieContainer = MapleCookie;
            var idLists = httph.GetHtml(item);
            if (idLists.StatusCode == HttpStatusCode.OK)
            {
                var opHtml = idLists.Html;
                Util.LogTxt(opHtml, this.MapleConfig.DeveloperMode);
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
                    this.MapleIds.DropDownItems.Clear();
                    foreach (var idItem in ids)
                    {
                        this.MapleIds.DropDownItems.Add(idItem);
                    }
                    if (this.MapleIds.DropDownItems.Count > 0)
                    {
                        Log("子号获取成功，可以快速切换Id。");
                    }
                }));
            }
        }

        public void UpdateMapleCookie()
        {
            Log("开始获取冒险岛密钥，请稍后。");
            HttpItem item1 = new HttpItem()
            {
                URL = ConstStr.updateCookie,
                CookieContainer = this.MapleCookie
            };
            var update = httph.GetHtml(item1);
            Util.LogTxt(update.Html, this.MapleConfig.DeveloperMode);
        }

        public void StartGame()
        {
            HttpItem item1 = new HttpItem()
            {
                URL = ConstStr.mapleStart,
                Method = "POST",
                Referer = ConstStr.mapleHome,
                ContentType = "application/x-www-form-urlencoded",
                CookieContainer = this.MapleCookie
            };
            var result = httph.GetHtml(item1);
            string encPwd = Util.GetCookie("MSGENC", this.MapleCookie);
            if (string.IsNullOrEmpty(encPwd))
            {
                Log("糟糕，启动密钥获取失败。重新登录吧。");
                Util.LogTxt(result.Html, this.MapleConfig.DeveloperMode);
                return;
            }
            this.MapleEncPwd = encPwd;

            Log("密钥获取成功，开始启动NGM唤起冒险岛。");

            string protocolUrl = "ngm://launch/%20" + HttpUtility.UrlEncode(string.Format(ConstStr.ngmArgument, encPwd, Util.GetTimeStamp(DateTime.Now.AddHours(1)))).Replace("%27", "'").Replace("+", "%20");
            Util.ProcessStartByCmd($"start {ngmPath} {protocolUrl} ");
            Util.LogTxt(protocolUrl, this.MapleConfig.DeveloperMode);
            Util.LogTxt($"start {ngmPath} {protocolUrl} ", this.MapleConfig.DeveloperMode);
        }
        #endregion

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

        private void BtnStartGame_Click(object sender, EventArgs e)
        {
            //启动游戏前校验所有参数是否合格。
            if (string.IsNullOrWhiteSpace(this.MapleConfig.MaplePath))
            {
                Log("请填写冒险岛启动路径。");
                this.SettingBtn_Click(null, null);
                return;
            }
            if (string.IsNullOrEmpty(ngmPath))
            {
                Log("请先安装NGM。下载地址：http://platform.nexon.com/NGM/Bin/Setup.exe，下载完成，双击安装。如果无法正常启动，请先点击【帮助】下的【网页启动游戏】启动一次游戏即可。");
                System.Diagnostics.Process.Start("http://platform.nexon.com/NGM/Bin/Setup.exe");
                return;
            }
            if (string.IsNullOrEmpty(this.MapleEncPwd))
            {
                Log("请先登录冒险岛。");
                return;
            }

            MainWorker.QueueTask(() =>
            {
                this.UpdateMapleCookie();
                this.StartGame();
            });
        }

        private readonly object lockobj = new object();

        private void SettingBtn_Click(object sender, EventArgs e)
        {
            SettingForm form = new SettingForm(this.MapleConfig);
            form.ShowDialog();
        }

        private void AddAcountBtn_Click(object sender, EventArgs e)
        {
            var objectOneKey = new OneKeyForm.OneKeyObject();
            OneKeyForm form = new OneKeyForm(objectOneKey);
            form.ShowDialog();

            if (!string.IsNullOrEmpty(objectOneKey.OneKeyId))
            {
                this.LoginBtn.Enabled = false;
                this.MapleIds.Visible = false;
                MainWorker.QueueTask(() =>
                {
                    this.Login2Naver(objectOneKey.OneKeyId);
                    if (!string.IsNullOrEmpty(this.MapleConfig.DefaultNaverCookie))
                    {
                        lock (lockobj)
                        {
                            bool isDefalt = false;
                            if (this.MapleConfig.LoginData == null)
                            {
                                isDefalt = true;
                                this.MapleConfig.LoginData = new List<LoginData>();
                            }

                            this.MapleConfig.LoginData.Add(new LoginData()
                            {
                                Guid = Util.ConvertDateTimeToInt(DateTime.Now).ToString(),
                                IsDefault = isDefalt,
                                AccountTag = objectOneKey.NaverName,
                                AccountCookieStr = this.MapleConfig.DefaultNaverCookie
                            });
                            this.MapleConfig.DefaultNaverNickName = objectOneKey.NaverName;
                            this.MapleConfig.Save();
                        }
                    }
                    this.LoadNaverIds();
                    this.Login2Maple();
                }, () => { ResetLoginBtn(); });
            }
        }

        private void btnHelp_ButtonClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://weilai1917.github.io/milaisoft-maplestory/");
        }

        private void LoginBtn_ButtonClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MapleConfig.DefaultNaverCookie))
            {
                this.AddAcountBtn_Click(null, null);
                return;
            }
            this.LoginBtn.Enabled = false;
            this.MapleIds.Visible = false;
            MainWorker.QueueTask(() =>
            {
                if (!string.IsNullOrWhiteSpace(MapleConfig.DefaultNaverCookie))
                {
                    this.Login2Naver();
                    this.Login2Maple();
                    this.LoadMapleIds();
                }
            }, () => { ResetLoginBtn(); });
        }

        private void StartWebSite_Click(object sender, EventArgs e)
        {
            Process.Start("https://nid.naver.com/nidlogin.login?mode=number&url=https%3A%2F%2Fgame.naver.com%2Flogin.nhn%3FnxtUrl%3Dhttps%253A%252F%252Fmaplestory.nexon.game.naver.com%252FHome%252FMain");
        }

        public void Log(string logTxt)
        {
            this.TxtLog.BeginInvoke(new Action(() =>
            {
                string logAppend = DateTime.Now.ToString("[HH:mm:ss]: ") + logTxt + "\r\n";
                this.TxtLog.AppendText(logAppend);
                this.TxtLog.Select((this.TxtLog.Text.Length - logAppend.Length) + 1, logAppend.Length - 1);
                this.TxtLog.SelectionFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.TxtLog.ScrollToCaret();
                this.TxtLog.SelectionStart = this.TxtLog.Text.Length;
                Util.LogTxt(logTxt, this.MapleConfig.DeveloperMode);
            }));
        }

        public void ResetLoginBtn()
        {
            this.BeginInvoke(new Action(() =>
            {
                this.MapleIds.Visible = true;
                this.LoginBtn.Enabled = true;
                if (!string.IsNullOrEmpty(this.MapleConfig.DefaultNaverNickName))
                {
                    this.LoginBtn.Text = this.MapleConfig.DefaultNaverNickName;
                }
            }));
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Process.Start("https://weilai1917.github.io/milaisoft-maplestory/");
        }

        private void MapleIds_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Log(string.Format("开始切换子号：{0}，请稍后。", e.ClickedItem.Text));
            MainWorker.QueueTask(() =>
            {
                HttpItem item = new HttpItem();
                item.URL = ConstStr.changeMapleId;
                item.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3";
                item.Method = "POST";
                item.Postdata = string.Format("id={0}&master=0&redirectTo=https%3A%2F%2Fmaplestory.nexon.game.naver.com%2FHome%2FMain", e.ClickedItem.Text);
                item.Referer = ConstStr.mapleHome;
                item.ContentType = "application/x-www-form-urlencoded";
                item.CookieContainer = MapleCookie;
                item.Header.Add("DNT", "1");
                item.Header.Add("Upgrade-Insecure-Requests", "1");
                var result = httph.GetHtml(item);
                Util.LogTxt(result.Html, this.MapleConfig.DeveloperMode);
                Log("子号切换成功，可以登录游戏。");
            });
        }
    }
}
