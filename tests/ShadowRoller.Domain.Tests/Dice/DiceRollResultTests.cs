using ShadowRoller.Domain.Contexts.Dice;
using System.Collections.Generic;
using Xunit;

namespace ShadowRoller.Domain.Tests.Dice;

public class DiceRollResultTests
{
    public static IEnumerable<object[]> ResultTestsValues()
    {
        return new List<object[]>
        {
            new object[]
            {
                new DiceModifierSumRollResult
                {
                    DiceResults = new []{ 1, 5, 6 }
                },
                12,
                "Player 1",
                @"Player 1 Rolled: 1 5 6 Modifiers: 
Result: 13
"
            },
            new object[]
            {
                new DiceModifierSumRollResult
                {
                    DiceResults = new []{ 1, 5, 6, 5 },
                    Modifiers = new [] { 1, -2 }
                },
                16,
                "Player 1",
                @"Player 1 Rolled: 1 5 6 5 Modifiers: 1 -2
Result: 16
"
            },
            new object[]
            {
                new DiceModifierSumRollResult
                {
                    Modifiers = new [] { 1, -2 }
                },
                -1,
                "Player 1",
                @"Player 1 Rolled:  Modifiers: 1 -2
Result: -1
"
            },
        };
    }

    [Theory]
    [MemberData(nameof(ResultTestsValues))]
    public void ResultTests(DiceModifierSumRollResult resultItem,
        int expectedResult, string player, string expectedOutput)
    {
        Assert.Equal(expectedResult, resultItem.Result);
        Assert.Equal(expectedOutput, resultItem.ToString(player));
    }
}