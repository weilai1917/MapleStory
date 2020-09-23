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

        public MainForm(EasyMapleConfig config)
        {
            InitializeComponent();
            this.MapleConfig = config;
            this.MapleCookie = new CookieContainer();
        }

        private async void MainForm_Load(object sender, EventArgs e)
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
            this.MapleIds.Visible = false;
            this.LoadNaverIdsCb();
            if (!string.IsNullOrWhiteSpace(this.MapleConfig.DefaultNaverCookie) && !this.MapleConfig.CkNotAutoLogin)
            {
                await this.Login().ConfigureAwait(false);
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

        private async void BtnStartGame_Click(object sender, EventArgs e)
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
            if (this.MapleConfig.CkAutoReLogin)
            {
                this.MapleIds.Visible = false;
                await this.Login().ContinueWith(x => x.Result ? this.Start() : null).ConfigureAwait(false);
            }
            else
                await this.Start().ConfigureAwait(false);
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
            this.LoadNaverIdsCb();
            if (!this.MapleConfig.DefaultNaverCookie.Equals(defaultCookie))
            {
                this.MapleIds.Visible = false;
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
            this.MapleIds.Visible = false;
            this.Login().ConfigureAwait(false);
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            this.BtnStartGame_Click(null, null);
        }

        private async void MapleIds_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Log(string.Format("开始切换子号：{0}，请稍后。", e.ClickedItem.Text));
            await MapleIdService.ChangeMapleIds(this.MapleCookie, e.ClickedItem.Text, this.MapleConfig.DeveloperMode);
            Log($"{ e.ClickedItem.Text}切换成功，可以启动游戏。");
        }

        private async void NaverIds_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //e.ClickedItem.Tag
            this.MapleIds.Visible = false;
            await this.LoginBySelect(e.ClickedItem.Tag.ToString()).ConfigureAwait(false);
        }

        #region 登录、启动、日志
        private async Task<bool> Login()
        {
            return await Task.Run(new Func<bool>(() =>
            {
                Log($"准备启动{this.MapleConfig.DefaultNaverNickName}...");
                var naverCookie = NaverIdService.ReLoginNaver(this.MapleConfig.DefaultNaverCookie).Result;
                if (string.IsNullOrEmpty(naverCookie))
                {
                    Log($"{this.MapleConfig.DefaultNaverNickName}的登录失败。请查看帮助提示1-1.");
                    this.MapleConfig.DefaultNaverCookie = "";
                    this.MapleConfig.DefaultNaverNickName = "";
                    this.MapleConfig.Save();
                    return false;
                }
                NaverIdService.ReLoadCookieContainer(naverCookie, ref this.MapleCookie);
                this.MapleEncPwd = MapleIdService.LoginMaple(this.MapleCookie, this.MapleConfig.DeveloperMode).Result;
                if (string.IsNullOrEmpty(this.MapleEncPwd))
                {
                    Log("冒险岛登录失败，请查看帮助提示2-1.");
                    return false;
                }
                Log($" {this.MapleConfig.DefaultNaverNickName} 登录成功，愉快的冒险吧(●ˇ∀ˇ●)...");

                if (!this.MapleConfig.CkNotLoadMapleIds)
                {
                    var mapleIds = MapleIdService.LoadMapleIds(this.MapleCookie, this.MapleConfig.DeveloperMode).Result;
                    this.BeginInvoke(new Action(() =>
                    {
                        this.MapleIds.DropDownItems.Clear();
                        foreach (var idItem in mapleIds)
                            this.MapleIds.DropDownItems.Add(idItem);
                        this.MapleIds.Visible = true;
                    }));
                }

                this.BeginInvoke(new Action(() =>
                {
                    this.DefaultAccount.Text = this.MapleConfig.DefaultNaverNickName;
                    this.BtnStartGame.Enabled = true;
                }));
                return true;
            }));
        }

        private async Task<bool> LoginBySelect(string accountGuidId)
        {
            return await Task.Run(new Func<bool>(() =>
            {
                this.MapleConfig.LoginData.ForEach(x => x.IsDefault = false);
                var account = this.MapleConfig.LoginData.First(x => x.Guid == accountGuidId);
                account.IsDefault = true;
                this.MapleConfig.DefaultNaverCookie = account.AccountCookieStr;
                this.MapleConfig.DefaultNaverNickName = account.AccountTag;
                this.MapleConfig.Save();
                if (account == null)
                {
                    Log("出现了不可能的错误，账号指定错误...");
                    return false;
                }

                Log($"准备启动{account.AccountTag}...");
                var naverCookie = NaverIdService.ReLoginNaver(account.AccountCookieStr).Result;
                if (string.IsNullOrEmpty(naverCookie))
                {
                    Log($"{account.AccountTag}的登录失败。帮助提示1-1.");
                    return false;
                }
                NaverIdService.ReLoadCookieContainer(naverCookie, ref this.MapleCookie);
                this.MapleEncPwd = MapleIdService.LoginMaple(this.MapleCookie, this.MapleConfig.DeveloperMode).Result;
                if (string.IsNullOrEmpty(this.MapleEncPwd))
                {
                    Log("冒险岛登录失败，请查看帮助提示2-1.");
                    return false;
                }
                Log($" {account.AccountTag} 登录成功，愉快的冒险吧(●ˇ∀ˇ●)...");

                if (!this.MapleConfig.CkNotLoadMapleIds)
                {
                    var mapleIds = MapleIdService.LoadMapleIds(this.MapleCookie, this.MapleConfig.DeveloperMode).Result;
                    this.BeginInvoke(new Action(() =>
                    {
                        this.MapleIds.DropDownItems.Clear();
                        foreach (var idItem in mapleIds)
                            this.MapleIds.DropDownItems.Add(idItem);
                        this.MapleIds.Visible = true;
                    }));
                }

                this.BeginInvoke(new Action(() =>
                {
                    this.DefaultAccount.Text = this.MapleConfig.DefaultNaverNickName;
                    this.BtnStartGame.Enabled = true;
                }));
                return true;
            }));
        }

        private async Task Start()
        {
            await Task.Run(() =>
            {
                Log("开始启动冒险岛，请稍后...");
                this.MapleConfig.MapleStartStatus = 0;
                this.MapleConfig.Save();
                var ip = MapleIdService.UpdateMapleCookie(this.MapleCookie, this.MapleConfig.DeveloperMode).Result;
                this.MapleEncPwd = MapleIdService.StartGame(this.MapleCookie, this.MapleConfig).Result;
                if (string.IsNullOrEmpty(this.MapleEncPwd))
                {
                    Log("冒险岛启动密钥获取失败，请重试。帮助提示2-2.");
                    return;
                }
                Log("冒险岛启动成功，唤起游戏中...");

                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (this.MapleConfig.MapleStartStatus == 0)
                {
                    this.MapleConfig.Reload();
                    switch (this.MapleConfig.MapleStartStatus)
                    {
                        case 1:
                            Log("冒险岛游戏启动成功 (。・∀・)ノ");
                            break;
                        case -1:
                            Log("冒险岛启动失败，请查看帮助提示2-4.");
                            break;
                        case 2:
                            Log("冒险岛启动异常。帮助提示2-3.");
                            break;
                    }
                    Thread.Sleep(100);
                    if (sw.ElapsedMilliseconds >= 10000)
                    {
                        Log("未检测到游戏启动，可尝试重新启动。");
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

        private void LoadNaverIdsCb()
        {
            this.BeginInvoke(new Action(() =>
            {
                this.NaverIds.DropDownItems.Clear();
                foreach (var idItem in this.MapleConfig.LoginData)
                    this.NaverIds.DropDownItems.Add(
                        new ToolStripMenuItem(idItem.AccountTag) { Tag = idItem.Guid });
            }));
        }
        #endregion

    }
}
