using ShadowRoller.Domain.Contexts.Dice;
using System.Linq;
using Xunit;

namespace ShadowRoller.Domain.Tests.Dice;

public class DiceRollContextTests
{
    private class ShadowRunRollContextTestData : TheoryData<DiceRollContext, int, int[]>
    {
        public ShadowRunRollContextTestData()
        {
            Add(
                new DiceRollContext(
                    [
                        new(6),
                        new(6),
                        new(6),
                        new(6)
                    ],
                    [ 1, -1 ]),
                4,
                [1, -1]
            );
        }
    }

    [Theory]
    [ClassData(typeof(ShadowRunRollContextTestData))]
    public void ShadowRunRollContextTest(DiceRollContext context, int expectedAmount, int[] expectedModifiers)
    {
        var result = context.Resolve();
        Assert.Equal(expectedAmount, result.DiceResults.Count());
        Assert.Equivalent(expectedModifiers, result.Modifiers);
    }
}