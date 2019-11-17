using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EasyMaple.MainForm;

namespace EasyMaple
{
    public partial class SettingForm : MapleFormBase
    {
        public SettingForm(Context ctx)
            : base(ctx)
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Context.Config.MaplePath = this.textBox4.Text;
            this.Context.Config.LEPath = this.textBox3.Text;
            this.Context.Config.DeveloperMode = this.checkBox1.Checked;
            this.Context.Config.ValidProgramName = this.checkBox2.Checked;
            this.Context.Config.KoreaSystem = this.checkBox3.Checked;
            this.Context.Config.ProxyIsOther = this.ProxyIsOther.Checked;
            this.Context.Config.Save();
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.textBox4.Text = this.Context.Config.MaplePath;
            this.textBox3.Text = this.Context.Config.LEPath;
            this.checkBox1.Checked = this.Context.Config.DeveloperMode;
            this.checkBox2.Checked = this.Context.Config.ValidProgramName;
            this.checkBox3.Checked = this.Context.Config.KoreaSystem;
            this.ProxyIsOther.Checked = this.Context.Config.ProxyIsOther;
        }

        private void MapleBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "请选择冒险岛MapleStory.exe文件...";
                dlg.Filter = "MapleStory.exe|*.exe";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.textBox4.Text = dlg.FileName;
                }
            }
        }

        private void LEBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "请选择LEProc.exe文件...";
                dlg.Filter = "LEProc.exe|*.exe";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.textBox3.Text = dlg.FileName;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.textBox4.Text))
            {
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
                    if (Util.IsAdminRun())
                    {
                        RegistryRoot.SetValue("RootPath", this.textBox4.Text);
                        MessageBox.Show("注册表目录已修复\n" + this.textBox4.Text);
                    }
                }
            }
        }
    }
}
