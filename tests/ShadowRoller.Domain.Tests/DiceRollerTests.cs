namespace ShadowRoller.Domain.Tests;

using System;
using System.Linq;
using ShadowRoller.Domain;
using Xunit;

public class DiceRollerTests
{
    [Fact]
    public void NoZeroMaxValueAllowed()
    {
        Assert.Throws<ArgumentException>(() => DiceRoller.RollSeparately(1, 0));
    }

    [Fact]
    public void NoNegativeMaxValueAllowed()
    {
        Assert.Throws<ArgumentException>(() => DiceRoller.RollSeparately(1, -1));
    }

    [Theory]
    [InlineData(3, 3, 3)]
    [InlineData(1, 3, 1)]
    [InlineData(0, 3, 0)]
    [InlineData(-1, 3, 0)]
    public void ZeroAmount(int amount, int maxValue, int expectedAmountValues)
    {
        var actualValues = DiceRoller.RollSeparately(amount, maxValue);
        var actualAmountValues = actualValues.Count();
        Assert.Equal(actualAmountValues, expectedAmountValues);
    }

}