using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyMaple
{
    public class Util
    {
        public static string GetTimeStamp(System.DateTime time)
        {
            long ts = ConvertDateTimeToInt(time);
            return ts.ToString();
        }

        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }

        public static string GetCookie(string cookieName, CookieContainer cc)

        {
            List<Cookie> lstCookies = new List<Cookie>();
            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.Instance, null, cc, new object[] { });
            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                    | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c1 in colCookies) lstCookies.Add(c1);
            }

            var model = lstCookies.Find(p => p.Name == cookieName);
            if (model != null)
            {
                return model.Value;
            }
            return string.Empty;
        }

        public static void LogTxt(string strLog, bool dev = false)
        {
            if (!dev)
                return;
            string sFileName = Application.StartupPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            FileStream fs;
            StreamWriter sw;
            if (File.Exists(sFileName))
            //验证文件是否存在，有则追加，无则创建
            {
                fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            }
            else
            {
                fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            }
            sw = new StreamWriter(fs);
            sw.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss fff") + "]" + strLog);
            sw.Close();
            fs.Close();
        }

        public static bool IsAdminRun()
        {
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

        public static string ProcessStartByCmd(string InputTxt)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine(InputTxt + " &exit");
            process.StandardInput.AutoFlush = true;
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();
            return output;
        }

        public static void ProcessStart(string executable, string[] args, int show = 1)
        {
            var arg = string.Empty;
            arg = args == null
                      ? string.Empty
                      : args.Aggregate(arg, (current, s) => current + $" {s}");

            var shExecInfo = new SHELLEXECUTEINFO();

            shExecInfo.cbSize = Marshal.SizeOf(shExecInfo);

            shExecInfo.fMask = 0;
            shExecInfo.hwnd = IntPtr.Zero;
            shExecInfo.lpVerb = "runas";
            shExecInfo.lpFile = executable;
            shExecInfo.lpParameters = arg;
            shExecInfo.nShow = show;

            if (ShellExecuteEx(ref shExecInfo) == false)
            {
                throw new Exception("Error.\r\n" + $"Executable: {executable}\r\n"
                                    + $"Arguments: {arg}");
            }
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

        [StructLayout(LayoutKind.Sequential)]
        public struct SHELLEXECUTEINFO
        {
            public int cbSize;
            public uint fMask;
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.LPTStr)] public string lpVerb;
            [MarshalAs(UnmanagedType.LPTStr)] public string lpFile;
            [MarshalAs(UnmanagedType.LPTStr)] public string lpParameters;
            [MarshalAs(UnmanagedType.LPTStr)] public string lpDirectory;
            public int nShow;
            public IntPtr hInstApp;
            public IntPtr lpIDList;
            [MarshalAs(UnmanagedType.LPTStr)] public string lpClass;
            public IntPtr hkeyClass;
            public uint dwHotKey;
            public IntPtr hIcon;
            public IntPtr hProcess;
        }

        private static RegistryKey GetRegistryKey(RegistryKey startPath, string[] path)
        {
            RegistryKey RegistryRoot = startPath;
            foreach (string p in path)
            {
                if (RegistryRoot == null)
                    return null;

                var subKey = RegistryRoot.OpenSubKey(p, true);
                if (subKey == null)
                    return null;

                RegistryRoot = subKey;
            }

            return RegistryRoot;
        }

        public static string GetRegistryValue(RegistryKey startPath, string[] path, string getpath)
        {
            var root = GetRegistryKey(startPath, path);

            if (root == null)
                return string.Empty;

            return Convert.ToString(root.GetValue(getpath));


        }

        public static void SetRegistryValue(RegistryKey startPath, string[] path, string getpath, string setval)
        {
            var root = GetRegistryKey(startPath, path);

            if (root == null)
                return;

            root.SetValue(getpath, setval);
        }
    }
}
