namespace OSExpertSystemWinUI.Models;

public sealed record ExpertRule(
    string Id,
    string OptionId,
    string Title,
    int Weight,
    IReadOnlyDictionary<string, string> Conditions,
    string Explanation,
    bool Active = true)
{
    public bool Matches(UserProfile profile)
    {
        if (!Active) return false;

        var answers = profile.ToDictionary();
        foreach (var condition in Conditions)
        {
            if (!answers.TryGetValue(condition.Key, out var actualValue))
                return false;

            if (!string.Equals(actualValue, condition.Value, StringComparison.OrdinalIgnoreCase))
                return false;
        }

        return true;
    }
}
