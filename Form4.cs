using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EasyMaple.Form3;

namespace EasyMaple
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private EasyMapleConfig easyconfig = new EasyMapleConfig();
        private void Button1_Click(object sender, EventArgs e)
        {
            string maplepath = this.textBox4.Text;
            string lepath = this.textBox3.Text;

            //记录下来，便于下次登录
            this.easyconfig.MaplePath = maplepath;
            this.easyconfig.LEPath = lepath;
            this.easyconfig.Save();
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.textBox4.Text = this.easyconfig.MaplePath;
            this.textBox3.Text = this.easyconfig.LEPath;
        }
    }
}
