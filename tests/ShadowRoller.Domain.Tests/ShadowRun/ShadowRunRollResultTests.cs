namespace ShadowRoller.Domain.ShadowRun.Tests;

using System.Collections.Generic;
using ShadowRoller.Domain.Contexts.ShadowRun;
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
                true
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
                false
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
                false
            },
        };
    }

    [Theory]
    [MemberData(nameof(ResultTestsValues))]
    public void ResultTests(ShadowRunRollResult resultItem, int expectedGrossHits, int expectedNetHits, int expectedMisses, bool expectedHasGlitched, bool expectedGlitchedCriticall)
    {
        Assert.Equal(expectedGrossHits, resultItem.GrossAmountHits);
        Assert.Equal(expectedNetHits, resultItem.NetAmountHits);
        Assert.Equal(expectedMisses, resultItem.AmountMisses);
        Assert.Equal(expectedHasGlitched, resultItem.HasGlitched);
        Assert.Equal(expectedGlitchedCriticall, resultItem.HasGlitchedCritically);
    }   
}