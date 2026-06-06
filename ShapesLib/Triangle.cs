using System;

namespace ShapesLib;

public class Triangle : Shape, IParsable<Triangle>
{
    public Point A { get; }
    public Point B { get; }
    public Point C { get; }

    public Triangle(Point a, Point b, Point c)
    {
        if (a == null || b == null || c == null)
            throw new ArgumentNullException("Triangle points cannot be null");

        double sideA = b.DistanceTo(c);
        double sideB = a.DistanceTo(c);
        double sideC = a.DistanceTo(b);

        const double epsilon = 1e-9;
        if (sideA + sideB <= sideC + epsilon ||
            sideA + sideC <= sideB + epsilon ||
            sideB + sideC <= sideA + epsilon)
        {
            throw new FormatException("Triangle became a line or does not exist");
        }

        A = a;
        B = b;
        C = c;
    }

    public override double GetPerimeter()
    {
        return A.DistanceTo(B) + B.DistanceTo(C) + C.DistanceTo(A);
    }

    public override double GetArea()
    {
        double a = B.DistanceTo(C);
        double b = A.DistanceTo(C);
        double c = A.DistanceTo(B);
        double p = GetPerimeter() / 2;

        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    public override string ToString()
    {
        return $"Triangle: A({A.X},{A.Y}), B({B.X},{B.Y}), C({C.X},{C.Y})";
    }

    public static Triangle Parse(string s, IFormatProvider? provider = null)
    {
        if (s == null) throw new ArgumentNullException(nameof(s));
        if (string.IsNullOrWhiteSpace(s)) throw new ArgumentException("An empty string", nameof(s));

        try
        {
            string geomPart = s.Split('|')[0];
            string[] pointStrings = geomPart.Split(';');

            if (pointStrings.Length != 3)
                throw new FormatException("Triangle should have 3 (three) points");

            Point p1 = Point.Parse(pointStrings[0]);
            Point p2 = Point.Parse(pointStrings[1]);
            Point p3 = Point.Parse(pointStrings[2]);

            return new Triangle(p1, p2, p3);
        }
        catch (Exception ex) when (ex is not ArgumentNullException && ex is not ArgumentException)
        {
            throw new FormatException("Wrong parameter string format for triangle", ex);
        }
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Triangle result)
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