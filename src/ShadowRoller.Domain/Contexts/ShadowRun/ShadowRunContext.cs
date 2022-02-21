namespace ShadowRoller.Domain.Contexts.ShadowRun;
public class ShadowRunContext : RollContext<ShadowRunRollResult>
{
    private const int MAXAMOUNTSIDES = 6;
    private int? HitLimit { get; init; }
    public ShadowRunContext(ICollection<Die> dice, int? hitLimit = null) : base(dice)
    {
        if(dice.Any(x => x.AmountSides > MAXAMOUNTSIDES))
            throw new InvalidAmountSidesException();
        HitLimit = hitLimit;
    }
    public override ShadowRunRollResult Resolve()
    {
        var rollResults = RollDice();
        var result = new ShadowRunRollResult
        {
            DiceResults = rollResults,
            HitLimit = HitLimit
        };
        return result;
    }
}