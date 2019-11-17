using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;


namespace EasyMaple
{
    public class MapleFormBase : Form
    {
        public Context Context;
        public MapleFormBase() { }

        public MapleFormBase(Context ctx)
        {

            this.Context = ctx;
        }

        public void Log(RichTextBox control, string logTxt, string debugTxt = "")
        {
            if (control == null || this.Context.Config.DeveloperMode)
            {
                if (!string.IsNullOrEmpty(debugTxt))
                {
                    Util.LogTxt(logTxt);
                    Util.LogTxt(debugTxt);
                }
            }

            if (!string.IsNullOrEmpty(logTxt))
            {
                if (control != null)
                {
                    control.BeginInvoke(new Action(() =>
                    {
                        control.Text = control.Text.Insert(0, "[" + DateTime.Now.ToString("HH:mm:ss") + "]" + logTxt + "\n");
                    }));
                }
            }
        }
    }
}
