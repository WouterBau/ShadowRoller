namespace ShadowRoller.Domain;
public class ShadowRunRoller
{
    private const int MIN = 1;
    private const int MAX = 7; //Is exclusive
    public static ShadowRunRollResult RollAmountDice(int amount, int? hitLimit = null)
    {
        var diceRolls = new List<int>();
        var rnd = new Random();
        while (diceRolls.Count < amount)
        {
            var value = rnd.Next(MIN, MAX);
            diceRolls.Add(value);
        }
        return new ShadowRunRollResult
        {
            DiceResults = diceRolls,
            HitLimit = hitLimit
        };
    }
    public static ShadowRunRollResult RollByAttributes(int[] attributeValues, int? hitLimit = null)
    {
        if (attributeValues.Any(x => x <= 0))
            throw new ArgumentException("Attributes should be positive integers");
        return RollAmountDice(attributeValues.Sum(), hitLimit);
    }
}
