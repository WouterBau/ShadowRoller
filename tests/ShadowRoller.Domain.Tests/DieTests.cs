using Xunit;

namespace ShadowRoller.Domain.Tests;
public class DieTests
{
    [Fact]
    public void NoZeroAmountSidesAllowed()
    {
        Assert.Throws<InvalidAmountSidesException>(
            () => new Die(0)
        );
    }

    [Fact]
    public void NoNegativeMaxValueAllowed()
    {
        Assert.Throws<InvalidAmountSidesException>(
            () => new Die(-1)
        );
    }

    [Theory]
    [InlineData(3)]
    [InlineData(1)]
    public void SetAmountSides(int amountSides)
    {
        var die = new Die(amountSides);
        Assert.Equal(amountSides, die.AmountSides);
    }

    [Fact]
    public void LastResultEmptyAtStart()
    {
        var die = new Die(3);
        Assert.False(die.LastResult.HasValue);
    }

    [Fact]
    public void LastResultStoredAfterRoll()
    {
        var die = new Die(3);
        var expected = die.Roll();
        Assert.True(die.LastResult.HasValue);
        if (die.LastResult.HasValue)
            Assert.Equal(expected, die.LastResult.Value);
    }

}