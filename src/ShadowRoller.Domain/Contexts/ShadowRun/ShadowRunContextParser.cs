namespace ShadowRoller.Domain.Contexts.ShadowRun;
public class ShadowRunContextParser : IRollContextParser<ShadowRunContext, ShadowRunRollResult>
{
    private const int AMOUNTSIDES = 6;
    public ShadowRunContext ParseToRollContext(string[] arguments)
    {
        var dice = new List<Die>();
        int? hitLimit = null;
        foreach (var arg in arguments.Select(x => x.Trim()))
            if (arg.StartsWith('[') && arg.EndsWith(']'))
                hitLimit = GetHitLimit(arg);
            else
                AlterDicePool(arg, dice);

        return new ShadowRunContext(dice, hitLimit);
    }

    private void AlterDicePool(string arg, ICollection<Die> dice)
    {
        var result = new List<Die>();
        var amountDice = GetValue(arg);
        if (!amountDice.HasValue)
            return;
        if (amountDice.Value >= 0)
            result.AddRange(Enumerable.Repeat(new Die(AMOUNTSIDES), amountDice.Value));
        else
            result.RemoveRange(0, Math.Abs(amountDice.Value));
    }

    private int? GetHitLimit(string arg)
    {
        arg = arg.Replace("[", "").Replace("]", "");
        return GetValue(arg);
    }

    private int? GetValue(string arg)
    {
        arg = arg.Trim();
        if (int.TryParse(arg, out var value))
            return value;
        return null;
    }
}