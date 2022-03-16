using System.Text;

namespace ShadowRoller.Domain.Contexts.Dice;
public class DiceModifierSumRollResult : IRollResult
{
    private IEnumerable<int> _modifiers = Array.Empty<int>();
    private IEnumerable<int> _diceResults = Array.Empty<int>();

    public IEnumerable<int> DiceResults { get => _diceResults; init => _diceResults = value; }
    public IEnumerable<int> Modifiers { get => _modifiers; init => _modifiers = value; }
    public int Result => DiceResults.Sum() + Modifiers.Sum();

    public string ToString(string player)
    {
        var sb = new StringBuilder()
            .AppendLine($"{player} Rolled: {string.Join(" ", DiceResults)} Modifiers: {string.Join(" ", Modifiers)}")
            .AppendLine($"Result: {Result}");
        return sb.ToString();
    }
}