using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Management;
using System.Windows.Forms;
using static EasyMaple.MainForm;

namespace EasyMaple
{
    public partial class SettingForm : Form
    {
        private EasyMapleConfig MapleConfig;

        public SettingForm(EasyMapleConfig config)
        {
            InitializeComponent();
            MapleConfig = config;
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            this.TxtMaplePath.DataBindings.Add("Text", MapleConfig, "MaplePath", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CkDeveloperMode.DataBindings.Add("Checked", MapleConfig, "DeveloperMode", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CkKoreaSystem.DataBindings.Add("Checked", MapleConfig, "KoreaSystem", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CkProxyIsOther.DataBindings.Add("Checked", MapleConfig, "ProxyIsOther", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CkAutoReLogin.DataBindings.Add("Checked", MapleConfig, "CkAutoReLogin", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CkTestWord.DataBindings.Add("Checked", MapleConfig, "CkTestWord", false, DataSourceUpdateMode.OnPropertyChanged);
            this.ReloadAccount();
        }

        private void MapleBtn_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "请选择冒险岛文件夹";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var maplePath = dlg.SelectedPath.EndsWith("\\") ? dlg.SelectedPath : (dlg.SelectedPath + "\\");
                    if (!File.Exists(maplePath + ConstStr.GameName))
                    {
                        MessageBox.Show("找不到游戏文件，请选择冒险岛游戏目录。");
                        return;
                    }
                    this.TxtMaplePath.Text = maplePath;
                }
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            //修改注册表值
            var maplePath = Util.GetRegistryValue(Registry.LocalMachine, ConstStr.mapleRegPath, ConstStr.mapleRegValueKey);
            if (!string.IsNullOrEmpty(maplePath))
            {
                try
                {
                    Util.SetRegistryValue(Registry.LocalMachine, ConstStr.mapleRegPath, ConstStr.mapleRegValueKey, this.MapleConfig.MaplePath.TrimEnd('\\'));
                    MessageBox.Show("注册表目录已修复\n" + this.MapleConfig.MaplePath);
                }
                catch (Exception)
                {
                    //设置异常，将信息委托给状态信息显示
                }
            }
        }

        private async void BtnAddNaverId_Click(object sender, EventArgs e)
        {
            var objectOneKey = new OneKeyForm.OneKeyObject();
            OneKeyForm form = new OneKeyForm(objectOneKey);
            form.ShowDialog();

            if (!string.IsNullOrEmpty(objectOneKey.OneKeyId))
            {
                this.tabControl1.Enabled = false;
                var naverCookie = await NaverIdService.LoginNaver(objectOneKey.OneKeyId);
                this.tabControl1.Enabled = true;

                if (string.IsNullOrEmpty(naverCookie))
                {
                    MessageBox.Show("Naver登录失败，请重试。");
                    return;
                }

                this.MapleConfig.DefaultNaverCookie = naverCookie;
                this.MapleConfig.DefaultNaverNickName = objectOneKey.NaverName;

                this.MapleConfig.LoginData.Add(new LoginData()
                {
                    Guid = Util.ConvertDateTimeToInt(DateTime.Now).ToString(),
                    IsDefault = this.MapleConfig.LoginData.Count <= 0,
                    AccountTag = objectOneKey.NaverName,
                    AccountCookieStr = naverCookie
                });
                this.ReloadAccount();
                this.MapleConfig.Save();
            }
        }

        private void BtnDelId_Click(object sender, EventArgs e)
        {
            if (this.dataAccountLst.Rows.Count <= 0)
            {
                return;
            }

            var guid = Convert.ToString(this.dataAccountLst.Rows[this.dataAccountLst.CurrentRow.Index].Cells[0].Value);
            var isdefault = Convert.ToBoolean(this.dataAccountLst.Rows[this.dataAccountLst.CurrentRow.Index].Cells[4].Value);
            if (isdefault)
            {
                this.MapleConfig.DefaultNaverCookie = "";
                this.MapleConfig.DefaultNaverNickName = "";
            }
            this.MapleConfig.LoginData.RemoveAll(x => x.Guid == guid);
            this.ReloadAccount();
            this.MapleConfig.Save();
        }

        private void dataAccountLst_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.RowIndex >= 0)
            {
                var selGuid = Convert.ToString(this.dataAccountLst.Rows[e.RowIndex].Cells[0].Value);

                foreach (var item in (List<LoginData>)this.dataAccountLst.DataSource)
                {
                    item.IsDefault = item.Guid == selGuid;

                    if (item.Guid == selGuid)
                    {
                        this.MapleConfig.DefaultNaverCookie = item.AccountCookieStr;
                        this.MapleConfig.DefaultNaverNickName = item.AccountTag;
                    }
                }

                this.MapleConfig.Save();
            }
        }

        private void dataAccountLst_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataAccountLst.IsCurrentCellDirty)
            {
                dataAccountLst.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void ReloadAccount()
        {
            if (this.MapleConfig.LoginData == null)
                this.MapleConfig.LoginData = new List<LoginData>();
            this.dataAccountLst.DataSource = null;
            this.dataAccountLst.DataSource = this.MapleConfig.LoginData;
            this.dataAccountLst.Update();
            this.dataAccountLst.Refresh();
        }

        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MapleConfig.Save();
        }
    }
}
