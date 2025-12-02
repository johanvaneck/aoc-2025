namespace Aoc2025;

public static class Day01
{
    public static void Run()
    {
        // Read file into string
        var input = File.ReadAllText("Day-01/input.txt");

        // Run part 1
        var result = Part01(input);

        // Run part 2
        var result2 = Part02(input);

        // Print result
        Console.WriteLine(result);
        Console.WriteLine(result2);
    }

    public static int Part01(string input)
    {
        var lines = input.Split("\n").Where(line => line != "").ToArray();
        var countZeros = 0;
        var position = 50;
        foreach (var line in lines)
        {
            var direction = line[0];
            var rotation = int.Parse(line.Substring(1));
            position += direction switch
            {
                'L' => -rotation,
                'R' => rotation,
                _ => 0
            };
            if (position < 0)
            {
                position = 100 + position % 100;
            }
            if (position >= 100)
            {
                position = position % 100;
            }
            if (position == 0)
            {
                countZeros++;
            }
        }
        return countZeros;
    }

    public static int Part02(string input)
    {
        var lines = input.Split("\n").Where(line => line != "").ToArray();
        var count = 0;
        var position = 50;

        foreach (var line in lines)
        {
            var direction = line[0];
            var rotation = int.Parse(line.Substring(1));

            for (int i = 0; i < rotation; i++)
            {
                if (direction == 'R')
                {
                    position++;
                    if (position > 99) position = 0;
                }
                else
                {
                    position--;
                    if (position < 0) position = 99;
                }

                if (position == 0)
                {
                    count++;
                }
            }
        }
        return count;
    }
}
