namespace Aoc2025;

public static class Day07
{
    public static void Run()
    {
        // Read file into string
        var input = File.ReadAllText("Day-07/input.txt");

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
        var lines = input.Split("\n").Where(line => line != "").ToArray();
        var chars = lines.Select(line => line.ToCharArray()).ToArray();
        var matrix = AddBeams(chars);
        return CountSplits(matrix);
    }

    // 1853021308 - too low
    // 27055852018812
    // 54111704037623 - too high
    public static long Part02(string input)
    {
        var lines = input.Split("\n").Where(line => line != "").ToArray();
        var chars = lines.Select(line => line.ToCharArray()).ToArray();
        var startCol = lines[0].IndexOf('S');
        var countCache = new Dictionary<(int, int), long>();
        return CountPaths(chars, 0, startCol, countCache);
    }

    public static char[][] AddBeams(char[][] matrix)
    {
        for (int iCol = 0; iCol < matrix[0].Length; iCol++)
        {
            var c = matrix[0][iCol];
            if (c == 'S')
            {
                ProjectBeam(matrix, 1, iCol);
            }
        }
        return matrix;
    }

    public static void ProjectBeam(char[][] matrix, int iRow, int iCol)
    {
        if (iRow < 0 || iRow >= matrix.Length || iCol < 0 || iCol >= matrix[iRow].Length)
        {
            return;
        }
        var i = 0;
        while (iRow + i < matrix.Length)
        {
            var c = matrix[iRow + i][iCol];
            if (c == '|')
            {
                return;
            }

            if (c == '^')
            {
                ProjectBeam(matrix, iRow + i, iCol - 1);
                ProjectBeam(matrix, iRow + i, iCol + 1);
                return;
            }
            else
            {
                matrix[iRow + i][iCol] = '|';
            }
            // PrintMatrix(matrix);
            // Console.ReadLine();
            // Console.Clear();
            i++;
        }
    }

    public static int CountSplits(char[][] matrix)
    {
        var count = 0;
        for (int iRow = 0; iRow < matrix.Length; iRow++)
        {
            for (int iCol = 0; iCol < matrix[iRow].Length; iCol++)
            {
                if (matrix[iRow][iCol] == '^' && matrix[iRow - 1][iCol] == '|')
                {
                    count++;
                }
            }
        }
        return count;
    }

    public static long CountPaths(char[][] matrix, int iRow, int iCol, Dictionary<(int, int), long> countCache)
    {
        if (countCache.ContainsKey((iRow, iCol)))
        {
            var cache = countCache[(iRow, iCol)];
            matrix[iRow][iCol] = 'X';
            return cache;
        }
        if (iRow < 0 || iRow >= matrix.Length || iCol < 0 || iCol >= matrix[iRow].Length)
        {
            return 1;
        }
        var i = 0;
        while (iRow + i < matrix.Length)
        {
            var c = matrix[iRow + i][iCol];
            if (c == '^')
            {
                var left = CountPaths(matrix, iRow + i, iCol - 1, countCache);
                countCache[(iRow + i, iCol - 1)] = left;
                var right = CountPaths(matrix, iRow + i, iCol + 1, countCache);
                countCache[(iRow + i, iCol + 1)] = right;
                var count = left + right;
                return count;
            }
            matrix[iRow + i][iCol] = '|';
            // PrintMatrix(matrix);
            // Thread.Sleep(3);
            // Console.ReadLine();
            // Console.Clear();
            i++;
        }
        return 1;
    }

    public static void PrintMatrix(char[][] matrix, int? iRow, int? iCol, char? marker)
    {
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                if (iRow == i && iCol == j && marker != null)
                {
                    Console.Write(marker);
                }
                else
                {
                    Console.Write(matrix[i][j]);
                }
            }
            Console.WriteLine();
        }
    }
}
