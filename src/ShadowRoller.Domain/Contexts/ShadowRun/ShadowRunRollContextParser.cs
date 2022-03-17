namespace ShadowRoller.Domain.Contexts.ShadowRun;
public class ShadowRunContextParser : IRollContextParser<ShadowRunRollContext, ShadowRunRollResult>
{
    private const int AMOUNTSIDES = 6;
    public ShadowRunRollContext ParseToRollContext(string[] arguments)
    {
        var dice = new List<Die>();
        int? hitLimit = null;
        foreach (var arg in arguments.Select(x => x.Trim()))
            if (arg.StartsWith('[') && arg.EndsWith(']'))
                hitLimit = GetHitLimit(arg);
            else
                AlterDicePool(arg, dice);

        return new ShadowRunRollContext(dice, hitLimit);
    }

    private static void AlterDicePool(string arg, List<Die> dice)
    {
        var amountDice = GetValue(arg);
        if (!amountDice.HasValue)
            return;
        if (amountDice.Value >= 0)
            dice.AddRange(Enumerable.Repeat(new Die(AMOUNTSIDES), amountDice.Value));
        else
            dice.RemoveRange(0, Math.Abs(amountDice.Value));
    }

    private static int? GetHitLimit(string arg)
    {
        arg = arg.Replace("[", "").Replace("]", "");
        return GetValue(arg);
    }

    private static int? GetValue(string arg)
    {
        arg = arg.Trim();
        if (int.TryParse(arg, out var value))
            return value;
        return null;
    }
}