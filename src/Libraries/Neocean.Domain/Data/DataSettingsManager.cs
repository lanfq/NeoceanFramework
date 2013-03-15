using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using System.Web.Hosting;

namespace Neocean.Domain.Data
{
    /// <summary>
    /// 数据库配置管理器
    /// </summary>
    public partial class DataSettingsManager
    {
        #region Protected Const
        protected const char separator = ':';
        protected const string filename = "Settings.txt";
        #endregion

        #region Protected Methods
        /// <summary>
        /// Maps a virtual path to a physical disk path.
        /// </summary>
        /// <param name="path">The path to map. E.g. "~/bin"</param>
        /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
        protected virtual string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                //hosted
                return HostingEnvironment.MapPath(path);
            }
            else
            {
                //not hosted. For example, run in unit tests
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
                return Path.Combine(baseDirectory, path);
            }
        }

        /// <summary>
        /// 将文本内容转换成配置
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected virtual DataSettings ParseSettings(string text)
        {
            var shellSettings = new DataSettings();
            if (String.IsNullOrEmpty(text))
                return shellSettings;

            //Old way of file reading. This leads to unexpected behavior when a user's FTP program transfers these files as ASCII (\r\n becomes \n).
            //var settings = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var settings = new List<string>();
            using (var reader = new StringReader(text))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                    settings.Add(str);
            }

            foreach (var setting in settings)
            {
                var separatorIndex = setting.IndexOf(separator);
                if (separatorIndex == -1)
                {
                    continue;
                }
                string key = setting.Substring(0, separatorIndex).Trim();
                string value = setting.Substring(separatorIndex + 1).Trim();

                switch (key)
                {
                    case "DataProvider":
                        shellSettings.DataProvider = value;
                        break;
                    case "DataConnectionString":
                        shellSettings.DataConnectionString = value;
                        break;
                    default:
                        shellSettings.RawDataSettings.Add(key, value);
                        break;
                }
            }

            return shellSettings;
        }

        /// <summary>
        /// 构建设置的文本内容
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        protected virtual string ComposeSettings(DataSettings settings)
        {
            if (settings == null)
                return "";

            return string.Format("DataProvider: {0}{2}DataConnectionString: {1}{2}",
                                 settings.DataProvider,
                                 settings.DataConnectionString,
                                 Environment.NewLine
                );
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 加载配置
        /// </summary>
        /// <returns></returns>
        public virtual DataSettings LoadSettings()
        {
            //use webHelper.MapPath instead of HostingEnvironment.MapPath which is not available in unit tests
            string filePath = Path.Combine(MapPath("~/App_Data/"), filename);
            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);
                return ParseSettings(text);
            }
            else
                return new DataSettings();
        }
        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="settings"></param>
        public virtual void SaveSettings(DataSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");

            //use webHelper.MapPath instead of HostingEnvironment.MapPath which is not available in unit tests
            string filePath = Path.Combine(MapPath("~/App_Data/"), filename);
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath))
                {
                    //we use 'using' to close the file after it's created
                }
            }

            var text = ComposeSettings(settings);
            File.WriteAllText(filePath, text);
        }
        #endregion
    }
}
