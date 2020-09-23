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
        /// <summary>
        /// 程序路径
        /// </summary>
        [UserScopedSetting]
        public string EasyMaplePath
        {
            get { return Convert.ToString(this["EasyMaplePath"]); }
            set { this["EasyMaplePath"] = value; }
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

        [UserScopedSetting]
        public string NgmPath
        {
            get { return Convert.ToString(this["NgmPath"]); }
            set { this["NgmPath"] = value; }
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
        /// -1 启动失败
        /// 0 未启动
        /// 1 游戏启动
        /// </summary>
        [UserScopedSetting]
        public int MapleStartStatus
        {
            get { return Convert.ToInt32(this["MapleStartStatus"]); }
            set { this["MapleStartStatus"] = value; }
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
        public bool ProxyIsOther
        {
            get { return Convert.ToBoolean(this["ProxyIsOther"]); }
            set { this["ProxyIsOther"] = value; }
        }

        [UserScopedSetting]
        public bool CkAutoReLogin
        {
            get { return Convert.ToBoolean(this["CkAutoReLogin"]); }
            set { this["CkAutoReLogin"] = value; }
        }

        [UserScopedSetting]
        public bool CkTestWord
        {
            get { return Convert.ToBoolean(this["CkTestWord"]); }
            set { this["CkTestWord"] = value; }
        }

        [UserScopedSetting]
        public bool CkNotLoadMapleIds
        {
            get { return Convert.ToBoolean(this["CkNotLoadMapleIds"]); }
            set { this["CkNotLoadMapleIds"] = value; }
        }


        [UserScopedSetting]
        public bool  CkNotAutoLogin
        {
            get { return Convert.ToBoolean(this["CkNotAutoLogin"]); }
            set { this["CkNotAutoLogin"] = value; }
        }

        [UserScopedSetting]
        public List<LoginData> LoginData
        {
            get { return this["LoginData"] as List<LoginData>; }
            set { this["LoginData"] = value; }
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

        private DateTime _addTime = DateTime.Now;
        public DateTime AddTime { get => _addTime; set => _addTime = value; }

        private bool _isDefault = false;
        public bool IsDefault { get => _isDefault; set => _isDefault = value; }
    }
}
