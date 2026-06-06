using System;

namespace ShapesLib;

public class Circle : Shape, IParsable<Circle>
{
    public Point Center { get; }
    public double Radius { get; }

    public Circle(Point center, double radius)
    {
        if (radius <= 0)
            throw new FormatException("Circle radius must be greater than 0");

        Center = center;
        Radius = radius;
    }

    public override double GetArea() => Math.PI * Radius * Radius;

    public override double GetPerimeter() => 2 * Math.PI * Radius;

    public override string ToString() => $"Circle: Center({Center.X},{Center.Y}), Radius={Radius}";

    public static Circle Parse(string s, IFormatProvider? provider = null)
    {
        if (s == null) throw new ArgumentNullException(nameof(s));
        if (string.IsNullOrWhiteSpace(s)) throw new ArgumentException("String cannot be empty", nameof(s));

        try
        {
            string geomPart = s.Split('|')[0];

            if (!geomPart.Contains(";"))
            {
                double r = double.Parse(geomPart);
                return new Circle(new Point(0, 0), r);
            }

            string[] parts = geomPart.Split(';');
            Point center = Point.Parse(parts[0]);
            double radius = double.Parse(parts[1]);

            return new Circle(center, radius);
        }
        catch (Exception ex) when (ex is not ArgumentNullException)
        {
            throw new FormatException("Circle parsing error", ex);
        }
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Circle result)
    {
        try
        {
            result = Parse(s!, provider);
            return true;
        }
        catch
        {
            result = null!;
            return false;
        }
    }
}