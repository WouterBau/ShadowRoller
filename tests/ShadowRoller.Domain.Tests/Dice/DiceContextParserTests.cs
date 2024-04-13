using ShadowRoller.Domain.Contexts.Dice;
using System.Linq;
using Xunit;

namespace ShadowRoller.Domain.Tests.Dice;

public class DiceContextParserTests
{
    private class DiceRollContextTestData : TheoryData<string[], int, int[]>
    {
        public DiceRollContextTestData()
        {
            Add(["1d6"], 1, []);
            Add(["1d6", "2d8"], 3, []);
            Add(["1d6", "2d8", "1"], 3, [1]);
            Add(["1d6", "12d8", "1", "-3"], 13, [1, -3]);
            Add(["1d6", "12a8", "1", "-3"], 1, [1, -3]);
        }
    }

    [Theory]
    [ClassData(typeof(DiceRollContextTestData))]
    public void TestContextParser(string[] arguments, int expectedAmountDice, int[] expectedModifiers)
    {
        var parser = new DiceRollContextParser();
        var context = parser.ParseToRollContext(arguments);
        var result = context.Resolve();
        Assert.Equal(expectedAmountDice, result.DiceResults.Count());
        Assert.Equivalent(expectedModifiers, result.Modifiers);
    }
}