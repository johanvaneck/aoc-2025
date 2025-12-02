namespace Aoc2025;

public static class Day02
{
    public static void Run()
    {
        // Read file into string
        var input = File.ReadAllText("Day-02/input.txt");

        // Run part 1
        var result = Part01(input);

        // Run part 2
        var result2 = Part02(input);

        // Print result
        Console.WriteLine(result);
        Console.WriteLine(result2);
    }

    public static long Part01(string input)
    {
        var ranges = input
            .Split(",")
            .Select(range => range.Split("-"))
            .Select(range => new { Start = long.Parse(range[0]), End = long.Parse(range[1]) })
            .ToArray();

        long sum = 0;
        foreach (var range in ranges)
        {
            for (long i = range.Start; i <= range.End; i++)
            {
                var str = i.ToString();
                var part1 = str.Substring(0, str.Length / 2);
                var part2 = str.Substring(str.Length / 2);
                if (part1 == part2)
                {
                    sum += i;
                }
            }
        }
        return sum;
    }

    public static long Part02(string input)
    {
        var ranges = input
            .Split(",")
            .Select(range => range.Split("-"))
            .Select(range => new { Start = long.Parse(range[0]), End = long.Parse(range[1]) })
            .ToArray();

        long sum = 0;
        foreach (var range in ranges)
        {
            for (long i = range.Start; i <= range.End; i++)
            {
                var str = i.ToString();
                if (IsInvalid(str))
                {
                    sum += i;
                }
            }
        }
        return sum;
    }

    public static bool IsInvalid(string str)
    {
        var debug = str == "824824824";
        var chunkSize = 1;
        while (chunkSize <= str.Length)
        {
            if (debug)
            {
                Console.WriteLine($"Divisor: {chunkSize}, str.Length: {str.Length}");
            }
            if (str.Length % chunkSize == 0)
            {
                var prevPart = str.Substring(0, (int)chunkSize);
                for (int j = chunkSize; j + chunkSize <= str.Length; j = j + chunkSize)
                {
                    var part = str.Substring(j, (int)chunkSize);
                    if (debug)
                    {
                        Console.WriteLine($"{str} {prevPart} {part}");
                    }
                    if (prevPart != part)
                    {
                        prevPart = part;
                        if (debug)
                        {
                            Console.WriteLine($"Breaking for");
                        }
                        break;
                    }
                    if (j + chunkSize == str.Length)
                    {
                        // Found an invalid ID
                        Console.WriteLine($"Invalid ID: {str}");
                        return true;
                    }
                    prevPart = part;
                }
            }
            chunkSize++;
        }
        return false;
    }
}
