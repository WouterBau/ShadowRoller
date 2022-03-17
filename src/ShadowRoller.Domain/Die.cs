namespace ShadowRoller.Domain;
public class Die
{
    private const int MIN = 1;
    public int AmountSides { get; init; }
    public int? LastResult { get; internal set; }
    public Die(int amountSides)
    {
        if (amountSides < MIN)
            throw new InvalidAmountSidesException();
        AmountSides = amountSides;
    }
    public int Roll()
    {
        var value = RollRandomizer.NextRandom(AmountSides);
        LastResult = value;
        return value;
    }
}