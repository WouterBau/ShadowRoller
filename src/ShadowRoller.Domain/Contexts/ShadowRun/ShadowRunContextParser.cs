namespace ShadowRoller.Domain.Contexts.ShadowRun;
public class ShadowRunContextParser : IRollContextParser<ShadowRunContext, ShadowRunRollResult>
{
    private const int AMOUNTSIDES = 6;
    public ShadowRunContext ParseToRollContext(string[] arguments)
    {
        var dice = new List<Die>();
        int? hitLimit = null;
        foreach (var arg in arguments.Select(x => x.Trim()))
        {
            if (arg.StartsWith('[') && arg.EndsWith(']'))
            {
                hitLimit = GetHitLimit(arg);
                if (hitLimit.HasValue)
                    break;
            }
            else
            {
                dice.AddRange(GetDice(arg));
            }
        }

        return new ShadowRunContext(dice, hitLimit);
    }

    private ICollection<Die> GetDice(string arg)
    {
        var amountDice = GetValue(arg);
        if (!amountDice.HasValue)
            return new Die[]{};
        return new Die[]{}; //TODO
    }

    private int? GetHitLimit(string arg)
    {
        arg = arg.Replace("[", "").Replace("]", "");
        return GetValue(arg);
    }

    private int? GetValue(string arg)
    {
        if (int.TryParse(arg, out var value))
            return value;
        return null;
    }
}