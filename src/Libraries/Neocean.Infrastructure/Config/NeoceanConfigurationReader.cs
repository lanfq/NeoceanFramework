using System;
using System.Collections;
using System.Configuration;

namespace Neocean.Infrastructure.Config
{
    /// <summary>
    /// 表示对Necoean配置信息进行读取的单例类型。
    /// </summary>
    public sealed class NeoceanConfigurationReader
    {
        #region Private Fields

        private static readonly NeoceanConfigurationReader instance = new NeoceanConfigurationReader();
        private readonly NeoceanConfigurationSection configuration;

        #endregion

        #region Ctor

        static NeoceanConfigurationReader()
        {
        }

        private NeoceanConfigurationReader()
        {
            configuration = NeoceanConfigurationSection.Instance;
            if (configuration == null)
                throw new ConfigurationErrorsException("当前应用程序的配置文件中不存在与Necoean相关的配置信息。");
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Memcache 连接超时时间。
        /// </summary>
        public TimeSpan ConnectionTimeout
        {
            get { return configuration.Memcache.SocketPool.ConnectionTimeout; }
        }

        /// <summary>
        /// Memcache 无反应或无响应超时时间
        /// </summary>
        public TimeSpan DeadTimeout
        {
            get { return configuration.Memcache.SocketPool.DeadTimeout; }
        }

        /// <summary>
        /// Memcache 最大连接池数
        /// </summary>
        public int MaxPoolSize
        {
            get { return configuration.Memcache.SocketPool.MaxPoolSize; }
        }

        /// <summary>
        /// Memcache 最小连接池数
        /// </summary>
        public int MinPoolSize
        {
            get { return configuration.Memcache.SocketPool.MinPoolSize; }
        }

        /// <summary>
        ///Memcache 服务器地址
        /// </summary>
        public ArrayList Servers
        {
            get
            {
                var address = new ArrayList();
                foreach (ServerAddConfigurationElement add in configuration.Memcache.Servers)
                    address.Add(string.Format("{0}:{1}", add.Address, add.Port));
                return address;
            }
        }

        /// <summary>
        /// Email 地址
        /// </summary>
        public string EmailHost
        {
            get { return configuration.EmailClient.Host; }
        }

        /// <summary>
        /// Email 端口号
        /// </summary>
        public int EmailPort
        {
            get { return configuration.EmailClient.Port; }
        }

        /// <summary>
        /// Emial 用户名
        /// </summary>
        public string EmailUserName
        {
            get { return configuration.EmailClient.UserName; }
        }

        /// <summary>
        /// Email 密码
        /// </summary>
        public string EmailPassword
        {
            get { return configuration.EmailClient.Password; }
        }

        /// <summary>
        /// Email 发送者
        /// </summary>
        public string EmailSender
        {
            get { return configuration.EmailClient.Sender; }
        }

        /// <summary>
        /// Email 是否启动SSL加密链接
        /// </summary>
        public bool EmailEnableSsl
        {
            get { return configuration.EmailClient.EnableSsl; }
        }

        /// <summary>
        /// 获取<c>ByteartRetailConfigurationReader</c>的单例类型。
        /// </summary>
        public static NeoceanConfigurationReader Instance
        {
            get { return instance; }
        }

        #endregion
    }
}