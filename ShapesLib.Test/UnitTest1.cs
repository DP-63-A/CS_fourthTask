using System;
using NUnit.Framework;
using ShapesLib;

namespace ShapesLib.Test;

[TestFixture]
public class ShapeTests
{
    private const double Precision = 1e-4;

    [TestCase("5|Red", 31.4159, 78.5398)]
    [TestCase("2,3;4|Green", 25.1327, 50.2655)]
    public void Circle_ValidParam_CalculatesCorrectly(string param, double expectedPerimeter, double expectedArea)
    {
        var circle = Circle.Parse(param);
        Assert.AreEqual(expectedPerimeter, circle.GetPerimeter(), Precision, "Circle perimeter is incorrect");
        Assert.AreEqual(expectedArea, circle.GetArea(), Precision, "Circle square is incorrect");
    }

    [TestCase("0,0;3,0;0,4|Blue", 12.0, 6.0)]
    public void Triangle_ValidParam_CalculatesCorrectly(string param, double expectedPerimeter, double expectedArea)
    {
        var triangle = Triangle.Parse(param);
        Assert.AreEqual(expectedPerimeter, triangle.GetPerimeter(), Precision, "Triangle perimeter is incorrect");
        Assert.AreEqual(expectedArea, triangle.GetArea(), Precision, "Triangle square is incorrect");
    }

    [TestCase("0,0;0,2;2,2;2,0|Green", 8.0, 4.0)]
    public void Square_ValidParam_CalculatesCorrectly(string param, double expectedPerimeter, double expectedArea)
    {
        var square = Square.Parse(param);
        Assert.AreEqual(expectedPerimeter, square.GetPerimeter(), Precision, "Square perimeter is incorrect");
        Assert.AreEqual(expectedArea, square.GetArea(), Precision, "Square square is incorrect");
    }

    [TestCase("0,0;0,3;4,3;4,0|Yellow", 14.0, 12.0)]
    public void Rectangle_ValidParam_CalculatesCorrectly(string param, double expectedPerimeter, double expectedArea)
    {
        var rect = Rectangle.Parse(param);
        Assert.AreEqual(expectedPerimeter, rect.GetPerimeter(), Precision, "Rectangle perimeter is incorrect");
        Assert.AreEqual(expectedArea, rect.GetArea(), Precision, "Rectangle square is incorrect");
    }

    [TestCase("0,1;3,5;6,1;3,-3|Purple", 20.0, 24.0)]
    public void Rhomb_ValidParam_CalculatesCorrectly(string param, double expectedPerimeter, double expectedArea)
    {
        var rhomb = Rhomb.Parse(param);
        Assert.AreEqual(expectedPerimeter, rhomb.GetPerimeter(), Precision, "Rhomb perimeter is incorrect");
        Assert.AreEqual(expectedArea, rhomb.GetArea(), Precision, "Rhomb square is incorrect");
    }

    [TestCase("-4,0;4,0;5|Yellow", 25.5270, 47.1239)]
    public void Ellipse_ValidParam_CalculatesCorrectly(string param, double expectedPerimeter, double expectedArea)
    {
        var ellipse = Ellipse.Parse(param);
        Assert.AreEqual(expectedPerimeter, ellipse.GetPerimeter(), Precision, "Ellipse perimeter is incorrect");
        Assert.AreEqual(expectedArea, ellipse.GetArea(), Precision, "Ellipse square is incorrect");
    }

    [Test]
    public void Circle_ToString_ReturnsCorrectFormat()
    {
        var circle = Circle.Parse("0,0;5");
        Assert.AreEqual("Circle: Center(0,0), Radius=5", circle.ToString());
    }

    [Test]
    public void Triangle_DegeneratePoints_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(() => Triangle.Parse("0,0;0,0;5,5"));
    }

    [Test]
    public void Rectangle_IsActuallySquare_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(() => Rectangle.Parse("0,0;0,2;2,2;2,0"));
    }

    [TestCase(null)]
    public void Parse_NullArgument_ThrowsArgumentNullException(string nullStr)
    {
        Assert.Throws<ArgumentNullException>(() => Circle.Parse(nullStr!));
    }

    [TestCase("   ")]
    [TestCase("")]
    public void Parse_EmptyArgument_ThrowsArgumentException(string emptyStr)
    {
        Assert.Throws<ArgumentException>(() => Circle.Parse(emptyStr));
    }
}