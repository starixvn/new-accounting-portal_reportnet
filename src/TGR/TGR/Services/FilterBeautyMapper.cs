namespace TGR.Services;

internal static class FilterBeautyMapper
{
    internal static string BeautyVrSetting(string text)
    {
        return text.Replace("VR_PLUS", "VR +")
            .Replace("NO_VR", "No VR")
            .Replace("VR_MINUS", "VR -");
    }

    internal static string BeautyMatchGroup(string text)
    {
        return text.Replace("EXTRA_TIME", "Extra Time")
            .Replace("NORMAL", "Normal")
            .Replace("PENALTY", "Penalty")
            .Replace("HEDGE", "Hedge");
    }
}