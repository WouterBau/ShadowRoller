using ShadowRoller.Domain.Contexts.Dice;
using Xunit;

namespace ShadowRoller.Domain.Tests.Dice;

public class DiceRollResultTests
{
    private class DiceRollResultTestData : TheoryData<DiceModifierSumRollResult, int, string, string> {
        public DiceRollResultTestData()
        {
            Add(
                new DiceModifierSumRollResult
                {
                    DiceResults = [1, 5, 6]
                },
                12,
                "Player 1",
                @"Player 1 Rolled: 1 5 6 Modifiers: 
Result: 12
"
            );
            Add(
                new DiceModifierSumRollResult
                {
                    DiceResults = [1, 5, 6, 5],
                    Modifiers = [1, -2]
                },
                16,
                "Player 1",
                @"Player 1 Rolled: 1 5 6 5 Modifiers: 1 -2
Result: 16
"
            );
            Add(
                new DiceModifierSumRollResult
                {
                    Modifiers = [1, -2]
                },
                -1,
                "Player 1",
                @"Player 1 Rolled:  Modifiers: 1 -2
Result: -1
"
            );
        }
    }


    [Theory]
    [ClassData(typeof(DiceRollResultTestData))]
    public void ResultTests(DiceModifierSumRollResult resultItem,
        int expectedResult, string player, string expectedOutput)
    {
        Assert.Equal(expectedResult, resultItem.Result);
        Assert.Equal(expectedOutput, resultItem.ToString(player));
    }
}