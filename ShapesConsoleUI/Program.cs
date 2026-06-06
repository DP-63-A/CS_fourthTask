using ShapesConsoleUI;
using ShapesLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ShapesEducationProject;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Path to JSON file was not given");
            Console.WriteLine("Please write: ShapesEducationProject <path_to_file.json>");
            return;
        }

        string filePath = args[0];

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File in '{filePath}' not found");
            return;
        }

        try
        {
            string jsonString = File.ReadAllText(filePath);
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var rawShapes = JsonSerializer.Deserialize<List<ShapeJsonDto>>(jsonString, jsonOptions);

            if (rawShapes == null || rawShapes.Count == 0)
            {
                Console.WriteLine("File is empty or incorrect");
                return;
            }

            List<Shape> shapes = new List<Shape>();

            foreach (var raw in rawShapes)
            {
                try
                {
                    Shape shape = raw.Name.Trim() switch
                    {
                        "Circle" => Circle.Parse(raw.Param),
                        "Triangle" => Triangle.Parse(raw.Param),
                        "Rectangle" => Rectangle.Parse(raw.Param),
                        "Square" => Square.Parse(raw.Param),
                        "Rhomb" => Rhomb.Parse(raw.Param),
                        "Ellipse" => Ellipse.Parse(raw.Param),

                        _ => throw new NotSupportedException($"Figure '{raw.Name}' is not supported")
                    };

                    shapes.Add(shape);
                }
                catch (NotSupportedException ex)
                {
                    Console.WriteLine($"Type exception: {ex.Message} ({raw.Description})");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Format exception: Unable to create {raw.Name} because: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[НЕПРЕДВИДЕННАЯ ОШИБКА] для {raw.Name}: {ex.Message}");
                }
            }

            Console.WriteLine("\n+++ Shapes processing results +++");
            Console.WriteLine(new string('-', 60));

            foreach (var shape in shapes)
            {
                Console.WriteLine(shape.ToString());
                Console.WriteLine($"Perimeter: {Math.Round(shape.GetPerimeter(), 4)}");
                Console.WriteLine($"Square:  {Math.Round(shape.GetArea(), 4)}");
                Console.WriteLine(new string('-', 60));
            }
        }
        catch (JsonException)
        {
            Console.WriteLine("File has incorrect structure or damaged");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Critical error occured: {ex.Message}");
        }
    }
}