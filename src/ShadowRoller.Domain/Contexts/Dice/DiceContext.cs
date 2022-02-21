namespace ShadowRoller.Domain.Contexts.Dice;
public class DiceContext : RollContext<DiceModifierSumRollResult>
{
    private IEnumerable<int> Modifiers { get; init; }
    public DiceContext(ICollection<Die> dice, IEnumerable<int> modifiers) : base(dice)
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