namespace ShadowRoller.Domain.ShadowRun.Tests;
using System.Linq;
using ShadowRoller.Domain.Contexts.ShadowRun;
using Xunit;

public class ShadowRunContextParserTests
{
    [Theory]
    [InlineData(new []{ "1", "1" }, 2, null)]
    [InlineData(new []{ "2", "1" }, 3, null)]
    [InlineData(new []{ "2", "-1" }, 1, null)]
    [InlineData(new []{ "2", "-1", "[1]" }, 1, 1)]
    public void TestContextParser(string[] arguments, int expectedAmountDice, int? expectedHitLimit)
    {
        var parser = new ShadowRunContextParser();
        var context = parser.ParseToRollContext(arguments);
        var result = context.Resolve();
        Assert.Equal(expectedAmountDice, result.DiceResults.Count());
        Assert.Equal(expectedHitLimit, result.HitLimit);
    }
}