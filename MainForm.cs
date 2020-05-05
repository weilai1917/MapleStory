using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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
        private string CurrIPAddress;

        public MainForm(EasyMapleConfig config)
        {
            InitializeComponent();
            this.MapleConfig = config;
            this.MapleCookie = new CookieContainer();
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
            this.BtnStartGame_Click(null, null);
        }

        private void BtnStartGameT_Click(object sender, EventArgs e)
        {

        }

        private async void MapleIds_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Log(string.Format("开始切换子号：{0}，请稍后。", e.ClickedItem.Text));
            await Task.Run(() =>
            {
                MapleIdService.ChangeMapleIds(this.MapleCookie, e.ClickedItem.Text, this.MapleConfig.DeveloperMode).ConfigureAwait(true);
                Log("子号切换成功，可以登录游戏。");
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
            this.MapleEncPwd = await MapleIdService.LoginMaple(this.MapleCookie, this.MapleConfig.DeveloperMode);
            if (string.IsNullOrEmpty(this.MapleEncPwd))
            {
                Log("冒险岛登陆失败，请查看帮助提示2-1.");
                return;
            }
            var mapleIds = await MapleIdService.LoadMapleIds(this.MapleCookie, this.MapleConfig.DeveloperMode);
            Log($" {this.MapleConfig.DefaultNaverNickName} 登录成功，愉快的冒险吧(●ˇ∀ˇ●)...");
            this.BeginInvoke(new Action(() =>
            {
                this.DefaultAccount.Text = this.MapleConfig.DefaultNaverNickName;
                this.BtnStartGame.Enabled = true;
                this.MapleIds.DropDownItems.Clear();
                foreach (var idItem in mapleIds)
                    this.MapleIds.DropDownItems.Add(idItem);
            }));
        }

        private async Task Start()
        {
            this.MapleConfig.MapleStartStatus = 0;
            this.MapleConfig.Save();
            var ip = await MapleIdService.UpdateMapleCookie(this.MapleCookie, this.MapleConfig.DeveloperMode);
            if (!this.CurrIPAddress.Equals(ip) && !string.IsNullOrEmpty(this.CurrIPAddress))
            {
                Log("检测到IP变动，1秒后重新登录冒险。");
                Thread.Sleep(1000);
                await this.Login();
                return;
            }
            this.CurrIPAddress = ip;

            this.MapleEncPwd = await MapleIdService.StartGame(this.MapleCookie, this.MapleConfig);
            if (string.IsNullOrEmpty(this.MapleEncPwd))
            {
                Log("冒险岛启动密钥获取失败，请重试。帮助提示2-2.");
                return;
            }
            Log("冒险岛启动成功，唤起游戏中...");
            await Task.Run(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (this.MapleConfig.MapleStartStatus == 0)
                {
                    this.MapleConfig.Reload();
                    switch (this.MapleConfig.MapleStartStatus)
                    {
                        case 1:
                            Log("冒险岛启动成功 (。・∀・)ノ");
                            break;
                        case -1:
                            Log("冒险岛启动失败，请检查网络配置。");
                            break;
                        case 2:
                            Log("冒险岛启动异常。帮助提示2-3.");
                            break;
                    }
                    Thread.Sleep(100);
                    if (sw.ElapsedMilliseconds >= 10000)
                    {
                        Log("未检测到游戏启动，请重新启动。");
                        sw.Stop();
                        break;
                    }
                }
                this.MapleConfig.MapleStartStatus = 0;
                this.MapleConfig.Save();
            });
        }

        public void Log(string logTxt)
        {
            this.BeginInvoke(new Action(() =>
            {
                this.LblStatus.Text = logTxt;
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.notifyIcon1.ShowBalloonTip(10, "", logTxt, ToolTipIcon.Info);
                }
            }));
        }

        #endregion

    }
}
