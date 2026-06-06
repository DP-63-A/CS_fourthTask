using System;

namespace ShapesLib;

public class Rhomb : Shape, IParsable<Rhomb>
{
    public Point P1 { get; }
    public Point P2 { get; }
    public Point P3 { get; }
    public Point P4 { get; }

    public Rhomb(Point p1, Point p2, Point p3, Point p4)
    {
        if (p1 == null || p2 == null || p3 == null || p4 == null)
            throw new ArgumentNullException("Rhomb points cannot be null.");

        double d12 = p1.DistanceTo(p2);
        double d23 = p2.DistanceTo(p3);
        double d34 = p3.DistanceTo(p4);
        double d41 = p4.DistanceTo(p1);
        double d13 = p1.DistanceTo(p3);
        double d24 = p2.DistanceTo(p4);

        const double epsilon = 1e-9;
        bool isValidRhomb = false;
        bool isSquare = false;

        if (Math.Abs(d12 - d23) < epsilon && Math.Abs(d23 - d34) < epsilon && Math.Abs(d34 - d41) < epsilon)
        {
            isValidRhomb = true;
            if (Math.Abs(d13 - d24) < epsilon) isSquare = true;
        }
        else if (Math.Abs(p1.DistanceTo(p3) - p3.DistanceTo(p2)) < epsilon &&
                 Math.Abs(p3.DistanceTo(p2) - p2.DistanceTo(p4)) < epsilon &&
                 Math.Abs(p2.DistanceTo(p4) - p4.DistanceTo(p1)) < epsilon)
        {
            isValidRhomb = true;
            if (Math.Abs(d12 - d34) < epsilon) isSquare = true;
        }

        if (!isValidRhomb)
            throw new FormatException("These points dont make rhomb");

        if (isSquare)
            throw new FormatException("This rhomb seems to be a square");

        P1 = p1; P2 = p2; P3 = p3; P4 = p4;
    }

    private double GetSide() => Math.Min(P1.DistanceTo(P2), P1.DistanceTo(P3));

    public override double GetPerimeter() => 4 * GetSide();

    public override double GetArea()
    {
        double[] lines = { P1.DistanceTo(P2), P1.DistanceTo(P3), P1.DistanceTo(P4) };
        Array.Sort(lines);

        double d13 = P1.DistanceTo(P3);
        double d24 = P2.DistanceTo(P4);
        double d12 = P1.DistanceTo(P2);
        double d34 = P3.DistanceTo(P4);

        double diag1 = d13;
        double diag2 = d24;

        if (Math.Abs(d13 - GetSide()) < 1e-9)
        {
            diag1 = d12;
            diag2 = d34;
        }

        return (diag1 * diag2) / 2.0;
    }

    public override string ToString() => $"Rhomb: P1({P1.X},{P1.Y}), P2({P2.X},{P2.Y}), P3({P3.X},{P3.Y}), P4({P4.X},{P4.Y})";

    public static Rhomb Parse(string s, IFormatProvider? provider = null)
    {
        if (s == null) throw new ArgumentNullException(nameof(s));
        if (string.IsNullOrWhiteSpace(s)) throw new ArgumentException("Empty string", nameof(s));

        try
        {
            string geomPart = s.Split('|')[0];
            string[] pointStrings = geomPart.Split(';');

            if (pointStrings.Length != 4)
                throw new FormatException("There should be four points for rhomb");

            return new Rhomb(Point.Parse(pointStrings[0]), Point.Parse(pointStrings[1]),
                               Point.Parse(pointStrings[2]), Point.Parse(pointStrings[3]));
        }
        catch (Exception ex) when (ex is not ArgumentNullException && ex is not ArgumentException)
        {
            throw new FormatException("Wrong parameter string format for rhomb", ex);
        }
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Rhomb result)
    {
        try { result = Parse(s!, provider); return true; } catch { result = null!; return false; }
    }
}