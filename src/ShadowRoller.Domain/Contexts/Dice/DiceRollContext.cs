namespace ShadowRoller.Domain.Contexts.Dice;
public class DiceRollContext : RollContext<DiceModifierSumRollResult>
{
    private IEnumerable<int> Modifiers { get; init; }
    public DiceRollContext(ICollection<Die> dice, IEnumerable<int> modifiers) : base(dice)
    {
        Modifiers = modifiers;
    }
    public override DiceModifierSumRollResult Resolve()
    {
        var rollResults = RollDice();
        var result = new DiceModifierSumRollResult
        {
            DiceResults = rollResults,
            Modifiers = Modifiers
        };
        return result;
    }
}