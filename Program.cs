using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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
            EasyMapleConfig config = new EasyMapleConfig();
            if (!config.ValidProgramName)
            {
                var fileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                if (!fileName.Contains(ConstStr.GameName))
                {
                    MessageBox.Show($"请修改文件名为{ConstStr.GameName}", "NGM限制");
                    return;
                }
            }
            string executePath = System.Windows.Forms.Application.ExecutablePath;
            Application.EnableVisualStyles();

            Util.LogTxt(Util.IsAdminRun() ? "AdminStart" : "Normal", config.DeveloperMode);
#if !Debug
            //判断当前登录用户是否为管理员
            if (!Util.IsAdminRun())
            {
                Util.ProcessStart(executePath, args);
                Environment.Exit(0);
                return;
            }
#endif
            if (args.Length > 0 && !string.IsNullOrEmpty(config.MaplePath))
            {
                var arg = string.Empty;
                arg = args == null
                          ? string.Empty
                          : args.Aggregate(arg, (current, s) => current + $" {s}");
                Util.LogTxt(arg, config.DeveloperMode);

                var applicationName = config.MaplePath + ConstStr.GameName;
                var commandLine = $"\"{applicationName}\" {arg}";
                var location = "ko-KR";
                var registries = RegistryEntriesLoader.GetRegistryEntries(false);

                var currentDirectory = Path.GetDirectoryName(applicationName);
                var ansiCodePage = (uint)CultureInfo.GetCultureInfo(location).TextInfo.ANSICodePage;
                var oemCodePage = (uint)CultureInfo.GetCultureInfo(location).TextInfo.OEMCodePage;
                var localeID = (uint)CultureInfo.GetCultureInfo(location).TextInfo.LCID;
                var defaultCharset = (uint)
                    GetCharsetFromANSICodepage(CultureInfo.GetCultureInfo(location)
                        .TextInfo.ANSICodePage);

                Util.LogTxt(commandLine, config.DeveloperMode);

                var l = new LoaderWrapper
                {
                    ApplicationName = applicationName,
                    CommandLine = commandLine,
                    CurrentDirectory = currentDirectory,
                    AnsiCodePage = ansiCodePage,
                    OemCodePage = oemCodePage,
                    LocaleID = localeID,
                    DefaultCharset = defaultCharset,
                    HookUILanguageAPI = 0,
                    Timezone = "Korea Standard Time",
                    NumberOfRegistryRedirectionEntries = registries?.Length ?? 0,
                    DebugMode = false
                };

                registries?.ToList()
                   .ForEach(
                       item =>
                           l.AddRegistryRedirectEntry(item.Root,
                               item.Key,
                               item.Name,
                               item.Type,
                               item.GetValue(CultureInfo.GetCultureInfo(location))));

                uint ret = l.Start();
                Util.LogTxt($"ret:{ret.ToString()}", config.DeveloperMode);

            }
            else
            {
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(config));
            }
        }

        private static int GetCharsetFromANSICodepage(int ansicp)
        {
            const int ANSI_CHARSET = 0;
            const int DEFAULT_CHARSET = 1;
            const int SYMBOL_CHARSET = 2;
            const int SHIFTJIS_CHARSET = 128;
            const int HANGEUL_CHARSET = 129;
            const int HANGUL_CHARSET = 129;
            const int GB2312_CHARSET = 134;
            const int CHINESEBIG5_CHARSET = 136;
            const int OEM_CHARSET = 255;
            const int JOHAB_CHARSET = 130;
            const int HEBREW_CHARSET = 177;
            const int ARABIC_CHARSET = 178;
            const int GREEK_CHARSET = 161;
            const int TURKISH_CHARSET = 162;
            const int VIETNAMESE_CHARSET = 163;
            const int THAI_CHARSET = 222;
            const int EASTEUROPE_CHARSET = 238;
            const int RUSSIAN_CHARSET = 204;
            const int MAC_CHARSET = 77;
            const int BALTIC_CHARSET = 186;

            var charset = ANSI_CHARSET;

            switch (ansicp)
            {
                case 932: // Japanese
                    charset = SHIFTJIS_CHARSET;
                    break;
                case 936: // Simplified Chinese
                    charset = GB2312_CHARSET;
                    break;
                case 949: // Korean
                    charset = HANGEUL_CHARSET;
                    break;
                case 950: // Traditional Chinese
                    charset = CHINESEBIG5_CHARSET;
                    break;
                case 1250: // Eastern Europe
                    charset = EASTEUROPE_CHARSET;
                    break;
                case 1251: // Russian
                    charset = RUSSIAN_CHARSET;
                    break;
                case 1252: // Western European Languages
                    charset = ANSI_CHARSET;
                    break;
                case 1253: // Greek
                    charset = GREEK_CHARSET;
                    break;
                case 1254: // Turkish
                    charset = TURKISH_CHARSET;
                    break;
                case 1255: // Hebrew
                    charset = HEBREW_CHARSET;
                    break;
                case 1256: // Arabic
                    charset = ARABIC_CHARSET;
                    break;
                case 1257: // Baltic
                    charset = BALTIC_CHARSET;
                    break;
            }

            return charset;
        }
    }
}
