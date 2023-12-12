using ShadowRoller.Domain.Contexts.Dice;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ShadowRoller.Domain.Tests.Dice;

public class DiceRollContextTests
{
    public static IEnumerable<object[]> ShadowRunRollContextTestValues() => new[]
    {
        new object[]{
            new DiceRollContext(
                new Die[]
                {
                    new(6),
                    new(6),
                    new(6),
                    new(6)
                },
                new[]
                {
                    1,
                    -1
                }),
            4,
            new[] { 1, -1 }
        }
    };

    [Theory]
    [MemberData(nameof(ShadowRunRollContextTestValues))]
    public void ShadowRunRollContextTest(DiceRollContext context, int expectedAmount, int[] expectedModifiers)
    {
        var result = context.Resolve();
        Assert.Equal(expectedAmount, result.DiceResults.Count());
        Assert.Equivalent(expectedModifiers, result.Modifiers);
    }
}