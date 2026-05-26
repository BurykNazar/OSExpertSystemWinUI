namespace OSExpertSystemWinUI.Models;

public sealed record OsOption(
    string Id,
    string Name,
    string ShortDescription,
    string FullAdvice,
    bool Active = true);
