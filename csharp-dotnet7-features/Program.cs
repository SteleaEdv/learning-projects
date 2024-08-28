// Global usings introduced in .NET 6/7
global using System;
global using System.Text.Json;
global using System.Linq;
global using System.Text.RegularExpressions;
using System.Numerics;

namespace DotNet7Features;

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to .NET 7 new features demonstration!");

        // Example of a new feature: Improved JSON Serialization with 'JsonSerializer'
        var person = new Person("John Doe", 30);
        string jsonString = JsonSerializer.Serialize(person);
        Console.WriteLine($"Serialized JSON: {jsonString}");

        // Improved DateOnly and TimeOnly types
        var date = DateOnly.FromDateTime(DateTime.Now);
        var time = TimeOnly.FromDateTime(DateTime.Now);
        Console.WriteLine($"Current Date: {date}, Current Time: {time}");

        // Improved LINQ performance and enhancements
        var numbers = Enumerable.Range(1, 10);
        var evenNumbers = numbers.Where(n => n % 2 == 0);
        Console.WriteLine("Even numbers using LINQ: " + string.Join(", ", evenNumbers));

        // New Regex Source Generators
        Regex regex = new Regex(@"\b\w{3,}\b"); // Matches words with 3 or more characters
        var matches = regex.Matches("This is a sample text with some long words.");
        Console.WriteLine("Words with 3 or more characters:");
        foreach (Match match in matches)
        {
            Console.WriteLine(match.Value);
        }

        // Required Members in Classes and Records
        var car = new Car { Make = "Tesla", Model = "Model S" };
        Console.WriteLine($"Car: {car.Make} {car.Model}");

        // Numeric IntPtr and UIntPtr
        IntPtr pointer = (IntPtr)123456;
        Console.WriteLine($"Numeric IntPtr value: {pointer}");

        // Extended Random API
        Random random = new();
        Console.WriteLine("Random number (0-99): " + random.NextInt64(0, 100));

        // Pattern Matching Enhancements
        object shape = new Circle(5);
        if (shape is Circle { Radius: > 0 } circle)
        {
            Console.WriteLine($"Circle with radius: {circle.Radius}");
        }

        // Static Abstract Members in Interfaces Example
        var celsius = new Celsius { Degrees = 25 };
        var fahrenheit = Converter<Celsius, Fahrenheit>.Convert(celsius);
        Console.WriteLine($"Temperature in Fahrenheit: {fahrenheit.Degrees}");

        // Generic Math Support
        var sum = MathOperations.Add(10, 20);
        Console.WriteLine($"Sum using generic math support: {sum}");

        // Span<char> Enhancements
        var sampleText = "Hello, .NET 7!";
        ReadOnlySpan<char> span = sampleText.AsSpan();
        Console.WriteLine($"First 5 characters using Span<char>: {span.Slice(0, 5).ToString()}");

        // UTF-8 String Literals
        ReadOnlySpan<byte> utf8Literal = "Hello, UTF-8!"u8;
        Console.WriteLine($"UTF-8 String Literal: {System.Text.Encoding.UTF8.GetString(utf8Literal)}");
    }

    // Simple record type for demonstration
    record Person(string Name, int Age);

    // Demonstrate required members in a class
    class Car
    {
        public required string Make { get; init; }
        public required string Model { get; init; }
    }

    // Pattern Matching Enhancements Example
    record Circle(double Radius);

    // Static Abstract Members in Interfaces Example
    interface ITemperature<T>
    {
        public double Degrees { get; set; }
        public static abstract T FromFahrenheit(double degrees);
        public static abstract T ToFahrenheit(double degrees);
    }

    class Celsius : ITemperature<Celsius>
    {
        public double Degrees { get; set; }
        public static Celsius FromFahrenheit(double degrees) => new Celsius { Degrees = (degrees - 32) * 5 / 9 };
        public static Celsius ToFahrenheit(double degrees) => new Celsius { Degrees = degrees * 9 / 5 + 32 };
    }

    class Fahrenheit : ITemperature<Fahrenheit>
    {
        public double Degrees { get; set; }
        public static Fahrenheit FromFahrenheit(double degrees) => new Fahrenheit { Degrees = degrees };
        public static Fahrenheit ToFahrenheit(double degrees) => new Fahrenheit { Degrees = degrees };
    }

    static class Converter<TFrom, TTo>
        where TFrom : ITemperature<TFrom>
        where TTo : ITemperature<TTo>, new()
    {
        public static TTo Convert(TFrom from)
        {
            // Convert the temperature to Fahrenheit first
            double fahrenheitDegrees = TFrom.ToFahrenheit(from.Degrees).Degrees;
            // Now create an instance of TTo with the Fahrenheit degrees
            return new() { Degrees = TTo.FromFahrenheit(fahrenheitDegrees).Degrees };
        }
    }

    // Generic Math Support Example
    static class MathOperations
    {
        public static T Add<T>(T left, T right) where T : INumber<T> => left + right;
    }
}
