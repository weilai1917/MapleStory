using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyMaple
{
    public partial class OneKeyForm : Form
    {
        public OneKeyForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ((MainForm)this.Owner).userKey = this.textBox1.Text;
            this.Close();
        }
    }
}
