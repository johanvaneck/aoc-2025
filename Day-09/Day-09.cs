namespace Aoc2025;

public static class Day09
{
    public static void Run()
    {
        // Read file into string
        var input = File.ReadAllText("Day-09/example.txt");

        // Run part 1
        var result = Part01(input);

        // Run part 2
        var result2 = Part02(input);

        // Print result
        Console.WriteLine($"Part01: {result}");
        Console.WriteLine($"Part02: {result2}");
    }

    // 4751881200 - too low
    public static long Part01(string input)
    {
        var coords = input
            .Split("\n")
            .Where(line => line != "")
            .Select(line => line.Split(",").Select(long.Parse).ToArray())
            .ToArray();

        var max = 0L;
        for (int i = 0; i < coords.Length; i++)
        {
            for (int j = 0; j < coords.Length; j++)
            {
                if (i == j)
                {
                    continue;
                }
                var a = coords[i];
                var b = coords[j];
                var area = CalculateArea(a[0], a[1], b[0], b[1]);
                max = Math.Max(max, area);
            }
        }
        return max;
    }

    public static long Part02(string input)
    {
        var coords = input
            .Split("\n")
            .Where(line => line != "")
            .Select(line => line.Split(",").Select(long.Parse).ToArray())
            .ToArray();

        var areas = new Dictionary<(long[], long[]), long>();
        for (int i = 0; i < coords.Length; i++)
        {
            for (int j = 0; j < coords.Length; j++)
            {
                var a = coords[i];
                var b = coords[j];
                if (
                        i == j
                        || areas.ContainsKey((a, b))
                        || areas.ContainsKey((b, a))
                        )
                {
                    continue;
                }
                var area = CalculateArea(a[0], a[1], b[0], b[1]);
                areas.Add((a, b), area);
            }
        }
        foreach (var area in areas.OrderByDescending(pair => pair.Value))
        {
            if (IsValid(coords, area.Key.Item1, area.Key.Item2))
            {
                return area.Value;
            }
        }
        return -1;
    }

    public static long CalculateArea(long x1, long y1, long x2, long y2)
    {
        return (Math.Abs(x2 - x1) + 1) * (Math.Abs(y2 - y1) + 1);
    }

    // public static IEnumerable<long[]> GetValidTiles(IEnumerable<long[]> coords)
    // {
    //     return new HashSet<>();
    // }

    public static bool IsValid(long[][] coords, long[] a, long[] b)
    {

        return false;
    }
}
