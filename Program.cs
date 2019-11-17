using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace EasyMaple
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Context ctx = new Context();
            if (!ctx.Config.ValidProgramName)
            {
                var fileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                if (!fileName.Contains("MapleStory.exe"))
                {
                    MessageBox.Show("请修改文件名为MapleStory.exe", "NGM限制");
                    return;
                }
            }
            string executePath = System.Windows.Forms.Application.ExecutablePath;
            Application.EnableVisualStyles();
            if (ctx.Config.DeveloperMode)
            {
                Util.LogTxt(Util.IsAdminRun() ? "AdminStart" : "Normal");
            }
#if !Debug
            //判断当前登录用户是否为管理员
            if (!Util.IsAdminRun())
            {
                Util.ProcessStart(executePath, args);
                Environment.Exit(0);
                return;
            }
#endif
            if (args.Length > 0 && !string.IsNullOrEmpty(ctx.Config.MaplePath))
            {
                executePath = string.Format("call \"{0}\" -run \"{1}\"", ctx.Config.LEPath, ctx.Config.MaplePath);
                var arg = string.Empty;
                arg = args == null
                          ? string.Empty
                          : args.Aggregate(arg, (current, s) => current + $" {s}");
                Util.ProcessStartByCmd(executePath + arg);

                if (ctx.Config.DeveloperMode)
                {
                    Util.LogTxt(executePath + arg);
                }

            }
            else
            {
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(ctx));
            }
        }
    }
}
