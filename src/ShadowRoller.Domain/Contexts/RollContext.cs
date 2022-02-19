namespace ShadowRoller.Domain.Contexts;
public abstract class RollContext<T> where T : IRollResult
{
    protected ICollection<Die> Dice{ get; private set; }

    public RollContext(ICollection<Die> dice)
    {
        Dice = dice;
    }

    public abstract T Resolve();
}