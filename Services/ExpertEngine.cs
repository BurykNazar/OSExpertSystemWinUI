using OSExpertSystemWinUI.Models;

namespace OSExpertSystemWinUI.Services;

public sealed class ExpertEngine
{
    private readonly KnowledgeBase _kb;

    public ExpertEngine(KnowledgeBase kb)
    {
        _kb = kb;
    }

    public IReadOnlyList<Recommendation> Evaluate(UserProfile profile)
    {
        var scoreMap = _kb.Options.ToDictionary(o => o.Id, _ => 0);
        var hitMap = _kb.Options.ToDictionary(o => o.Id, _ => new List<RuleHit>());

        foreach (var rule in _kb.Rules)
        {
            if (!rule.Matches(profile)) continue;

            scoreMap[rule.OptionId] += rule.Weight;
            hitMap[rule.OptionId].Add(new RuleHit(rule.Id, rule.Title, rule.Weight, rule.Explanation));
        }

        var maxScore = Math.Max(1, scoreMap.Values.Max());

        return _kb.Options
            .Select(option => new Recommendation
            {
                Option = option,
                Score = scoreMap[option.Id],
                Percent = scoreMap[option.Id] * 100.0 / maxScore,
                Hits = hitMap[option.Id]
            })
            .OrderByDescending(r => r.Score)
            .ThenBy(r => r.Option.Name)
            .ToList();
    }
}
