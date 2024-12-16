using Microsoft.Extensions.Configuration;

namespace Utilities.Configurations;

public interface ConfigurationService
{
    string CurrentDirectory { get; }

    string FraudSystemCategoryApi { get; }

    bool IsDisableMostvisitedPage { get; }
}

public class ConfigurationServiceImp(IConfiguration configuration) : ConfigurationService
{
    public string CurrentDirectory => AppDomain.CurrentDomain.BaseDirectory;

    public string FraudSystemCategoryApi => configuration["FraudSystemCategoryAPI"];

    public bool IsDisableMostvisitedPage => configuration.GetValue<bool>("IsDisableMostvisitedPage");
}