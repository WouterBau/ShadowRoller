namespace ShadowRoller.Domain.Contexts.Dice;
public class DiceModifierSumRollResult : IRollResult
{
    private IEnumerable<int> _modifiers = new int[]{};
    private IEnumerable<int> _diceResults = new int[]{};

    public IEnumerable<int> DiceResults { get => _diceResults; init => _diceResults = value; }
    public IEnumerable<int> Modifiers { get => _modifiers; init => _modifiers = value; }
    public int Result => DiceResults.Sum() + Modifiers.Sum();
}