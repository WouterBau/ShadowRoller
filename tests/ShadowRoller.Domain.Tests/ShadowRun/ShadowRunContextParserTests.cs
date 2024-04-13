using ShadowRoller.Domain.Contexts.ShadowRun;
using System.Linq;
using Xunit;

namespace ShadowRoller.Domain.ShadowRun.Tests;
public class ShadowRunContextParserTests
{
    private class ShadowRunContextTestData : TheoryData<string[], int, int?>
    {
        public ShadowRunContextTestData()
        {
            Add(["0"], 0, null);
            Add(["-1"], 0, null);
            Add(["1", "1"], 2, null);
            Add([" 1 ", " 1 "], 2, null);
            Add(["2", "1"], 3, null);
            Add(["2", "-1"], 1, null);
            Add(["2", "0", "-1"], 1, null);
            Add(["2", "-1", "[1]"], 1, 1);
            Add(["a"], 0, null);
            Add(["a", "2", "[1]", "-1"], 1, 1);
            Add(["a", "2", "[1", "-1"], 1, null);
            Add(["a", "2", "1]", "-1"], 1, null);
        }
    }

    [Theory]
    [ClassData(typeof(ShadowRunContextTestData))]
    public void TestContextParser(string[] arguments, int expectedAmountDice, int? expectedHitLimit)
    {
        var parser = new ShadowRunContextParser();
        var context = parser.ParseToRollContext(arguments);
        var result = context.Resolve();
        Assert.Equal(expectedAmountDice, result.DiceResults.Count());
        Assert.Equal(expectedHitLimit, result.HitLimit);
    }
}