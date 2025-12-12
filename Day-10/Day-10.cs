namespace Aoc2025;

public static class Day10
{
    public static void Run()
    {
        // Read file into string
        var input = File.ReadAllText("Day-10/example.txt");

        // Run part 1
        var result = Part01(input);

        // Run part 2
        var result2 = Part02(input);

        // Print result
        Console.WriteLine($"Part01: {result}");
        Console.WriteLine($"Part02: {result2}");
    }

    /*
[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}

[0 1 1 0] X [            
              [0 0 0 1]
              [0 1 0 1]
              [0 0 1 0]
              [0 0 1 1]
              [1 0 1 0]
              [1 1 0 0]
            ]           

     */
    public static long Part01(string input)
    {
        var machines = input
            .Split("\n").Where(line => line != "")
            .Where(line => line != "")
            .Select(line => line.Split(" "))
            .Select(line => new
            {
                Diagram = line[0]
                .Substring(1, line[0].Length - 2)
                .Select(l => l == '#'),
                Buttons = line[1..^1]
                .Select(button => button.Substring(1, button.Length - 2))
                .Select(bs => bs.Split(",").Select(int.Parse)),
                Joltage = line[^1]
                .Substring(1, line[^1].Length - 2)
                .Split(",")
                .Select(int.Parse),
            });

        return 0;
    }

    public static long Part02(string input)
    {
        return 0;
    }
}
// foreach (var machine in machines)
// {
//     Console.WriteLine($"Diagram: {string.Join(",", machine.Diagram.Select(b => b ? "#" : "."))}");
//     Console.WriteLine($"Buttons: {string.Join("|", machine.Buttons.Select(b => string.Join(",", b)))}");
//     Console.WriteLine($"Joltage: {string.Join(",", machine.Joltage)}");
//     Console.WriteLine();
// }
