namespace ShadowRoller.Domain;
public class ShadowRunRoller
{
    private const int MAX = 6;
    public static ShadowRunRollResult RollAmountDice(int amount, int? hitLimit = null)
    {
        var diceRolls = DiceRoller.RollSeparately(amount, MAX);
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
