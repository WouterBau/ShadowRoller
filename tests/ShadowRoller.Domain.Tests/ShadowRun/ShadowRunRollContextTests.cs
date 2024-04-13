using ShadowRoller.Domain.Contexts.ShadowRun;
using System.Linq;
using Xunit;

namespace ShadowRoller.Domain.ShadowRun.Tests;
public class ShadowRunRollContextTests
{

    private class ShadowRunRollContextTestData : TheoryData<ShadowRunRollContext, int>
    {
        public ShadowRunRollContextTestData()
        {
            Add(
                new ShadowRunRollContext(
                    [
                        new(6),
                        new(6),
                        new(6),
                        new(6)
                    ]),
                4
            );
        }
    }

    [Theory]
    [ClassData(typeof(ShadowRunRollContextTestData))]
    public void ShadowRunRollContextTest(ShadowRunRollContext context, int expectedAmount)
    {
        var result = context.Resolve();
        Assert.Equal(expectedAmount, result.DiceResults.Count());
    }

    [Fact]
    public void ShadowRunRollContextFailsOnInvalidDie()
    {
        Assert.Throws<InvalidAmountSidesException>(() => new ShadowRunRollContext(new[] { new Die(7) }));
    }
}