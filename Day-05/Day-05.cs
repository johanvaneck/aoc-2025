namespace Aoc2025;

public static class Day05
{
    public static void Run()
    {
        // Read file into string
        var input = File.ReadAllText("Day-05/input.txt");

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
        var parts = input.Split("\n\n").ToArray();
        var ranges = parts[0].Split("\n").Select(range => range.Split("-")).ToArray();
        var ingredients = parts[1].Split("\n").Where(line => line != "").ToArray();
        var sum = 0;
        foreach (var ingredient in ingredients)
        {
            var ingredientValue = long.Parse(ingredient);
            foreach (var range in ranges)
            {
                var start = long.Parse(range[0]);
                var end = long.Parse(range[1]);
                if (ingredientValue >= start && ingredientValue <= end)
                {
                    sum++;
                    break;
                }
            }
        }
        return sum;
    }

    // 277882449153876 - too low
    // 332156762033854 - not right
    // 357907198933892 - CORRECT
    // 359219480334427 - too high => complement soluiton
    public static long Part02(string input)
    {
        var parts = input.Split("\n\n").ToArray();
        var ranges = parts[0].Split("\n")
            .Select(range => range.Split("-").Select(s => long.Parse(s)).ToArray())
            .Select(s => new { Start = s[0], End = s[1] })
            .OrderBy(r => r.Start)
            .ToList();

        if (ranges == null)
        {
            throw new Exception("ranges is null");
        }

        if (ranges == null || ranges.Count == 0) return 0;

        long sum = 0;

        // Initialize with the first range
        long currentStart = ranges[0].Start;
        long currentEnd = ranges[0].End;

        for (int i = 1; i < ranges.Count; i++)
        {
            var next = ranges[i];

            if (next.Start > currentEnd + 1) // Discontinuous (Gap found) (+1 depends if 5-6 is contiguous to 4-5)
            {
                // 1. Add the accumulated block to sum
                sum += (currentEnd - currentStart + 1);

                // 2. Reset logic for the new block
                currentStart = next.Start;
                currentEnd = next.End;
            }
            else // Overlapping or touching
            {
                // Extend the current block if this new range goes further
                currentEnd = Math.Max(currentEnd, next.End);
            }
        }

        // Add the final block after the loop finishes
        sum += (currentEnd - currentStart + 1);

        return sum;
    }

    public static long Part02Old(string input)
    {
        var parts = input.Split("\n\n").ToArray();
        var ranges = parts[0].Split("\n")
            .Select(range => range.Split("-").Select(s => long.Parse(s)).ToArray())
            .Select(s => (s[0], s[1]))
            .ToHashSet();

        if (ranges == null)
        {
            throw new Exception("ranges is null");
        }

        var intersections = GetIntersections(ranges);
        // foreach (var range in intersections)
        // {
        //     Console.WriteLine($"intersection: {range.Item1}-{range.Item2}");
        // }
        // var complements = GetComplements(ranges);
        // foreach (var range in complements)
        // {
        //     Console.WriteLine($"complement: {range.Item1}-{range.Item2}");
        // }

        return ranges.Select(r => r.Item2 - r.Item1 + 1).Sum()
            - intersections.Select(i => i.Item2 - i.Item1 + 1).Sum();
    }


    // public static HashSet<(long, long)> GetComplements(HashSet<(long, long)> ranges)
    // {
    //     var complements = new HashSet<(long, long)>();
    //     foreach (var range1 in ranges)
    //     {
    //         foreach (var range2 in ranges)
    //         {
    //             if (range1 == range2)
    //             {
    //                 continue;
    //             }
    //             var start1 = range1.Item1;
    //             var end1 = range1.Item2;
    //
    //             var start2 = range2.Item1;
    //             var end2 = range2.Item2;
    //             // Case 0.1:
    //             // *********|****|****
    //             // *|****|****
    //             // Case 0.2:
    //             // *|****|****
    //             // *********|****|****
    //             if (end1 < start2 || end2 < start1)
    //             {
    //                 complements.Add((start1, end1));
    //                 complements.Add((start2, end2));
    //             }
    //         }
    //     }
    //     return complements;
    // }

    public static HashSet<(long, long)> GetIntersections(HashSet<(long, long)> ranges)
    {
        var intersections = new HashSet<(long, long)>();
        foreach (var range1 in ranges)
        {
            foreach (var range2 in ranges)
            {
                if (range1 == range2)
                {
                    continue;
                }
                var start1 = range1.Item1;
                var end1 = range1.Item2;

                var start2 = range2.Item1;
                var end2 = range2.Item2;
                // Case 0.1:
                // *********|****|****
                // *|****|****
                // Case 0.2:
                // *|****|****
                // *********|****|****
                if (end1 < start2 || end2 < start1)
                {
                    // No intersection
                    continue;
                }
                // Case 1:
                // ****|****|****
                // *|****|****
                if (start1 >= start2 && end1 >= end2)
                {
                    var x = (start1, end2);
                    intersections.Add(x);
                }
                // Case 2:
                // ****|****|****
                // *******|****|****
                if (start1 <= start2 && end1 <= end2)
                {
                    var x = (start2, end1);
                    intersections.Add(x);
                }
                // Case 3:
                // ****|***********|****
                // *******|****|****
                if (start1 <= start2 && end1 >= end2)
                {
                    var x = (start2, end2);
                    intersections.Add(x);
                }
                // Case 4:
                // *******|****|****
                // ****|***********|****
                if (start1 >= start2 && end1 <= end2)
                {
                    var x = (start1, end1);
                    intersections.Add(x);
                }
            }
        }
        return intersections;
    }
}
