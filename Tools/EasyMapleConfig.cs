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

        /// <summary>
        /// NGM路径
        /// </summary>
        [UserScopedSetting]
        public string NgmPath
        {
            get { return Convert.ToString(this["NgmPath"]); }
            set { this["NgmPath"] = value; }
        }

        /// <summary>
        /// 冒险岛测试服路径
        /// </summary>
        [UserScopedSetting]
        public string MapleTPath
        {
            get { return Convert.ToString(this["MapleTPath"]); }
            set { this["MapleTPath"] = value; }
        }

        /// <summary>
        /// 默认账号的NaverCookie
        /// </summary>
        [UserScopedSetting]
        public string DefaultNaverCookie
        {
            get { return Convert.ToString(this["DefaultNaverCookie"]); }
            set { this["DefaultNaverCookie"] = value; }
        }

        /// <summary>
        /// 默认账号的昵称
        /// </summary>
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

        /// <summary>
        /// 代理是其他
        /// </summary>
        [UserScopedSetting]
        public bool ProxyIsOther
        {
            get { return Convert.ToBoolean(this["ProxyIsOther"]); }
            set { this["ProxyIsOther"] = value; }
        }

        /// <summary>
        /// 自动重新登录
        /// </summary>
        [UserScopedSetting]
        public bool CkAutoReLogin
        {
            get { return Convert.ToBoolean(this["CkAutoReLogin"]); }
            set { this["CkAutoReLogin"] = value; }
        }

        /// <summary>
        /// 测试服
        /// </summary>
        [UserScopedSetting]
        public bool CkTestWord
        {
            get { return Convert.ToBoolean(this["CkTestWord"]); }
            set { this["CkTestWord"] = value; }
        }

        /// <summary>
        /// 不自动加载子号
        /// </summary>
        [UserScopedSetting]
        public bool CkNotLoadMapleIds
        {
            get { return Convert.ToBoolean(this["CkNotLoadMapleIds"]); }
            set { this["CkNotLoadMapleIds"] = value; }
        }

        /// <summary>
        /// 不自动登录
        /// </summary>
        [UserScopedSetting]
        public bool  CkNotAutoLogin
        {
            get { return Convert.ToBoolean(this["CkNotAutoLogin"]); }
            set { this["CkNotAutoLogin"] = value; }
        }

        /// <summary>
        /// 登录数据
        /// </summary>
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
        /// <summary>
        /// 账号标识
        /// </summary>
        private string _guid = string.Empty;
        public string Guid { get => _guid; set => _guid = value; }

        /// <summary>
        /// 账号昵称
        /// </summary>
        private string _accountTag = string.Empty;
        public string AccountTag { get => _accountTag; set => _accountTag = value; }

        /// <summary>
        /// Cookie
        /// </summary>
        private string _accountCookieStr = string.Empty;
        public string AccountCookieStr { get => _accountCookieStr; set => _accountCookieStr = value; }

        /// <summary>
        /// 添加时间
        /// </summary>
        private DateTime _addTime = DateTime.Now;
        public DateTime AddTime { get => _addTime; set => _addTime = value; }

        /// <summary>
        /// 是否默认
        /// </summary>
        private bool _isDefault = false;
        public bool IsDefault { get => _isDefault; set => _isDefault = value; }
    }
}
