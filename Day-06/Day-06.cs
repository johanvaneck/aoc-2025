using System.Text.RegularExpressions;

namespace Aoc2025;

public static class Day06
{
    public static void Run()
    {
        // Read file into string
        var input = File.ReadAllText("Day-06/input.txt");

        // Run part 1
        var result = Part01(input);

        // Run part 2
        var result2 = Part02(input);

        // Print result
        Console.WriteLine($"Part01: {result}");
        Console.WriteLine($"Part02: {result2}");
    }

    public static long Part01(string input)
    {
        var equations = input
            .Split("\n")
            .Where(line => line != "")
            .Select(line =>
                    Regex.Split(line.Trim(), @"\s+")
                    .Select(s => s.Trim())
                    .ToArray()
                    ).ToArray();

        long sum = 0;
        for (int iCol = 0; iCol < equations[0].Length; iCol++)
        {
            var operation = equations[equations.Length - 1][iCol].Trim();
            long answer = operation == "*" ? 1 : 0;
            for (int iRow = 0; iRow < equations.Length - 1; iRow++)
            {
                var valueString = equations[iRow][iCol].Trim();
                var value = long.Parse(valueString);
                if (operation == "+")
                {
                    answer += value;
                }
                else if (operation == "*")
                {
                    answer *= value;
                }
                else
                {
                    throw new Exception($"operation: {operation}");
                }
            ;
            }
            sum += answer;
        }
        return sum;
    }

    public static long Part02(string input)
    {
        var lines = input
            .Split("\n")
            .Where(line => line != "")
            .ToArray();

        string operation = "";
        var values = new List<long>();
        var equations = new List<(string, List<long>)>();
        var maxLineLength = lines.Max(s => s.Length);
        for (int iCol = 0; iCol < maxLineLength; iCol++)
        {
            var numberString = "";
            for (int iRow = 0; iRow < lines.Length; iRow++)
            {
                var c = lines[iRow][iCol];
                numberString += c;
            }
            Console.WriteLine($"numberString: {numberString}");
            if (numberString.Contains('*'))
            {
                numberString = numberString.Replace("*", "");
                operation = "*";
            }
            else if (numberString.Contains('+'))
            {
                numberString = numberString.Replace("+", "");
                operation = "+";
            }

            if (string.IsNullOrWhiteSpace(numberString))
            {
                var equation = (operation, values);
                Console.WriteLine($"{operation}: {string.Join(", ", values)}");
                equations.Add(equation);
                operation = "";
                values = new List<long>();
                continue;
            }

            values.Add(long.Parse(numberString));

            if (iCol == maxLineLength - 1)
            {
                var equation = (operation, values);
                Console.WriteLine($"{operation}: {string.Join(", ", values)}");
                equations.Add(equation);
            }
        }

        long sum = 0;
        foreach (var equation in equations)
        {
            var op = equation.Item1;
            var vals = equation.Item2;
            sum += op switch
            {
                "*" => vals.Aggregate((a, b) => a * b),
                "+" => vals.Aggregate((a, b) => a + b),
                _ => throw new Exception($"op: {op}")
            };
        }
        return sum;
    }
    // public static long Part02(string input)
    // {
    //     var equations = input
    //                 .Split("\n")
    //                 .Where(line => line != "")
    //                 .Select(line =>
    //                         Regex.Split(line.Trim(), @"\s+")
    //                         .Select(s => s.Trim())
    //                         .ToArray()
    //                         ).ToArray();
    //
    //     var equationList = new List<(string, string[])>();
    //     for (int iCol = 0; iCol < equations[0].Length; iCol++)
    //     {
    //         var operation = equations[equations.Length - 1][iCol].Trim();
    //         long answer = operation == "*" ? 1 : 0;
    //         var equation = (operation, new string[equations.Length - 1]);
    //         for (int iRow = 0; iRow < equations.Length - 1; iRow++)
    //         {
    //             var valueString = equations[iRow][iCol].Trim();
    //             equation.Item2[iRow] = valueString;
    //         }
    //         equationList.Add(equation);
    //     }
    //
    //     var newEquations = new List<(string, string[])>();
    //     foreach (var equation in equationList)
    //     {
    //         var operation = equation.Item1;
    //         var values = equation.Item2;
    //         var longest = values.Max(s => s.Length);
    //         string[] newValues = Enumerable.Repeat("", values.Length).ToArray();
    //         foreach (var value in values)
    //         {
    //             for (int i = longest - 1; i >= 0; i--)
    //             {
    //                 if (i > value.Length - 1)
    //                 {
    //                     continue;
    //                 }
    //                 var v = value[i];
    //                 newValues[i] = newValues[i] + v;
    //             }
    //         }
    //         newEquations.Add((operation, newValues));
    //     }
    //
    //     long sum = 0;
    //     foreach (var equation in newEquations)
    //     {
    //         var operation = equation.Item1;
    //         var values = equation.Item2.Select(s => long.Parse(s));
    //         Console.WriteLine(string.Join(operation, values));
    //         sum += operation switch
    //         {
    //             "*" => values.Aggregate((a, b) => a * b),
    //             "+" => values.Aggregate((a, b) => a + b),
    //             _ => throw new Exception($"operation: {operation}")
    //         };
    //     }
    //     return sum;
    // }
}
