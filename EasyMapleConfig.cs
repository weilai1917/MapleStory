using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMaple
{
    public sealed class EasyMapleConfig : ApplicationSettingsBase
    {
        /// <summary>
        /// 冒险岛路径
        /// </summary>
        [UserScopedSetting]
        public string MaplePath
        {
            get { return Convert.ToString(this["MaplePath"]); }
            set { this["MaplePath"] = value; }
        }

        /// <summary>
        /// LE路径
        /// </summary>
        [UserScopedSetting]
        public string LEPath
        {
            get { return Convert.ToString(this["LEPath"]); }
            set { this["LEPath"] = value; }
        }

        /// <summary>
        /// NaverCookie
        /// </summary>
        [UserScopedSetting]
        public string NaverCookie
        {
            get { return Convert.ToString(this["NaverCookie"]); }
            set { this["NaverCookie"] = value; }
        }

        /// <summary>
        /// 开发者模式
        /// </summary>
        [UserScopedSetting]
        public bool DeveloperMode
        {
            get { return Convert.ToBoolean(this["DeveloperMode"]); }
            set { this["DeveloperMode"] = value; }
        }

        [UserScopedSetting]
        public bool ValidProgramName
        {
            get { return Convert.ToBoolean(this["ValidProgramName"]); }
            set { this["ValidProgramName"] = value; }
        }

        [UserScopedSetting]
        public bool KoreaSystem
        {
            get { return Convert.ToBoolean(this["KoreaSystem"]); }
            set { this["KoreaSystem"] = value; }
        }
    }
}
