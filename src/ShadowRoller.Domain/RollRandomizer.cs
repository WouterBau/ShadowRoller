namespace ShadowRoller.Domain;
public static class RollRandomizer
{
    private static readonly Random _randomizer = new();

    public static int NextRandom(int maxValue)
    {
        var value = _randomizer.Next(maxValue);
        value++;
        return value;
    }
}