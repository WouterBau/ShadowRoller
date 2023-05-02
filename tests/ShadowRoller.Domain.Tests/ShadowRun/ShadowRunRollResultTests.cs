namespace ShadowRoller.Domain.ShadowRun.Tests;

using ShadowRoller.Domain.Contexts.ShadowRun;
using System.Collections.Generic;
using Xunit;

public class ShadowRunRollResultTests
{
    public static IEnumerable<object[]> ResultTestsValues()
    {
        return new List<object[]>
        {
            new object[]
            {
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
"
            },
            new object[]
            {
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
"
            },
            new object[]
            {
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
"
            },
            new object[]
            {
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
"
            },
        };
    }

    [Theory]
    [MemberData(nameof(ResultTestsValues))]
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

    [Theory]
    [InlineData(new[] { 0, 1 }, new[] { 1 })]
    [InlineData(new[] { 6, 7 }, new[] { 6 })]
    [InlineData(new[] { 0, 1, 2, 3, 4, 5, 6, 7 }, new[] { 1, 2, 3, 4, 5, 6 })]
    [InlineData(null, new int[] { })]
    public void ShadowRunRollResult_Filters_Invalid_Values(int[] inputDiceResults, int[] expectedDiceResults)
    {
        var result = new ShadowRunRollResult()
        {
            DiceResults = inputDiceResults
        };
        Assert.Equivalent(expectedDiceResults, result.DiceResults);
    }
}