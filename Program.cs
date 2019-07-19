using System;
using System.Diagnostics;
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
            EasyMapleConfig easyconfig = new EasyMapleConfig();
            if (false)
            {
                var fileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                if (!fileName.Contains("MapleStory.exe"))
                {
                    MessageBox.Show("请修改文件名为MapleStory.exe", "NGM限制");
                    return;
                }
            }

            /**
             * 当前用户是管理员的时候，直接启动应用程序
             * 如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行
             */
            //获得当前登录的Windows用户标示
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            //创建Windows用户主题
            Application.EnableVisualStyles();

            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            //判断当前登录用户是否为管理员
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                //如果是管理员，则直接运行

                Application.EnableVisualStyles();
                if (args.Length > 0)
                {
                    Process process = new Process();
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();

                    string maplepath = easyconfig.MaplePath.EndsWith("\\") ? easyconfig.MaplePath : (easyconfig.MaplePath + "\\") + "MapleStory.exe";
                    string lepath = easyconfig.LEPath.EndsWith("\\") ? easyconfig.LEPath : (easyconfig.LEPath + "\\") + "LEProc.exe";

                    string inputTxt = string.Format("call \"{0}\" -run \"{1}\" ", lepath, maplepath);
                    if (easyconfig.KoreaSystem)
                    {
                        inputTxt = string.Format("start {0} ", maplepath);
                    }


                    if (easyconfig.DeveloperMode) MessageBox.Show(inputTxt);

                    if (args.Length > 0)
                    {
                        for (int i = 0; i < args.Length; i++)
                        {
                            inputTxt += args[i] + " ";
                        }
                    }
                    process.StandardInput.WriteLine(inputTxt + "&exit");
                    process.StandardInput.AutoFlush = true;
                    string output = process.StandardOutput.ReadToEnd();
                    if (easyconfig.DeveloperMode) MessageBox.Show(output);
                    process.WaitForExit();
                    process.Close();
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                }
            }
            else
            {
                //创建启动对象
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                //设置运行文件
                startInfo.FileName = System.Windows.Forms.Application.ExecutablePath;
                //设置启动参数
                startInfo.Arguments = String.Join(" ", args);
                //设置启动动作,确保以管理员身份运行
                startInfo.Verb = "runas";
                //如果不是管理员，则启动UAC
                System.Diagnostics.Process.Start(startInfo);
                //退出
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
