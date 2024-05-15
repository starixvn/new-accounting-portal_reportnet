using System.Configuration;

namespace Utilities.Configurations
{
    public interface ConfigurationService
    {
        string CurrentDirectory { get; }

        string FraudSystemCategoryApi { get; }
    }

    public class ConfigurationServiceImp : ConfigurationService
    {
        public string CurrentDirectory => AppDomain.CurrentDomain.BaseDirectory;

        public string FraudSystemCategoryApi => ConfigurationManager.AppSettings["FraudSystemCategoryAPI"];
    }
}
