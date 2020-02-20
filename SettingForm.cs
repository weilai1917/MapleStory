using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.CkValidProgramName.DataBindings.Add("Checked", MapleConfig, "ValidProgramName", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void MapleBtn_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "请选择冒险岛文件夹";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var maplePath = dlg.SelectedPath.EndsWith("\\") ? dlg.SelectedPath : (dlg.SelectedPath + "\\");
                    this.TxtMaplePath.Text = maplePath;
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            this.MapleConfig.Save();
            this.Close();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            //修改注册表值
            RegistryKey RegistryRoot = Registry.LocalMachine;
            string[] path = new string[] { "SOFTWARE", "Wizet", "Maple" };
            string curPath = string.Empty;
            foreach (string p in path)
            {
                curPath = p;
                if (RegistryRoot != null && RegistryRoot.OpenSubKey(p, true) != null)
                    RegistryRoot = RegistryRoot.OpenSubKey(p, true);
            }
            if (RegistryRoot != null)
            {
                object value = RegistryRoot.GetValue("RootPath");
                if (Util.IsAdminRun())
                {
                    RegistryRoot.SetValue("RootPath", this.MapleConfig.MaplePath.TrimEnd('\\'));
                    MessageBox.Show("注册表目录已修复\n" + this.MapleConfig.MaplePath);
                }
            }
        }

        private void BtnWatchLog_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
        }

        private void TxtMaplePath_TextChanged(object sender, EventArgs e)
        {
            this.MapleConfig.Save();
        }

        private void CkDeveloperMode_CheckedChanged(object sender, EventArgs e)
        {
            this.MapleConfig.Save();
        }

        private void CkValidProgramName_CheckedChanged(object sender, EventArgs e)
        {
            this.MapleConfig.Save();
        }

        private void CkProxyIsOther_CheckedChanged(object sender, EventArgs e)
        {
            this.MapleConfig.Save();
        }

        private void CkKoreaSystem_CheckedChanged(object sender, EventArgs e)
        {
            this.MapleConfig.Save();
        }
    }
}
