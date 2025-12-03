namespace Aoc2025;

public static class Day03
{
    public static void Run()
    {
        // Read file into string
        var input = File.ReadAllText("Day-03/input.txt");

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
        var lines = input.Split("\n").Where(line => line != "").ToArray();
        var sum = 0;
        foreach (var line in lines)
        {
            var jolts = MaxJolts(line);
            Console.WriteLine($"{line} -> {jolts}");
            sum += jolts;
        }
        return sum;
    }

    public static long Part02(string input)
    {
        var lines = input.Split("\n").Where(line => line != "").ToArray();
        long sum = 0;
        foreach (var line in lines)
        {
            var jolts = MaxJoltsN(line);
            if (jolts.Length != 12)
            {
                throw new Exception($"line: {line} Length: {jolts.Length}");
            }
            Console.WriteLine($"{line} -> {jolts}");
            sum += long.Parse(jolts);
        }
        return sum;
    }

    public static string MaxJoltsN(string line, int limit = 12)
    {
        var digit = 0;
        var maxIndex = 0;
        for (int i = 0; i < line.Length - limit + 1; i++)
        {
            var d = line[i];
            var num = int.Parse(d.ToString());
            if (num > digit)
            {
                digit = num;
                maxIndex = i;
            }
        }
        Console.WriteLine($"line: {line} limit: {limit} maxIndex: {maxIndex} digit: {digit}");
        if (limit == 1)
        {
            return digit.ToString();
        }
        var nextDigit = MaxJoltsN(line.Substring(maxIndex + 1), limit - 1).ToString();
        return $"{digit}{nextDigit}";
    }

    public static int MaxJolts(string line)
    {
        var firstDigit = 0;
        var maxIndex = 0;
        for (int i = 0; i < line.Length - 1; i++)
        {
            var d = line[i];
            var num = int.Parse(d.ToString());
            if (num > firstDigit)
            {
                firstDigit = num;
                maxIndex = i;
            }
        }
        var secondDigit = int.Parse(line[maxIndex + 1].ToString());
        for (int i = maxIndex + 1; i < line.Length; i++)
        {
            var d = line[i];
            var num = int.Parse(d.ToString());
            if (num > secondDigit)
            {
                secondDigit = num;
                maxIndex = i;
            }
        }
        var jolts = int.Parse($"{firstDigit}{secondDigit}");
        return jolts;
    }
}
