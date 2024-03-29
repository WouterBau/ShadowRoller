namespace ShadowRoller.Domain.ShadowRun.Tests;

using ShadowRoller.Domain.Contexts.ShadowRun;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class ShadowRunRollContextTests
{
    public static IEnumerable<object[]> ShadowRunRollContextTestValues() => new[]
        {
            new object[]{
                new ShadowRunRollContext(
                    new Die[]
                    {
                        new Die(6),
                        new Die(6),
                        new Die(6),
                        new Die(6)
                    },
                    null),
                4
            }
        };

    [Theory]
    [MemberData(nameof(ShadowRunRollContextTestValues))]
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