namespace ShadowRoller.Domain;
public static class DiceRoller
{
    private static Random _randomizer = new Random();

    public static int NextRandom(int maxValue)
    {
        var value = _randomizer.Next(maxValue);
        value++;
        return value;
    }

    private const int MIN = 1;

    public static int RollSumWithModifiers(int amount, int maxValue, IEnumerable<int> modifiers)
    {
        var result = RollSum(amount, maxValue);
        foreach(var modifier in modifiers)
            result = result + modifier;
        return result;
    }

    public static int RollSum(int amount, int maxValue)
    {
        var diceRolls = RollSeparately(amount, maxValue);
        var sum = diceRolls.Sum();
        return sum;
    }

    public static IEnumerable<int> RollSeparately(int amount, int maxValue)
    {
        if (maxValue < MIN)
            throw new ArgumentException();

        var diceRolls = new List<int>();
        var rnd = new Random();

        var max = maxValue + 1;
        while (diceRolls.Count < amount)
        {
            var value = rnd.Next(MIN, max);
            diceRolls.Add(value);
        }

        return diceRolls;
    }
}