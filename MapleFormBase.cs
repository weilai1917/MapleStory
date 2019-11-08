using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyMaple
{
    public class MapleFormBase : Form
    {
        public Context Context;
        public MapleFormBase() { }

        public void SetContext(Context ctx)
        {
            this.Context = ctx;
        }

        public void Log(RichTextBox control, string logTxt)
        {
            control.BeginInvoke(new Action(() =>
            {
                control.Text = control.Text.Insert(0, "[" + DateTime.Now.ToString("HH:mm:ss") + "]" + logTxt + "\n");
            }));
        }
    }
}
