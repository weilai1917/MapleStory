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
            var maplePath = Util.GetRegistryValue(Registry.LocalMachine, ConstStr.mapleRegPath, ConstStr.mapleRegValueKey);
            if (string.IsNullOrEmpty(maplePath) || string.Compare(maplePath, Application.StartupPath, true) != 0)
            {
                try
                {
                    Util.SetRegistryValue(Registry.LocalMachine, ConstStr.mapleRegPath, ConstStr.mapleRegValueKey, Application.StartupPath);
                }
                catch (Exception)
                {
                    //设置异常，将信心委托给状态信息显示
                }
            }
            this.MapleConfig.EasyMaplePath = Application.StartupPath;
            var ngmPath = Util.GetRegistryValue(Registry.ClassesRoot, ConstStr.ngmRegPath, "");
            if (string.IsNullOrEmpty(ngmPath) || ngmPath.Split(' ').Length < 2)
            {
                //设置异常，将信心委托给状态信息显示
            }
            this.MapleConfig.NgmPath = ngmPath;


            

            MainWorker.QueueTask(() =>
            {
                if (!string.IsNullOrWhiteSpace(this.MapleConfig.DefaultNaverCookie))
                {
                    Log($"默认账号{this.MapleConfig.DefaultNaverNickName}，准备启动。");
                    //this.Login2Naver();
                    //this.Login2Maple();
                    //this.LoadMapleIds();
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
            Util.ProcessStartByCmd($"start {this.MapleConfig.NgmPath} {protocolUrl} ");
            Util.LogTxt(protocolUrl, this.MapleConfig.DeveloperMode);
            Util.LogTxt($"start {this.MapleConfig.NgmPath} {protocolUrl} ", this.MapleConfig.DeveloperMode);
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
            if (string.IsNullOrEmpty(this.MapleConfig.NgmPath))
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
                //this.UpdateMapleCookie();
                //this.StartGame();
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
                   // this.Login2Naver(objectOneKey.OneKeyId);
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
                   // this.LoadNaverIds();
                  //  this.Login2Maple();
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
                    //this.Login2Naver();
                    //this.Login2Maple();
                    //this.LoadMapleIds();
                }
            }, () => { ResetLoginBtn(); });
        }

        private void StartWebSite_Click(object sender, EventArgs e)
        {
            Process.Start("https://nid.naver.com/nidlogin.login?mode=number&url=https%3A%2F%2Fgame.naver.com%2Flogin.nhn%3FnxtUrl%3Dhttps%253A%252F%252Fmaplestory.nexon.game.naver.com%252FHome%252FMain");
        }

        public void Log(string logTxt)
        {
            //this.TxtLog.BeginInvoke(new Action(() =>
            //{
            //    string logAppend = DateTime.Now.ToString("[HH:mm:ss]: ") + logTxt + "\r\n";
            //    this.TxtLog.AppendText(logAppend);
            //    this.TxtLog.Select((this.TxtLog.Text.Length - logAppend.Length) + 1, logAppend.Length - 1);
            //    this.TxtLog.SelectionFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //    this.TxtLog.ScrollToCaret();
            //    this.TxtLog.SelectionStart = this.TxtLog.Text.Length;
            //    Util.LogTxt(logTxt, this.MapleConfig.DeveloperMode);
            //}));
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

        private void MainForm_Activated(object sender, EventArgs e)
        {

        }
    }
}
