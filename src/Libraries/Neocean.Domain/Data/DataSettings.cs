using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neocean.Domain.Data
{
    /// <summary>
    /// 数据库设置
    /// </summary>
    public partial class DataSettings
    {
        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        public DataSettings()
        {
            RawDataSettings = new Dictionary<string, string>();
        }
        #endregion

        #region Public Property
        /// <summary>
        /// 数据驱动
        /// </summary>
        public string DataProvider { get; set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string DataConnectionString { get; set; }
        /// <summary>
        /// 原始数据设置
        /// </summary>
        public IDictionary<string, string> RawDataSettings { get; private set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// 验证是否有效
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return !String.IsNullOrEmpty(this.DataProvider) && !String.IsNullOrEmpty(this.DataConnectionString);
        }
        #endregion

    }
}
