namespace Aoc2025;

public static class Day04
{
    public static void Run()
    {
        // Read file into string
        var input = File.ReadAllText("Day-04/input.txt");

        // Run part 1
        var (result, _) = Part01(input);

        // Run part 2
        var result2 = Part02(input);

        // Print result
        Console.WriteLine(result);
        Console.WriteLine(result2);
    }

    public static (long, string) Part01(string input)
    {
        var lines = input.Split("\n").Where(line => line != "").ToArray();
        var count = 0;
        var xLimMin = 0;
        var yLimMin = 0;
        var yLimMax = lines.Length - 1;
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines.Length; x++)
            {
                var xLimMax = lines[y].Length - 1;
                var tl = (x <= xLimMin || y <= yLimMin) ? 0 : (lines[y - 1][x - 1] == '@' ? 1 : 0);
                var tm = (y <= yLimMin) ? 0 : (lines[y - 1][x] == '@' ? 1 : 0);
                var tr = (x >= xLimMax || y <= yLimMin) ? 0 : (lines[y - 1][x + 1] == '@' ? 1 : 0);
                var l = (x <= xLimMin) ? 0 : (lines[y][x - 1] == '@' ? 1 : 0);
                var r = (x >= xLimMax) ? 0 : (lines[y][x + 1] == '@' ? 1 : 0);
                var bl = (x <= xLimMin || y >= yLimMax) ? 0 : (lines[y + 1][x - 1] == '@' ? 1 : 0);
                var bm = (y >= yLimMax) ? 0 : (lines[y + 1][x] == '@' ? 1 : 0);
                var br = (x >= xLimMax || y >= yLimMax) ? 0 : (lines[y + 1][x + 1] == '@' ? 1 : 0);

                var adjacent = tl + tm + tr + l + r + bl + bm + br;
                if (adjacent < 4 && lines[y][x] == '@')
                {
                    var newLine = lines[y].Substring(0, x) + "x" + lines[y].Substring(x + 1);
                    if (lines[y].Length != newLine.Length)
                    {
                        throw new Exception($"lines[y].Length: {lines[y].Length} newLine.Length: {newLine.Length}");
                    }
                    lines[y] = newLine;
                    count++;
                }
            }
        }
        return (count, string.Join("\n", lines));
    }

    public static long Part02(string input)
    {
        long count = 1;
        long sum = 0;
        while (count != 0)
        {
            (count, input) = Part01(input);
            sum += count;
        }
        return sum;
    }
}
