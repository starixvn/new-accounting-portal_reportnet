namespace Utilities.Configurations;

using Microsoft.Extensions.Configuration;

public interface ConfigurationService
{
    string CurrentDirectory { get; }

    string FraudSystemCategoryApi { get; }

    bool IsDisableMostvisitedPage { get; }

    DateTime AvailableDate { get; }

    int PerformanceYearRange { get; }

    int PerformanceAllowYearRange { get; }

    int PerformanceDefaultDayRange { get; }
}

public class ConfigurationServiceImp(IConfiguration configuration) : ConfigurationService
{
    public string CurrentDirectory => AppDomain.CurrentDomain.BaseDirectory;

    public string FraudSystemCategoryApi => configuration["FraudSystemCategoryAPI"];

    public bool IsDisableMostvisitedPage => configuration.GetValue<bool>("IsDisableMostvisitedPage");

    public DateTime AvailableDate  => configuration.GetValue<DateTime>("AvailableDate");

    public int PerformanceYearRange => configuration.GetValue<int>("PerformanceYearRange");

    public int PerformanceAllowYearRange => configuration.GetValue<int>("PerformanceAllowYearRange");

    public int PerformanceDefaultDayRange => configuration.GetValue<int>("PerformanceDefaultDayRange");
}