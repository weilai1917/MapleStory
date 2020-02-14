using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMaple
{
    public sealed class EasyMapleConfig : ApplicationSettingsBase
    {
        [UserScopedSetting]
        public List<LoginData> LoginData
        {
            get { return this["LoginData"] as List<LoginData>; }
            set { this["LoginData"] = value; }
        }

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
        /// 冒险岛路径
        /// </summary>
        [UserScopedSetting]
        public string MapleTPath
        {
            get { return Convert.ToString(this["MapleTPath"]); }
            set { this["MapleTPath"] = value; }
        }

        /// <summary>
        /// NaverCookie
        /// </summary>
        [UserScopedSetting]
        public string DefaultNaverCookie
        {
            get { return Convert.ToString(this["DefaultNaverCookie"]); }
            set { this["DefaultNaverCookie"] = value; }
        }

        [UserScopedSetting]
        public string DefaultNaverNickName
        {
            get { return Convert.ToString(this["DefaultNaverNickName"]); }
            set { this["DefaultNaverNickName"] = value; }
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

        [UserScopedSetting]
        public bool ProxyIsOther
        {
            get { return Convert.ToBoolean(this["ProxyIsOther"]); }
            set { this["ProxyIsOther"] = value; }
        }
    }

    [Serializable]
    public class LoginData
    {
        private string _guid = string.Empty;
        public string Guid { get => _guid; set => _guid = value; }

        private string _accountTag = string.Empty;
        public string AccountTag { get => _accountTag; set => _accountTag = value; }

        private string _accountCookieStr = string.Empty;
        public string AccountCookieStr { get => _accountCookieStr; set => _accountCookieStr = value; }

        private bool _isDefault = false;
        public bool IsDefault { get => _isDefault; set => _isDefault = value; }
    }
}
