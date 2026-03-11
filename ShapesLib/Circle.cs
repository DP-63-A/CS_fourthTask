using System;

namespace ShapesLib;

public class Circle : Shape, IParsable<Circle>
{
    public override double GetArea()
    {
        throw new NotImplementedException();
    }

    public override double GetPerimeter()
    {
        throw new NotImplementedException();
    }

    public static Circle Parse(string s, IFormatProvider provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(string s, IFormatProvider provider, out Circle result)
    {
        throw new NotImplementedException();
    }
}