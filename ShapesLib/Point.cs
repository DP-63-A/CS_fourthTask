using System;

namespace ShapesLib;

public class Point
{
    public double X { get; set; }
    public double Y { get; set; }

    public Point(double x, double y) { X = x; Y = y; }

    public static Point Parse(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) throw new ArgumentNullException(nameof(s));
        string[] parts = s.Split(',');
        if (parts.Length != 2) throw new FormatException("Wrong point format");

        return new Point(double.Parse(parts[0]), double.Parse(parts[1]));
    }

    public double DistanceTo(Point other)
    {
        return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
    }
}