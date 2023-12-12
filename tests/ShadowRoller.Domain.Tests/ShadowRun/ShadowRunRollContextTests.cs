using ShadowRoller.Domain.Contexts.ShadowRun;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ShadowRoller.Domain.ShadowRun.Tests;
public class ShadowRunRollContextTests
{
    public static IEnumerable<object[]> ShadowRunRollContextTestValues() => new[]
        {
            new object[]{
                new ShadowRunRollContext(
                    new Die[]
                    {
                        new(6),
                        new(6),
                        new(6),
                        new(6)
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