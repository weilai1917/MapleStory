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
        OneKeyObject oneKeyObj;
        public OneKeyForm(OneKeyObject obj)
        {
            oneKeyObj = obj;
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            oneKeyObj.NaverName = this.textBox2.Text;
            oneKeyObj.OneKeyId = this.textBox1.Text;
            this.Close();
        }

        public class OneKeyObject
        {
            public string NaverName { get; set; }
            public string OneKeyId { get; set; }
        }
    }
}
