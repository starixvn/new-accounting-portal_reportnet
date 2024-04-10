namespace Utilities.Configurations
{
    public interface ConfigurationService
    {
        string CurrentDirectory { get; }
    }

    public class ConfigurationServiceImp : ConfigurationService
    {
        public string CurrentDirectory => AppDomain.CurrentDomain.BaseDirectory;
    }
}
