using System.Text;

namespace ShadowRoller.Domain.Contexts.Dice;
public class DiceModifierSumRollResult : IRollResult
{
    public IEnumerable<int> DiceResults { get; init; } = Array.Empty<int>();
    public IEnumerable<int> Modifiers { get; init; } = Array.Empty<int>();
    public int Result => DiceResults.Sum() + Modifiers.Sum();

    public string ToString(string player)
    {
        var sb = new StringBuilder()
            .AppendLine($"{player} Rolled: {string.Join(" ", DiceResults)} Modifiers: {string.Join(" ", Modifiers)}")
            .AppendLine($"Result: {Result}");
        return sb.ToString();
    }
}