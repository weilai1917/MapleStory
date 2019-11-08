using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EasyMaple
{
    public static class Util
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

        public static void ProcessStart(string executable, string[] args)
        {
            var arg = string.Empty;
            arg = args == null
                      ? string.Empty
                      : args.Aggregate(arg, (current, s) => current + $" \"{s}\"");

            var shExecInfo = new SHELLEXECUTEINFO();

            shExecInfo.cbSize = Marshal.SizeOf(shExecInfo);

            shExecInfo.fMask = 0;
            shExecInfo.hwnd = IntPtr.Zero;
            shExecInfo.lpVerb = "runas";
            shExecInfo.lpFile = executable;
            shExecInfo.lpParameters = arg;
            shExecInfo.lpDirectory = Path.GetDirectoryName(executable);

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
    }
}
