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
using System.Linq;

namespace EasyMaple
{
    public partial class MainForm : MapleFormBase
    {
        private bool alertOne = false;
        private string ngmPath = "";
        private HttpHelper httph = new HttpHelper();
        public MainForm(Context ctx)
            : base(ctx)
        {
            InitializeComponent();
            this.dataGridView1.Columns[1].ReadOnly = true;
        }

        public void LoadNaverIds()
        {
            this.dataGridView1.BeginInvoke(new Action(() =>
            {
                this.dataGridView1.DataSource = this.Context.Config.LoginData;
                this.dataGridView1.DataMember = null;
                this.dataGridView1.Update();
                this.dataGridView1.Refresh();

            }));
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
                    Log(this.richTextBox1, "Naver登录失败，请重试。" + result.StatusCode.ToString());
                    throw new Exception(result.StatusCode.ToString() + result.Html);
                }
                this.Context.NaverCookieStr = result.Cookie;
            }
            else
            {
                HttpItem item = new HttpItem();
                #region 将cookie塞到CookiContainer中
                item.URL = ConstStr.urlCheckLogin;
                item.Cookie = this.Context.NaverCookieStr;
                var result = httph.GetHtml(item);
                if (result.Html.Trim().IndexOf("NOLOGIN") > -1)
                {
                    this.Context.NaverCookieStr = "";
                    this.Context.Config.DefaultNaverCookie = "";
                    this.Context.Config.Save();
                    Log(this.richTextBox1, "Naver登录信息失效，请重新登录。" + result.StatusCode.ToString());
                    throw new Exception(result.StatusCode.ToString() + result.Html);
                }
                var array = this.Context.NaverCookieStr.Replace(" ", "").Replace("HttpOnly", "").Split(new string[] { ";," }, StringSplitOptions.RemoveEmptyEntries);
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
            this.Context.MapleCookie = _tempCookie;
            Log(this.richTextBox1, "Naver登录成功，准备登录到冒险岛。");
        }

        public void Login2Maple()
        {
            HttpItem item1 = new HttpItem()
            {
                URL = ConstStr.naverLogin,
                CookieContainer = this.Context.MapleCookie,
            };
            var naverGameResult = httph.GetHtml(item1);
            if (naverGameResult.Cookie == null || naverGameResult.Cookie.IndexOf("GDP_LOGIN") == -1 || naverGameResult.Cookie.IndexOf("PN_LOGIN") == -1)
            {
                Log(this.richTextBox1, "冒险岛登录失败，请重试。" + naverGameResult.StatusCode.ToString());
                throw new Exception(naverGameResult.StatusCode.ToString() + naverGameResult.Html);
            }

            HttpItem item2 = new HttpItem()
            {
                URL = ConstStr.mapleLogin,
                CookieContainer = this.Context.MapleCookie,
            };
            item2.Header.Add("DNT", "1");
            var mapleResult = httph.GetHtml(item2);
            if (mapleResult.Cookie == null || mapleResult.Cookie.IndexOf("ENC") == -1 || mapleResult.Cookie.IndexOf("NPP") == -1)
            {
                Log(this.richTextBox1, "冒险岛登录失败，请重试。" + mapleResult.StatusCode.ToString());
                throw new Exception(mapleResult.StatusCode.ToString() + mapleResult.Html);
            }

            HttpItem item3 = new HttpItem()
            {
                URL = ConstStr.mapleHome,
                Referer = ConstStr.mapleLogin,
                CookieContainer = this.Context.MapleCookie,
            };
            item3.Header.Add("DNT", "1");
            var homeResult = httph.GetHtml(item3);
            string encPwd = Util.GetCookie("MSGENCT", this.Context.MapleCookie);
            if (string.IsNullOrEmpty(encPwd))
            {
                Log(this.richTextBox1, "冒险岛登录失败，请重试。" + homeResult.StatusCode.ToString());
                throw new Exception(homeResult.StatusCode.ToString() + homeResult.Html);
            }
            this.Context.MapleEncPwd = encPwd;
            Log(this.richTextBox1, "冒险岛登录成功，愉快的游戏吧。");
        }

        public void UpdateMapleCookie()
        {
            Log(this.richTextBox1, "开始获取冒险岛密钥，请稍后。");
            HttpItem item1 = new HttpItem()
            {
                URL = ConstStr.updateCookie,
                CookieContainer = this.Context.MapleCookie
            };
            var update = httph.GetHtml(item1);
        }

