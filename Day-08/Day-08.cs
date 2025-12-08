namespace Aoc2025;

public static class Day08
{
    public static void Run()
    {
        // Read file into string
        var input = File.ReadAllText("Day-08/input.txt");

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
        var coords = input
            .Split("\n")
            .Where(line => line != "")
            .Select(line => line.Split(",").Select(int.Parse).ToArray())
            .ToArray();

        var limit = 1000;
        var count = 0;
        var distances = GetDistances(coords);
        var sortedDistances = distances.OrderBy(pair => pair.Value).ToArray();
        var circuts = new List<HashSet<int[]>>();
        foreach (var distancePair in sortedDistances)
        {
            var coord1 = distancePair.Key.Item1;
            var coord2 = distancePair.Key.Item2;
            // Console.WriteLine($"Pair: ({string.Join(",", coord1)}) ({string.Join(",", coord2)}) Distance: {distancePair.Value}");
            var distance = distancePair.Value;
            HashSet<int[]>? coord1ExistingCircut = null;
            HashSet<int[]>? coord2ExistingCircut = null;
            foreach (var circut in circuts)
            {
                if (circut.Contains(coord2))
                {
                    coord2ExistingCircut = circut;
                }
                else if (circut.Contains(coord1))
                {
                    coord1ExistingCircut = circut;
                }
            }
            if (count >= limit)
            {
                break;
                // if (coord1ExistingCircut == null)
                // {
                //     // Add new circut
                //     Console.WriteLine($"Limit exeeded. Adding new circut: ({string.Join(",", coord1)}) ({string.Join(",", coord2)})");
                //     circuts.Add(new HashSet<int[]> { coord1 });
                // }
                // if (coord2ExistingCircut == null)
                // {
                //     // Add new circut
                //     Console.WriteLine($"Limit exeeded. Adding new circut: ({string.Join(",", coord2)}) ({string.Join(",", coord1)})");
                //     circuts.Add(new HashSet<int[]> { coord2 });
                // }
            }
            else if (coord1ExistingCircut != null && coord2ExistingCircut != null)
            {
                // Merge circuts
                // Console.WriteLine($"Merging circuts: {circuts.IndexOf(coord1ExistingCircut)} and {circuts.IndexOf(coord2ExistingCircut)}");
                circuts.Remove(coord2ExistingCircut);
                coord1ExistingCircut.UnionWith(coord2ExistingCircut);
            }
            else if (coord1ExistingCircut != null)
            {
                // Console.WriteLine($"Adding to circut: {circuts.IndexOf(coord1ExistingCircut)}: ({string.Join(",", coord2)})");
                coord1ExistingCircut.Add(coord2);
            }
            else if (coord2ExistingCircut != null)
            {
                // Console.WriteLine($"Adding to circut: {circuts.IndexOf(coord2ExistingCircut)}: ({string.Join(",", coord1)})");
                coord2ExistingCircut.Add(coord1);
            }
            else
            {
                // Console.WriteLine($"Adding new circut: ({string.Join(",", coord1)}) ({string.Join(",", coord2)})");
                var newCircut = new HashSet<int[]>();
                newCircut.Add(coord1);
                newCircut.Add(coord2);
                circuts.Add(newCircut);
            }
            count++;
            // Console.ReadLine();
        }
        return circuts
            .OrderBy(circut => circut.Count)
            .TakeLast(3)
            .Aggregate(1L, (sum, circut) => sum * circut.Count);
    }

    public static long Part02(string input)
    {
        var coords = input
            .Split("\n")
            .Where(line => line != "")
            .Select(line => line.Split(",").Select(int.Parse).ToArray())
            .ToArray();
        var distances = GetDistances(coords);
        var sortedDistances = distances.OrderBy(pair => pair.Value).ToArray();
        var circuts = new List<HashSet<int[]>>();
        foreach (var distancePair in sortedDistances)
        {
            var coord1 = distancePair.Key.Item1;
            var coord2 = distancePair.Key.Item2;
            // Console.WriteLine($"Pair: ({string.Join(",", coord1)}) ({string.Join(",", coord2)}) Distance: {distancePair.Value}");
            var distance = distancePair.Value;
            HashSet<int[]>? coord1ExistingCircut = null;
            HashSet<int[]>? coord2ExistingCircut = null;
            if (circuts == null)
            {
                continue;
            }
            foreach (var circut in circuts)
            {
                if (circut.Contains(coord2))
                {
                    coord2ExistingCircut = circut;
                }
                else if (circut.Contains(coord1))
                {
                    coord1ExistingCircut = circut;
                }
            }
            if (coord1ExistingCircut != null && coord2ExistingCircut != null)
            {
                // Merge circuts
                // Console.WriteLine($"Merging circuts: {circuts.IndexOf(coord1ExistingCircut)} and {circuts.IndexOf(coord2ExistingCircut)}");
                circuts.Remove(coord2ExistingCircut);
                coord1ExistingCircut.UnionWith(coord2ExistingCircut);
            }
            else if (coord1ExistingCircut != null)
            {
                // Console.WriteLine($"Adding to circut: {circuts.IndexOf(coord1ExistingCircut)}: ({string.Join(",", coord2)})");
                coord1ExistingCircut.Add(coord2);
            }
            else if (coord2ExistingCircut != null)
            {
                // Console.WriteLine($"Adding to circut: {circuts.IndexOf(coord2ExistingCircut)}: ({string.Join(",", coord1)})");
                coord2ExistingCircut.Add(coord1);
            }
            else
            {
                // Console.WriteLine($"Adding new circut: ({string.Join(",", coord1)}) ({string.Join(",", coord2)})");
                var newCircut = new HashSet<int[]>();
                newCircut.Add(coord1);
                newCircut.Add(coord2);
                circuts.Add(newCircut);
            }

            var firstCircut = circuts?.First();
            if (firstCircut == null)
            {
                continue;
            }
            Console.WriteLine($"Pair: ({string.Join(",", coord1)}) ({string.Join(",", coord2)}) Count: {firstCircut.Count} Length: {coords.Length}");
            if (firstCircut != null && firstCircut.Count == coords.Length)
            {
                return ((long)coord1[0]) * ((long)coord2[0]);
            }
            // Console.ReadLine();
        }
        return 0;
    }

    public static Dictionary<(int[], int[]), double> GetDistances(int[][] coords)
    {
        var distances = new Dictionary<(int[], int[]), double>();
        foreach (var coord1 in coords)
        {
            foreach (var coord2 in coords)
            {
                if (coord1 == coord2)
                {
                    continue;
                }
                if (
                        distances.ContainsKey((coord1, coord2))
                        || distances.ContainsKey((coord2, coord1))
                        )
                {
                    continue;
                }
                var distance = CalculateDistance(
                        coord1[0], coord1[1], coord1[2],
                        coord2[0], coord2[1], coord2[2]
                        );
                distances.Add((coord1, coord2), distance);
            }
        }
        return distances;
    }

    public static double CalculateDistance(int x1, int y1, int z1, int x2, int y2, int z2)
    {
        return Math.Sqrt(
                Math.Pow(x2 - x1, 2)
                + Math.Pow(y2 - y1, 2)
                + Math.Pow(z2 - z1, 2)
                );
    }
}
