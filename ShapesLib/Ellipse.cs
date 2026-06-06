using System;

namespace ShapesLib;

public class Ellipse : Shape, IParsable<Ellipse>
{
    public Point F1 { get; }
    public Point F2 { get; }
    public double SemiMajorAxis { get; }
    public double SemiMinorAxis { get; }

    public Ellipse(Point f1, Point f2, double a)
    {
        if (f1 == null || f2 == null)
            throw new ArgumentNullException("Focuses cannot be null");
        if (a <= 0)
            throw new FormatException("Major semiaxis should be bigger than zero");

        double c = f1.DistanceTo(f2) / 2.0;

        if (a <= c + 1e-9)
            throw new FormatException("Major semiaxis (a) must be bigger than half distance between focuses (c)");

        F1 = f1;
        F2 = f2;
        SemiMajorAxis = a;
        SemiMinorAxis = Math.Sqrt(a * a - c * c);
    }

    public override double GetArea()
    {
        return Math.PI * SemiMajorAxis * SemiMinorAxis;
    }

    public override double GetPerimeter()
    {
        double a = SemiMajorAxis;
        double b = SemiMinorAxis;
        return Math.PI * (3 * (a + b) - Math.Sqrt((3 * a + b) * (a + 3 * b)));
    }

    public override string ToString()
    {
        return $"Ellipse: Focus1({F1.X},{F1.Y}), Focus2({F2.X},{F2.Y}), MajorAxis={SemiMajorAxis}";
    }

    public static Ellipse Parse(string s, IFormatProvider? provider = null)
    {
        if (s == null) throw new ArgumentNullException(nameof(s));
        if (string.IsNullOrWhiteSpace(s)) throw new ArgumentException("Empty string", nameof(s));

        try
        {
            string geomPart = s.Split('|')[0];
            string[] parts = geomPart.Split(';');

            if (parts.Length != 3)
                throw new FormatException("There should be two focuses and major semiaxis splitted with ;");

            Point f1 = Point.Parse(parts[0]);
            Point f2 = Point.Parse(parts[1]);
            double a = double.Parse(parts[2]);

            return new Ellipse(f1, f2, a);
        }
        catch (Exception ex) when (ex is not ArgumentNullException && ex is not ArgumentException)
        {
            throw new FormatException("Wrong parameter string format for ellipse", ex);
        }
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Ellipse result)
    {
        try { result = Parse(s!, provider); return true; } catch { result = null!; return false; }
    }
}