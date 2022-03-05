using System.Configuration;
using System.Reflection;

namespace Helper.Configuration
{
    public class Settings
    {
        private const string ErrMessageFormat = "{0} Could Not Be Found";
        private static string _sharedConfigFileName = @"ConfigFiles\dev.config";

        public static string GetSetting(string settingName)
        {
            var request = _getConfigInstance().AppSettings.Settings[settingName].Value ?? string.Format(ErrMessageFormat, "Application Setting");
            return request;
        }

        private static System.Configuration.Configuration _getConfigInstance()
        {
            _sharedConfigFileName = @"ConfigFiles\dev.config";

            var exeConfigurationFileMap = new ExeConfigurationFileMap();
            var uri = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) ?? string.Empty);

            exeConfigurationFileMap.ExeConfigFilename = Path.Combine(uri.LocalPath, _sharedConfigFileName);

            return ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, ConfigurationUserLevel.None);
        }
	}
}
