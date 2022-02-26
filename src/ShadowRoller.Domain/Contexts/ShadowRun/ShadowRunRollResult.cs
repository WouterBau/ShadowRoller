using System.Text;

namespace ShadowRoller.Domain.Contexts.ShadowRun;
public class ShadowRunRollResult : IRollResult
{
    private IEnumerable<int> _diceResults = new int[]{};
    public IEnumerable<int> DiceResults
    {
        get => _diceResults;
        init
        {
            if (value == null)
                return;
            _diceResults = value.Where(x => 1 <= x && x <= 6);
        }
    }
    public int? HitLimit { get; init; }
    public int GrossAmountHits => DiceResults.Where(x => x >= 5).Count();
    public int NetAmountHits
    {
        get
        {
            if (HitLimit.HasValue && HitLimit.Value < GrossAmountHits)
                return HitLimit.Value;
            return GrossAmountHits;
        }
    }
    public int AmountMisses => DiceResults.Where(x => x == 1).Count();
    public bool HasGlitched => AmountMisses > (DiceResults.Count() / 2);
    public bool HasGlitchedCritically => HasGlitched && GrossAmountHits == 0;

    public string ToString(string player)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{player} Rolled: {string.Join(" ", DiceResults)} Limit: {HitLimit}");
        sb.AppendLine($"Net amount hits: {NetAmountHits} Amount misses: {AmountMisses}");
        if (HasGlitchedCritically)
            sb.AppendLine("CRITICAL GLITCH!!");
        else if (HasGlitched)
            sb.AppendLine("REGULAR GLITCH!!");
        return sb.ToString();
    }
}