        public void StartGame()
        {
            HttpItem item1 = new HttpItem()
            {
                URL = ConstStr.mapleStart,
                Method = "POST",
                Referer = ConstStr.mapleHome,
                ContentType = "application/x-www-form-urlencoded",
                CookieContainer = this.Context.MapleCookie
            };
            var result = httph.GetHtml(item1);
            string encPwd = Util.GetCookie("MSGENC", this.Context.MapleCookie);
            if (string.IsNullOrEmpty(encPwd))
            {
                Log(this.richTextBox1, "糟糕，启动密钥获取失败。重新登录吧。" + result.StatusCode.ToString());
                throw new Exception(result.StatusCode.ToString() + result.Html);
            }
            this.Context.MapleEncPwd = encPwd;

            Log(this.richTextBox1, "密钥获取成功，开始启动冒险岛。");

            string protocolUrl = "ngm://launch/%20" + HttpUtility.UrlEncode(string.Format(ConstStr.ngmArgument, encPwd, Util.GetTimeStamp(DateTime.Now.AddHours(1)))).Replace("%27", "'").Replace("+", "%20");
            if (this.Context.Config.ProxyIsOther)
            {
                this.webBrowser1.Navigate(protocolUrl);
            }
            else
            {
                Util.ProcessStartByCmd($"start {ngmPath} {protocolUrl} ");
            }
            Log(this.richTextBox1, "", protocolUrl);
            Log(this.richTextBox1, "", $"start {ngmPath} {protocolUrl} ");
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
            //Login.ContinueWith(t =>
            //{
            //    HttpItem item = new HttpItem();
            //    item.URL = ConstStr.IDList;
            //    item.Referer = ConstStr.mapleHome;
            //    item.ContentType = "";
            //    item.Header.Add("DNT", "1");
            //    item.Header.Add("X-Requested-With", "XMLHttpRequest");
            //    item.CookieContainer = mCookies;
            //    var idLists = httph.GetHtml(item);
            //    if (idLists.StatusCode == HttpStatusCode.OK)
            //    {
            //        var opHtml = idLists.Html;
            //        if (easyconfig.DeveloperMode) Log(opHtml);
            //        List<string> ids = new List<string>();
            //        //<ul> < li > < a href = "javascript:void(0)" > 274355068 </ a >  < input type = "radio" name = "login_id_sel" value="274355068" /> < > </ ul >
            //        foreach (var op in opHtml.Split(new string[] { "input" }, StringSplitOptions.None))
            //        {
            //            if (!op.Contains("value"))
            //                continue;
            //            string splitStr = op.Replace(" ", "");
            //            int indexvalue = splitStr.IndexOf("value=\"") + 7;
            //            int endindex = splitStr.IndexOf("\"", indexvalue);
            //            ids.Add(splitStr.Substring(indexvalue, endindex - indexvalue));
            //        }
            //        this.BeginInvoke(new Action(() =>
            //        {
            //            this.changeMapleId.DropDownItems.Clear();
            //            foreach (var idItem in ids)
            //            {
            //                this.changeMapleId.DropDownItems.Add(idItem);
            //            }
            //        }));
            //        Log("子号获取成功，可以快速切换Id。");
            //    }
            //    else
            //    {
            //        Log("冒险岛子号列表获取失败，不切换子号无需关注，不影响启动游戏。");
            //    }

            //}, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        private void BtnStartGame_Click(object sender, EventArgs e)
        {
            //启动游戏前校验所有参数是否合格。
            if (string.IsNullOrWhiteSpace(this.Context.Config.MaplePath)
                || (!this.Context.Config.KoreaSystem && string.IsNullOrWhiteSpace(this.Context.Config.LEPath)))
            {
                Log(this.richTextBox1, "请填写冒险岛路径或LE路径。");
                this.SettingBtn_Click(null, null);
                return;
            }
            if (string.IsNullOrEmpty(ngmPath))
            {
                Log(this.richTextBox1, "请安装NGM。http://platform.nexon.com/NGM/Bin/Setup.exe");
                System.Diagnostics.Process.Start("http://platform.nexon.com/NGM/Bin/Setup.exe");
                return;
            }
            if (string.IsNullOrEmpty(this.Context.MapleEncPwd))
            {
                Log(this.richTextBox1, "请先登录冒险岛。");
                return;
            }

            MainWorker.QueueTask(this.Context, () =>
            {
                this.UpdateMapleCookie();
                this.StartGame();
            });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MainWorker.QueueTask(this.Context, () =>
            {

                #region 冒险岛注册表路径调整

                //修改注册表值
                RegistryKey RegistryRoot = Registry.LocalMachine;
                string[] path = new string[] { "SOFTWARE", "Wizet", "Maple" };
                string curPath = string.Empty;
                foreach (string p in path)
                {
                    curPath = p;
                    if (RegistryRoot != null)
                        RegistryRoot = RegistryRoot.OpenSubKey(p, true);
                }
                if (RegistryRoot != null)
                {
                    object value = RegistryRoot.GetValue("RootPath");
                    Log(this.richTextBox1, "", "roothPath:" + Convert.ToString(value));
                    if (Util.IsAdminRun())
                    {
                        RegistryRoot.SetValue("RootPath", Environment.CurrentDirectory);
                    }
                    else
                    {
                        Log(this.richTextBox1, "冒险岛启动路径没有正常修改，请以管理员重新运行程序。");
                    }
                }
                else
                {
                    Log(this.richTextBox1, "未找到注册表信息，请正确安装游戏，或者尝试手动启动游戏一次。" + curPath);
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
                    Log(this.richTextBox1, "请安装NGM。http://platform.nexon.com/NGM/Bin/Setup.exe");
                    System.Diagnostics.Process.Start("http://platform.nexon.com/NGM/Bin/Setup.exe");
                }
                Log(this.richTextBox1, "", "ngmPath:" + ngmPath);
                #endregion

                if (!string.IsNullOrWhiteSpace(this.Context.Config.MaplePath) && !this.Context.Config.MaplePath.Contains("exe"))
                {
                    Log(this.richTextBox1, "冒险岛配置不正确，请选择到exe文件");
                }

                Random a = new Random();
                int randomA = a.Next(1, 100);
                if (randomA == 10 || randomA == 17)
                {
                    Log(this.richTextBox1, "12138，五十分之一的概率，中了就是缘分，帮助里赞助一波。");
                    ZanForm form = new ZanForm();
                    form.ShowDialog();
                }

            });
            this.dataGridView1.Enabled = false;
            MainWorker.QueueTask(this.Context, () =>
            {
                this.LoadNaverIds();
                if (!string.IsNullOrWhiteSpace(this.Context.NaverCookieStr))
                {
                    this.Login2Naver();
                    this.Login2Maple();
                }
            }, () => { this.dataGridView1.Enabled = true; });
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
            //Log(string.Format("开始切换子号：{0}，请稍后。", e.ClickedItem.Text));
            //var Change = new Task(new Action(() =>
            //{
            //    HttpItem item = new HttpItem();
            //    item.URL = ConstStr.changeMapleId;
            //    item.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3";
            //    item.Method = "POST";
            //    item.Postdata = string.Format("id={0}&master=0&redirectTo=https%3A%2F%2Fmaplestory.nexon.game.naver.com%2FHome%2FMain", e.ClickedItem.Text);
            //    item.Referer = ConstStr.mapleHome;
            //    item.ContentType = "application/x-www-form-urlencoded";
            //    item.CookieContainer = mCookies;
            //    item.Header.Add("DNT", "1");
            //    item.Header.Add("Upgrade-Insecure-Requests", "1");
            //    var result = httph.GetHtml(item);
            //    if (easyconfig.DeveloperMode) Log(result.Html);
            //    Log("子号切换成功，可以登录游戏。");
            //}));
            //Change.Start();
        }

        private readonly object lockobj = new object();

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 3 && !dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                if (Convert.ToBoolean((this.dataGridView1.Rows[e.RowIndex].Cells[3] as DataGridViewCheckBoxCell).Value))
                {
                    foreach (DataGridViewRow item in this.dataGridView1.Rows)
                    {
                        if (item.Index != e.RowIndex)
                        {
                            item.Cells[3].Value = false;
                        }
                    }
                    this.Context.ReLogin();
                    this.Context.NaverCookieStr = Convert.ToString(this.dataGridView1.Rows[e.RowIndex].Cells["AccountCookieStr"].Value);
                    this.Context.Config.DefaultNaverCookie = this.Context.NaverCookieStr;
                    this.Context.Config.Save();
                    Log(this.richTextBox1, "默认启动账号切换为：" + this.dataGridView1.Rows[e.RowIndex].Cells["AccountTag"].Value);
                    this.dataGridView1.Enabled = false;
                    MainWorker.QueueTask(this.Context, () =>
                    {
                        this.Login2Naver();
                        this.Login2Maple();
                    }, () => { this.dataGridView1.Enabled = true; });
                }
            }
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DeleteRow_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                var row = this.dataGridView1.SelectedRows[0].Index;
                var id = this.dataGridView1.SelectedRows[0].Cells["Guid"].Value.ToString();
                this.Context.Config.LoginData.RemoveAll(x => x.Guid == id);
                this.Context.Config.DefaultNaverCookie = this.Context.Config.LoginData.FirstOrDefault(x => x.IsDefault)?.AccountCookieStr;
                if (Convert.ToBoolean(this.dataGridView1.Rows[row].Cells[3].Value))
                {
                    this.Context.ReLogin();
                }
                this.Context.Config.Save();
                this.LoadNaverIds();
            }
        }

        private void SettingBtn_Click(object sender, EventArgs e)
        {
            SettingForm form = new SettingForm(this.Context);
            form.ShowDialog();
        }

        private void AddAcountBtn_Click(object sender, EventArgs e)
        {
            var objectOneKey = new OneKeyForm.OneKeyObject();
            OneKeyForm form = new OneKeyForm(objectOneKey);
            form.ShowDialog();

            if (!string.IsNullOrEmpty(objectOneKey.OneKeyId))
            {
                MainWorker.QueueTask(this.Context, () =>
                {
                    this.Login2Naver(objectOneKey.OneKeyId);
                    lock (lockobj)
                    {
                        bool isDefalt = false;
                        if (this.Context.Config.LoginData == null)
                        {
                            this.Context.Config.LoginData = new List<LoginData>();
                        }
                        string id = Util.ConvertDateTimeToInt(DateTime.Now).ToString();
                        this.Context.Config.LoginData.Add(new LoginData()
                        {
                            Guid = id,
                            IsDefault = isDefalt,
                            AccountTag = objectOneKey.NaverName,
                            AccountCookieStr = this.Context.NaverCookieStr
                        });
                        this.Context.Config.Save();
                    }
                    this.LoadNaverIds();
                });
            }
        }

        private void AddNaverId_ButtonClick(object sender, EventArgs e)
        {
            var objectOneKey = new OneKeyForm.OneKeyObject();
            OneKeyForm form = new OneKeyForm(objectOneKey);
            form.ShowDialog();

            if (!string.IsNullOrEmpty(objectOneKey.OneKeyId))
            {
                MainWorker.QueueTask(this.Context, () =>
                {
                    this.Login2Naver(objectOneKey.OneKeyId);
                    lock (lockobj)
                    {
                        bool isDefalt = false;
                        if (this.Context.Config.LoginData == null)
                        {
                            this.Context.Config.LoginData = new List<LoginData>();
                        }
                        string id = Util.ConvertDateTimeToInt(DateTime.Now).ToString();
                        this.Context.Config.LoginData.Add(new LoginData()
                        {
                            Guid = id,
                            IsDefault = isDefalt,
                            AccountTag = objectOneKey.NaverName,
                            AccountCookieStr = this.Context.NaverCookieStr
                        });
                        this.Context.Config.Save();
                    }
                    this.LoadNaverIds();
                });
            }
        }

        private void btnHelp_ButtonClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://weilai1917.github.io/milaisoft-maplestory/");
        }

        private void 网页启动游戏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://nid.naver.com/nidlogin.login?mode=number&url=https%3A%2F%2Fgame.naver.com%2Flogin.nhn%3FnxtUrl%3Dhttps%253A%252F%252Fmaplestory.nexon.game.naver.com%252FHome%252FMain");
        }

        private void LoginBtn_ButtonClick(object sender, EventArgs e)
        {
            this.dataGridView1.Enabled = false;
            MainWorker.QueueTask(this.Context, () =>
            {
                this.LoadNaverIds();
                if (!string.IsNullOrWhiteSpace(this.Context.NaverCookieStr))
                {
                    this.Login2Naver();
                    this.Login2Maple();
                }
            }, () => { this.dataGridView1.Enabled = true; });
        }
    }

}
