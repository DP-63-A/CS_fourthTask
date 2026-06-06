using System;

namespace ShapesLib;

public class Square : Shape, IParsable<Square>
{
    public Point P1 { get; }
    public Point P2 { get; }
    public Point P3 { get; }
    public Point P4 { get; }

    public Square(Point p1, Point p2, Point p3, Point p4)
    {
        if (p1 == null || p2 == null || p3 == null || p4 == null)
            throw new ArgumentNullException("Square points cannot be null.");

        double d12 = p1.DistanceTo(p2);
        double d23 = p2.DistanceTo(p3);
        double d34 = p3.DistanceTo(p4);
        double d41 = p4.DistanceTo(p1);
        double d13 = p1.DistanceTo(p3);
        double d24 = p2.DistanceTo(p4);

        const double epsilon = 1e-9;
        bool isValidSquare = false;

        if (Math.Abs(d12 - d23) < epsilon && Math.Abs(d23 - d34) < epsilon &&
            Math.Abs(d34 - d41) < epsilon && Math.Abs(d13 - d24) < epsilon)
        {
            isValidSquare = true;
        }
        else if (Math.Abs(p1.DistanceTo(p3) - p3.DistanceTo(p2)) < epsilon &&
                 Math.Abs(p3.DistanceTo(p2) - p2.DistanceTo(p4)) < epsilon &&
                 Math.Abs(p2.DistanceTo(p4) - p4.DistanceTo(p1)) < epsilon &&
                 Math.Abs(d12 - d34) < epsilon)
        {
            isValidSquare = true;
        }

        if (!isValidSquare)
        {
            throw new FormatException("It is not square with these points");
        }

        P1 = p1; P2 = p2; P3 = p3; P4 = p4;
    }

    public double GetSide()
    {
        double d12 = P1.DistanceTo(P2);
        double d13 = P1.DistanceTo(P3);
        double d14 = P1.DistanceTo(P4);

        return Math.Min(d12, Math.Min(d13, d14));
    }

    public override double GetPerimeter()
    {
        return 4 * GetSide();
    }

    public override double GetArea()
    {
        double side = GetSide();
        return side * side;
    }

    public override string ToString()
    {
        return $"Square: P1({P1.X},{P1.Y}), P2({P2.X},{P2.Y}), P3({P3.X},{P3.Y}), P4({P4.X},{P4.Y})";
    }

    public static Square Parse(string s, IFormatProvider? provider = null)
    {
        if (s == null) throw new ArgumentNullException(nameof(s));
        if (string.IsNullOrWhiteSpace(s)) throw new ArgumentException("String is empty", nameof(s));

        try
        {
            string geomPart = s.Split('|')[0];
            string[] pointStrings = geomPart.Split(';');

            if (pointStrings.Length != 4)
                throw new FormatException("There should be four points in square");

            Point p1 = Point.Parse(pointStrings[0]);
            Point p2 = Point.Parse(pointStrings[1]);
            Point p3 = Point.Parse(pointStrings[2]);
            Point p4 = Point.Parse(pointStrings[3]);

            return new Square(p1, p2, p3, p4);
        }
        catch (Exception ex) when (ex is not ArgumentNullException && ex is not ArgumentException)
        {
            throw new FormatException("Wrong parameter string format for square", ex);
        }
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Square result)
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