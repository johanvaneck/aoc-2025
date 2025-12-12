namespace Aoc2025;

public static class Day11
{
    public record Server(
            string Name,
            string[] Connections
            );

    private static Dictionary<string, Server> servers = new();
    private static Dictionary<(string, string), long> cache = new();

    public static void Run()
    {
        servers = File.ReadAllText("Day-11/input.txt")
            .Split("\n").Where(line => line != "")
            .Where(line => line != "")
            .Select(line => line.Split(":"))
            .Select(line => new Server(
                        line[0].Trim(),
                        line[1].Split(" ")
                        .Select(c => c.Trim())
                        .Where(c => c != "")
                        .ToArray()
                        )
                    )
            .ToDictionary(s => s.Name, s => s);


        // Run part 1
        var result = Part01();

        // Run part 2
        var result2 = Part02();

        // Print result
        Console.WriteLine($"Part01: {result}");
        Console.WriteLine($"Part02: {result2}");
    }

    public static long Part01()
    {
        return FindNPaths("you", "out", servers, cache);
    }

    public static long Part02()
    {
        var svrToDac = FindNPaths("svr", "dac", servers, cache);
        var svrToFft = FindNPaths("svr", "fft", servers, cache);
        var dacToFft = FindNPaths("dac", "fft", servers, cache);
        var dacToOut = FindNPaths("dac", "out", servers, cache);
        var fftToDac = FindNPaths("fft", "dac", servers, cache);
        var fftToOut = FindNPaths("fft", "out", servers, cache);
        // var scenario1 = FindNPaths("svr", "dac", servers, cache)
        //     * FindNPaths("dac", "fft", servers, cache)
        //     * FindNPaths("fft", "out", servers, cache);
        // var scenario2 = FindNPaths("svr", "fft", servers, cache)
        //     * FindNPaths("fft", "dac", servers, cache)
        //     * FindNPaths("dac", "out", servers, cache);
        var scenario1 = svrToDac * dacToFft * fftToOut;
        var scenario2 = svrToFft * fftToDac * dacToOut;

        Console.WriteLine($"SVR -> DAC: {svrToDac}");
        Console.WriteLine($"SVR -> FFT: {svrToFft}");
        Console.WriteLine($"DAC -> FFT: {dacToFft}");
        Console.WriteLine($"DAC -> OUT: {dacToOut}");
        Console.WriteLine($"FFT -> DAC: {fftToDac}");
        Console.WriteLine($"FFT -> OUT: {fftToOut}");
        Console.WriteLine($"Scenario 1: {scenario1}");
        Console.WriteLine($"Scenario 2: {scenario2}");
        return Math.Max(scenario1, scenario2);
    }

    public static long FindNPaths(
        string from,
        string to,
        Dictionary<string, Server> servers,
        Dictionary<(string, string), long> cache // <--- CHANGED: Key is (from, to)
    )
    {
        // 1. Success Base Case
        if (from == to)
        {
            return 1;
        }

        // 2. Dead End Base Case (Safe handling)
        // If the server doesn't exist in our source list, it's a dead end. Return 0.
        if (!servers.ContainsKey(from))
        {
            return 0;
        }

        // 3. Cache Check
        // We check the combination of current node AND target
        if (cache.ContainsKey((from, to)))
        {
            return cache[(from, to)];
        }

        // 4. Recursive Step
        var server = servers[from];

        // Note: If a connection leads to a dead end, it returns 0, which is fine.
        var sum = server.Connections.Sum(c => FindNPaths(c, to, servers, cache));

        // 5. Store and Return
        cache[(from, to)] = sum;
        return sum;
    }

    // public static void FindPaths(
    //        string name,
    //        Dictionary<string, Server> servers,
    //        List<string> currentPath,
    //        Dictionary<string, List<string>> cache,
    //        List<List<string>> paths
    //        )
    // {
    //     if (cache == null)
    //     {
    //         cache = new Dictionary<string, List<string>>();
    //     }
    //     if (cache.ContainsKey(name))
    //     {
    //         Console.WriteLine($"Cache hit: {name}");
    //         var merged = currentPath.Concat(cache[name]).ToList();
    //         if (merged.Contains("dac") && merged.Contains("fft"))
    //         {
    //             paths.Add(merged);
    //         }
    //         return;
    //     }
    //     currentPath.Add(name);
    //     if (name == "out")
    //     {
    //         if (currentPath.Contains("dac") && currentPath.Contains("fft"))
    //         {
    //             Console.WriteLine($"Found! {name}: {string.Join(" ", currentPath)}");
    //             paths.Add(currentPath);
    //         }
    //         else
    //         {
    //             Console.WriteLine($"Nope. {name}: {string.Join(" ", currentPath)}");
    //         }
    //         return;
    //     }
    //     if (!servers.ContainsKey(name))
    //     {
    //         throw new Exception($"Server {name} not found in {string.Join(" ", servers.Keys)}");
    //     }
    //     var server = servers[name];
    //
    //     foreach (var c in server.Connections)
    //     {
    //         FindPaths(c, servers, currentPath.ToList(), cache, paths);
    //     }
    // }
    //
    // public static void PrintCache(Dictionary<string, List<string>>? cache)
    // {
    //     if (cache == null) return;
    //     foreach (var (k, v) in cache)
    //     {
    //         Console.WriteLine($"{k}: {string.Join(" ", v)}");
    //     }
    //     Console.WriteLine();
    // }
}
