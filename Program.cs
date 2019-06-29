using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
                    EasyMaple.Form1.EasyMapleConfig easyconfig = new EasyMaple.Form1.EasyMapleConfig();
                    Process process = new Process();
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();

                    string maplepath = easyconfig.MaplePath;
                    string lepath = easyconfig.LEPath;

                    string inputTxt = string.Format("call {0} -run {1} ", lepath, maplepath);
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
                    process.WaitForExit();
                    process.Close();
                }
                else
                {
                    //var settings = new CefSettings()
                    //{
                    //    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.139 Safari/537.36",
                    //    CachePath = Directory.GetCurrentDirectory() + @"\Cache",
                    //    IgnoreCertificateErrors = true,
                    //};
                    //settings.CefCommandLineArgs.Add("disable-gpu", "1");
                    ////settings.CefCommandLineArgs.Add("", "");
                    //settings.CefCommandLineArgs.Add("disable-gpu-compositing", "1");
                    //settings.CefCommandLineArgs.Add("enable-begin-frame-scheduling", "1");
                    //settings.CefCommandLineArgs.Add("disable-gpu-vsync", "1"); //Disable Vsync
                    ////Disables the DirectWrite font rendering system on windows.
                    ////Possibly useful when experiencing blury fonts.
                    //settings.CefCommandLineArgs.Add("disable-direct-write", "1");
                    ////初始化配置
                    //Cef.Initialize(settings);



                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
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
