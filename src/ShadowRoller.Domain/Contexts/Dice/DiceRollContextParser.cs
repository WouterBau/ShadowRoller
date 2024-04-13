using System.Text.RegularExpressions;

namespace ShadowRoller.Domain.Contexts.Dice;
public class DiceRollContextParser : IRollContextParser<DiceRollContext, DiceModifierSumRollResult>
{
    private readonly Regex _regex = new(@"^\d+d\d+$", RegexOptions.None, TimeSpan.FromMilliseconds(100));
    public DiceRollContext ParseToRollContext(string[] arguments)
    {
        var dice = new List<Die>();
        var modifiers = new List<int>();
        foreach (var arg in arguments.Select(x => x.Trim()))
        {
            if (int.TryParse(arg, out var modifier))
                modifiers.Add(modifier);
            else if (_regex.IsMatch(arg))
            {
                var values = arg.Split('d');
                var amountDice = int.Parse(values[0]);
                var amountSides = int.Parse(values[1]);
                dice.AddRange(Enumerable.Repeat(new Die(amountSides), amountDice));
            }
        }
        return new DiceRollContext(dice, modifiers);
    }
}