using Microsoft.Extensions.Configuration;

namespace Utilities.Configurations
{
    public interface ConfigurationService
    {
        string CurrentDirectory { get; }

        string FraudSystemCategoryApi { get; }
    }

    public class ConfigurationServiceImp(IConfiguration configuration) : ConfigurationService
    {
        public string CurrentDirectory => AppDomain.CurrentDomain.BaseDirectory;

        public string FraudSystemCategoryApi => configuration["FraudSystemCategoryAPI"];
    }
}
