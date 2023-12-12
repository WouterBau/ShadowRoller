using ShadowRoller.Domain.Contexts.ShadowRun;
using System.Linq;
using Xunit;

namespace ShadowRoller.Domain.ShadowRun.Tests;
public class ShadowRunContextParserTests
{
    [Theory]
    [InlineData(new[] { "0" }, 0, null)]
    [InlineData(new[] { "-1" }, 0, null)]
    [InlineData(new[] { "1", "1" }, 2, null)]
    [InlineData(new[] { " 1 ", " 1 " }, 2, null)]
    [InlineData(new[] { "2", "1" }, 3, null)]
    [InlineData(new[] { "2", "-1" }, 1, null)]
    [InlineData(new[] { "2", "0", "-1" }, 1, null)]
    [InlineData(new[] { "2", "-1", "[1]" }, 1, 1)]
    [InlineData(new[] { "a" }, 0, null)]
    [InlineData(new[] { "a", "2", "[1]", "-1" }, 1, 1)]
    [InlineData(new[] { "a", "2", "[1", "-1" }, 1, null)]
    [InlineData(new[] { "a", "2", "1]", "-1" }, 1, null)]
    public void TestContextParser(string[] arguments, int expectedAmountDice, int? expectedHitLimit)
    {
        var parser = new ShadowRunContextParser();
        var context = parser.ParseToRollContext(arguments);
        var result = context.Resolve();
        Assert.Equal(expectedAmountDice, result.DiceResults.Count());
        Assert.Equal(expectedHitLimit, result.HitLimit);
    }
}