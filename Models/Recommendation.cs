namespace OSExpertSystemWinUI.Models;

public sealed class Recommendation
{
    public required OsOption Option { get; init; }
    public int Score { get; init; }
    public double Percent { get; init; }
    public List<RuleHit> Hits { get; init; } = [];

    public string PercentText => $"{Percent:0}%";
    public string ScoreText => $"{Score} балів";
}
