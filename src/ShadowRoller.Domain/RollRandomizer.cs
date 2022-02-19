namespace ShadowRoller.Domain;
public static class RollRandomizer
{
    private static Random _randomizer = new Random();

    public static int NextRandom(int maxValue)
    {
        var value = _randomizer.Next(maxValue);
        value++;
        return value;
    }
}