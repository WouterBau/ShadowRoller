using ShadowRoller.Domain.Contexts.ShadowRun;
using System.Linq;
using Xunit;

namespace ShadowRoller.Domain.ShadowRun.Tests;
public class ShadowRunRollResultTests
{
    private class ShadowRunRollResultTestData : TheoryData<ShadowRunRollResult, int, int, int, bool, bool, string, string> {
        public ShadowRunRollResultTestData()
        {
            Add(
                new ShadowRunRollResult
                {
                    DiceResults = new []{ 1 }
                },
                0,
                0,
                1,
                true,
                true,
                "Player 1",
                @"Player 1 Rolled: 1 Limit: 
Net amount hits: 0 Amount misses: 1
CRITICAL GLITCH!!
");
            Add(
                new ShadowRunRollResult
                {
                    DiceResults = new []{ 1, 1, 5 }
                },
                1,
                1,
                2,
                true,
                false,
                "Player 1",
                @"Player 1 Rolled: 1 1 5 Limit: 
Net amount hits: 1 Amount misses: 2
REGULAR GLITCH!!
");
            Add(
                new ShadowRunRollResult
                {
                    DiceResults = new []{ 1, 5, 6 }
                },
                2,
                2,
                1,
                false,
                false,
                "Player 1",
                @"Player 1 Rolled: 1 5 6 Limit: 
Net amount hits: 2 Amount misses: 1
");
            Add(
                new ShadowRunRollResult
                {
                    DiceResults = new []{ 1, 5, 6, 5 },
                    HitLimit = 1
                },
                3,
                1,
                1,
                false,
                false,
                "Player 1",
                @"Player 1 Rolled: 1 5 6 5 Limit: 1
Net amount hits: 1 Amount misses: 1
");
            Add(
                new ShadowRunRollResult
                {
                    DiceResults = new []{ 1, 5 },
                    HitLimit = 1
                },
                1,
                1,
                1,
                false,
                false,
                "Player 1",
                @"Player 1 Rolled: 1 5 Limit: 1
Net amount hits: 1 Amount misses: 1
");
            Add(
                new ShadowRunRollResult
                {
                    DiceResults = new []{ 1, 1 },
                    HitLimit = 1
                },
                0,
                0,
                2,
                true,
                true,
                "Player 1",
                @"Player 1 Rolled: 1 1 Limit: 1
Net amount hits: 0 Amount misses: 2
CRITICAL GLITCH!!
");
        }
    }

    [Theory]
    [ClassData(typeof(ShadowRunRollResultTestData))]
    public void ResultTests(ShadowRunRollResult resultItem,
        int expectedGrossHits, int expectedNetHits, int expectedMisses,
        bool expectedHasGlitched, bool expectedGlitchedCriticall,
        string player, string expectedOutput)
    {
        Assert.Equal(expectedGrossHits, resultItem.GrossAmountHits);
        Assert.Equal(expectedNetHits, resultItem.NetAmountHits);
        Assert.Equal(expectedMisses, resultItem.AmountMisses);
        Assert.Equal(expectedHasGlitched, resultItem.HasGlitched);
        Assert.Equal(expectedGlitchedCriticall, resultItem.HasGlitchedCritically);
        Assert.Equal(expectedOutput, resultItem.ToString(player));
    }

    private class ShadowRunRollResult_Filters_Invalid_Values_TestData : TheoryData<int[]?, int[]>
    {
        public ShadowRunRollResult_Filters_Invalid_Values_TestData()
        {
            Add([0], []);
            Add([7], []);
            Add([0, 1], [1]);
            Add([6, 7], [6]);
            Add([0, 1, 2, 3, 4, 5, 6, 7], [1, 2, 3, 4, 5, 6]);
            Add(null, []);
        }
    }

    [Theory]
    [ClassData(typeof(ShadowRunRollResult_Filters_Invalid_Values_TestData))]
    public void ShadowRunRollResult_Filters_Invalid_Values(int[] inputDiceResults, int[] expectedDiceResults)
    {
        var result = new ShadowRunRollResult()
        {
            DiceResults = inputDiceResults
        };
        Assert.Equivalent(expectedDiceResults, result.DiceResults);
        Assert.True(result.DiceResults.All(x => 1 <= x && x <= 6));
    }
}