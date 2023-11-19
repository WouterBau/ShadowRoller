using ShadowRoller.Domain.Contexts.Dice;
using System.Linq;
using Xunit;

namespace ShadowRoller.Domain.Tests.Dice;

public class DiceContextParserTests
{
    [Theory]
    [InlineData(new[] { "1d6" }, 1, new int[] { })]
    [InlineData(new[] { "1d6", "2d8" }, 3, new int[] { })]
    [InlineData(new[] { "1d6", "2d8", "1" }, 3, new[] { 1 })]
    [InlineData(new[] { "1d6", "12d8", "1", "-3" }, 13, new[] { 1, -3 })]
    [InlineData(new[] { "1d6", "12a8", "1", "-3" }, 1, new[] { 1, -3 })]
    public void TestContextParser(string[] arguments, int expectedAmountDice, int[] expectedModifiers)
    {
        var parser = new DiceRollContextParser();
        var context = parser.ParseToRollContext(arguments);
        var result = context.Resolve();
        Assert.Equal(expectedAmountDice, result.DiceResults.Count());
        Assert.Equivalent(expectedModifiers, result.Modifiers);
    }
}