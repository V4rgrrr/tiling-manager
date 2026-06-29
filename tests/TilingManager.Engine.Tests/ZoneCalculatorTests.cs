using TilingManager.Core.Models;

namespace TilingManager.Engine.Tests;

public class ZoneCalculatorTests
{
    [Fact]
    public void Should_Calculate_Correct_Pixels_For_Left_Half_Of_Screen()
    {
        // Arrange
        var calculator = new ZoneCalculator();
        
        var zone = new Zone { X = 0.0, Y = 0.0, Width = 0.5, Height = 1.0 };
        int mockScreenWidth = 1920;
        int mockScreenHeight = 1080;
        
        // Act
        var (x, y, width, height) = calculator.CalculateAbsoluteCoordinates(zone, mockScreenWidth, mockScreenHeight);
        
        // Assert
        Assert.Equal(0, x);
        Assert.Equal(0, y);
        Assert.Equal(960, width); // 1920 * 0.5 = 960
        Assert.Equal(1080, height); // 1080 * 1.0 = 1080
    }
}