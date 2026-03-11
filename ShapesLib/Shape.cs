using System;

namespace ShapesLib;

public abstract class Shape
{
    public Shape()
    {
    }

    public Shape(int n)
    {
    }

    public abstract double GetArea();

    public abstract double GetPerimeter();

}
