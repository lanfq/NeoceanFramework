using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neocean.Domain.Data
{
    /// <summary>
    /// 数据库配置帮助类
    /// </summary>
    public partial class DataSettingsHelper
    {
        #region Private Fields
        /// <summary>
        /// 数据库是否安装
        /// </summary>
        private static bool? _databaseIsInstalled;
        #endregion

        #region Public Static Methods
        /// <summary>
        /// 获取数据库是否安装
        /// </summary>
        /// <returns></returns>
        public static bool DatabaseIsInstalled()
        {
            if (!_databaseIsInstalled.HasValue)
            {
                var manager = new DataSettingsManager();
                var settings = manager.LoadSettings();
                _databaseIsInstalled = settings != null && !String.IsNullOrEmpty(settings.DataConnectionString);
            }
            return _databaseIsInstalled.Value;
        }
        /// <summary>
        /// 重置
        /// </summary>
        public static void ResetCache()
        {
            _databaseIsInstalled = null;
        }
        #endregion
    }
}
