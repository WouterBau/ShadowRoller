namespace ShadowRoller.Domain.Contexts;
public abstract class RollContext<T> where T : IRollResult
{
    protected ICollection<Die> Dice { get; init; }

    protected RollContext(ICollection<Die> dice)
    {
        Dice = dice;
    }

    public abstract T Resolve();

    protected IEnumerable<int> RollDice()
    {
        var rollResults = new List<int>();
        foreach (var die in Dice)
        {
            var rollResult = die.Roll();
            rollResults.Add(rollResult);
        }
        return rollResults;
    }
}