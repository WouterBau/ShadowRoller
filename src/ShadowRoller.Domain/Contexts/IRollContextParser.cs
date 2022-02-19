namespace ShadowRoller.Domain.Contexts;
public interface IRollContextParser<T, I>
 where T : RollContext<I>
  where I : IRollResult
{
    T ParseToRollContext(string arguments);
}