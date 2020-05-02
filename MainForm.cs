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

        //private Task t_HeartBeat;

        public MainForm(EasyMapleConfig config)
        {
            InitializeComponent();
            this.MapleConfig = config;
            this.MapleCookie = new CookieContainer();
        }

        public void EnableStartBtn(bool enable)
        {
            this.BeginInvoke(new Action(() =>
            {
                this.BtnStartGame.Enabled = enable;
            }));
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
                    //设置异常，将信息委托给状态信息显示
                }
            }
            this.MapleConfig.EasyMaplePath = Application.StartupPath;
            var ngmPath = Util.GetRegistryValue(Registry.ClassesRoot, ConstStr.ngmRegPath, "");
            if (string.IsNullOrEmpty(ngmPath) || ngmPath.Split(' ').Length < 2)
            {
                //设置异常，将信心委托给状态信息显示
            }
            this.MapleConfig.NgmPath = ngmPath.Split(' ')[0].Replace("\"", ""); 
            if (string.IsNullOrEmpty(this.MapleConfig.DefaultNaverCookie))
            {
                Log($"请先添加默认账号。");
                this.BtnSetting_Click(null, null);
            }

            if (!string.IsNullOrWhiteSpace(this.MapleConfig.DefaultNaverCookie))
            {
                Task.Run(() =>
                {
                    this.Login().ConfigureAwait(true);
                });
            }

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

        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void BtnStartGame_Click(object sender, EventArgs e)
        {
            //启动游戏前校验所有参数是否合格。
            if (string.IsNullOrWhiteSpace(this.MapleConfig.MaplePath))
            {
                Log("请填写冒险岛启动路径。");
                this.BtnSetting_Click(null, null);
                return;
            }
            if (string.IsNullOrEmpty(this.MapleConfig.NgmPath))
            {
                Log("请先安装NGM。");
                System.Diagnostics.Process.Start("http://platform.nexon.com/NGM/Bin/Setup.exe");
                return;
            }
            if (string.IsNullOrEmpty(this.MapleEncPwd))
            {
                Log("请先登录冒险岛。");
                return;
            }
            Task.Run(() =>
            {
                this.Start().ConfigureAwait(true);
            });
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://weilai1917.github.io/milaisoft-maplestory/");
        }

        private void BtnSetting_Click(object sender, EventArgs e)
        {
            var defaultCookie = this.MapleConfig.DefaultNaverCookie;
            SettingForm form = new SettingForm(this.MapleConfig);
            form.ShowDialog();
            this.MapleConfig.Reload();
            if (!this.MapleConfig.DefaultNaverCookie.Equals(defaultCookie))
            {
                Log($"默认账号已改变，切换需重新登录");
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.notifyIcon1.Visible = false;
            System.Environment.Exit(0);
        }

        private void BtnStartWeb_Click(object sender, EventArgs e)
        {
            Process.Start("https://nid.naver.com/nidlogin.login?mode=number&url=https%3A%2F%2Fgame.naver.com%2Flogin.nhn%3FnxtUrl%3Dhttps%253A%252F%252Fmaplestory.nexon.game.naver.com%252FHome%252FMain");
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                this.Login().ConfigureAwait(true);
            });
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                this.Start().ConfigureAwait(true);
            });
        }

        #region 登录、启动、日志
        private async Task Login()
        {
            Log($"准备启动{this.MapleConfig.DefaultNaverNickName}...");
            var naverCookie = await NaverIdService.ReLoginNaver(this.MapleConfig.DefaultNaverCookie);
            if (string.IsNullOrEmpty(naverCookie))
            {
                Log($"{this.MapleConfig.DefaultNaverNickName}的Naver信息已失效，已删除默认。");
                this.MapleConfig.DefaultNaverCookie = "";
                this.MapleConfig.DefaultNaverNickName = "";
                this.MapleConfig.Save();
                return;
            }
            NaverIdService.ReLoadCookieContainer(naverCookie, ref this.MapleCookie);
            this.MapleEncPwd = await MapleIdService.LoginMaple(this.MapleCookie);
            if (string.IsNullOrEmpty(this.MapleEncPwd))
            {
                Log("冒险岛登陆失败，请查看帮助提示2-1.");
                return;
            }
            await MapleIdService.LoadMapleIds(this.MapleCookie);
            Log($" {this.MapleConfig.DefaultNaverNickName} 登录成功，愉快的冒险吧(●ˇ∀ˇ●)...");
            this.BeginInvoke(new Action(() =>
            {
                this.DefaultAccount.Text = this.MapleConfig.DefaultNaverNickName;
                this.BtnStartGame.Enabled = true;
            }));

        }

        private async Task Start()
        {
            await MapleIdService.UpdateMapleCookie(this.MapleCookie);
            this.MapleEncPwd = await MapleIdService.StartGame(this.MapleCookie, this.MapleConfig);
            if (string.IsNullOrEmpty(this.MapleEncPwd))
            {
                Log("冒险岛启动密钥获取失败，请重试。帮助提示2-2.");
                return;
            }
        }

        public void Log(string logTxt)
        {
            this.BeginInvoke(new Action(() =>
            {
                this.LblStatus.Text = logTxt;
            }));
        }

        //private void MapleIds_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        //{
        //    Log(string.Format("开始切换子号：{0}，请稍后。", e.ClickedItem.Text));
        //    MainWorker.QueueTask(() =>
        //    {
        //        HttpItem item = new HttpItem();
        //        item.URL = ConstStr.changeMapleId;
        //        item.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3";
        //        item.Method = "POST";
        //        item.Postdata = string.Format("id={0}&master=0&redirectTo=https%3A%2F%2Fmaplestory.nexon.game.naver.com%2FHome%2FMain", e.ClickedItem.Text);
        //        item.Referer = ConstStr.mapleHome;
        //        item.ContentType = "application/x-www-form-urlencoded";
        //        item.CookieContainer = MapleCookie;
        //        item.Header.Add("DNT", "1");
        //        item.Header.Add("Upgrade-Insecure-Requests", "1");
        //        var result = httph.GetHtml(item);
        //        Util.LogTxt(result.Html, this.MapleConfig.DeveloperMode);
        //        Log("子号切换成功，可以登录游戏。");
        //    });
        //}
        #endregion

        private void BtnStartGameT_Click(object sender, EventArgs e)
        {

        }
    }
}
