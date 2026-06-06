using System;

namespace ShapesLib;

public class Rectangle : Shape, IParsable<Rectangle>
{
    public Point P1 { get; }
    public Point P2 { get; }
    public Point P3 { get; }
    public Point P4 { get; }

    public Rectangle(Point p1, Point p2, Point p3, Point p4)
    {
        if (p1 == null || p2 == null || p3 == null || p4 == null)
            throw new ArgumentNullException("Rectangle points cannot be null.");

        double d12 = p1.DistanceTo(p2);
        double d23 = p2.DistanceTo(p3);
        double d34 = p3.DistanceTo(p4);
        double d41 = p4.DistanceTo(p1);
        double d13 = p1.DistanceTo(p3);
        double d24 = p2.DistanceTo(p4);

        const double epsilon = 1e-9;
        bool isValidRectangle = false;
        bool isSquare = false;

        if (Math.Abs(d12 - d34) < epsilon && Math.Abs(d23 - d41) < epsilon && Math.Abs(d13 - d24) < epsilon)
        {
            isValidRectangle = true;
            if (Math.Abs(d12 - d23) < epsilon) isSquare = true;
        }

        else if (Math.Abs(d13 - d24) < epsilon && Math.Abs(d23 - d41) < epsilon && Math.Abs(d12 - d34) < epsilon)
        {
            isValidRectangle = true;
            if (Math.Abs(d13 - d23) < epsilon) isSquare = true;
            d23 = d23;
        }

        if (!isValidRectangle)
            throw new FormatException("These points does not make a rectangle");

        if (isSquare)
            throw new FormatException("These points make a square, not a rectangle");

        P1 = p1; P2 = p2; P3 = p3; P4 = p4;
    }

    public override double GetPerimeter()
    {
        double side1 = P1.DistanceTo(P2);
        double side2 = P2.DistanceTo(P3);

        if (Math.Abs(P1.DistanceTo(P3) - P2.DistanceTo(P4)) < 1e-9 && P1.DistanceTo(P2) > P1.DistanceTo(P3))
        {
            side1 = P1.DistanceTo(P3);
            side2 = P3.DistanceTo(P2);
        }

        return 2 * (side1 + side2);
    }

    public override double GetArea()
    {
        double[] sides = { P1.DistanceTo(P2), P1.DistanceTo(P3), P1.DistanceTo(P4) };
        Array.Sort(sides);

        return sides[0] * sides[1];
    }

    public override string ToString()
    {
        return $"Rectangle: P1({P1.X},{P1.Y}), P2({P2.X},{P2.Y}), P3({P3.X},{P3.Y}), P4({P4.X},{P4.Y})";
    }

    public static Rectangle Parse(string s, IFormatProvider? provider = null)
    {
        if (s == null) throw new ArgumentNullException(nameof(s));
        if (string.IsNullOrWhiteSpace(s)) throw new ArgumentException("Empty string", nameof(s));

        try
        {
            string geomPart = s.Split('|')[0];
            string[] pointStrings = geomPart.Split(';');

            if (pointStrings.Length != 4)
                throw new FormatException("There should be four points in rectangle");

            Point p1 = Point.Parse(pointStrings[0]);
            Point p2 = Point.Parse(pointStrings[1]);
            Point p3 = Point.Parse(pointStrings[2]);
            Point p4 = Point.Parse(pointStrings[3]);

            return new Rectangle(p1, p2, p3, p4);
        }
        catch (Exception ex) when (ex is not ArgumentNullException && ex is not ArgumentException)
        {
            throw new FormatException("Wrong parameter string format for rectangle", ex);
        }
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Rectangle result)
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