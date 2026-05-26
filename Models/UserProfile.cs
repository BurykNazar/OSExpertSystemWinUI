namespace OSExpertSystemWinUI.Models;

public sealed class UserProfile
{
    public string DeviceType { get; set; } = "home_laptop";
    public string MainTask { get; set; } = "office_study";
    public string HardwareLevel { get; set; } = "medium";
    public string UserExperience { get; set; } = "regular";
    public string SoftwareCompatibility { get; set; } = "windows_apps";
    public string SecurityPriority { get; set; } = "standard";
    public string Administration { get; set; } = "simple";
    public string Budget { get; set; } = "free_or_existing";

    public Dictionary<string, string> ToDictionary() => new()
    {
        [nameof(DeviceType)] = DeviceType,
        [nameof(MainTask)] = MainTask,
        [nameof(HardwareLevel)] = HardwareLevel,
        [nameof(UserExperience)] = UserExperience,
        [nameof(SoftwareCompatibility)] = SoftwareCompatibility,
        [nameof(SecurityPriority)] = SecurityPriority,
        [nameof(Administration)] = Administration,
        [nameof(Budget)] = Budget
    };
}
