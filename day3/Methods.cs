using System.Text.RegularExpressions;

public static class Methods
{
    public static string[] lines = new string[0];

    //Part One
    public static int GetPartOne(string[] ls)
    {
        lines = ls;
        int sum = 0;

        for (int y = 0; y < lines.Length; y++)
        {
            List<List<int>> numbersThisLine = new List<List<int>>();
            List<int> currentBlock = new List<int>();
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (char.IsDigit(lines[y][x]))
                {
                    currentBlock.Add(x);
                }
                else if (currentBlock.Count > 0)
                {
                    numbersThisLine.Add(currentBlock);
                    currentBlock = new List<int>();
                }
            }

            if (currentBlock.Count > 0)
            {
                numbersThisLine.Add(currentBlock);
                currentBlock = new List<int>();
            }

            foreach (List<int> li in numbersThisLine)
            {
                bool hasSymbol = false;
                string num = "";
                foreach (int i in li)
                {
                    // System.Console.WriteLine(i);
                    if (IsAdjacent(i, y)) hasSymbol = true;
                    num += lines[y][i];
                }
                if (hasSymbol) sum += int.Parse(num);
            }
        }
        return sum;
    }

    //Part Two
    public static int GetPartTwo(string[] ls)
    {
        lines = ls;
        int sum = 0;

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (lines[y][x] == '*')
                {
                    List<int> intsOfGear = new List<int>();
                    foreach (int[] positions in GetValidNeighbors(x, y))
                    {
                        int wholeNeighbor = GetWholeNum(positions[0], positions[1]);
                        if (!intsOfGear.Contains(wholeNeighbor) && wholeNeighbor != -1)
                            intsOfGear.Add(GetWholeNum(positions[0], positions[1]));
                    }
                    if (intsOfGear.Count == 2)
                        sum += intsOfGear[0] * intsOfGear[1];
                }
            }
        }
        return sum;
    }


    //Return if a Number is adjacent to a symbol
    static bool IsAdjacent(int x, int y)
    {
        if (GetNeighbors(x, y).Replace(".", "").Length > 0) return true;
        return false;
    }

    //Return all neighboring characters in a string
    static string GetNeighbors(int x, int y)
    {
        string chars = "";

        foreach (int[] p in GetNeighborCoordinates(x, y, lines[y].Length, lines.Length))
            chars += lines[p[1]][p[0]];

        return Regex.Replace(chars, "[0-9]", "");
    }

    //Get a list of neighboring characters
    static List<int[]> GetValidNeighbors(int x, int y)
    {
        List<int[]> positions = new List<int[]>();

        foreach (int[] p in GetNeighborCoordinates(x, y, lines[y].Length, lines.Length))
            positions.Add(p);

        return positions;
    }

    //Get the whole Number at these coordinates, including the digits before and after.
    static int GetWholeNum(int x, int y)
    {
        int lookLeft = 1;
        int lookRight = 1;
        if (!char.IsDigit(lines[y][x])) return -1;
        string num = "" + lines[y][x];

        while (x - lookLeft >= 0 && char.IsDigit(lines[y][x - lookLeft]))
        {
            num = lines[y][x - lookLeft] + num;
            lookLeft++;
        }
        while (x + lookRight < lines[y].Length && char.IsDigit(lines[y][x + lookRight]))
        {
            num = num + lines[y][x + lookRight];
            lookRight++;
        }
        if (num == "*" || num == "") return -1;
        return int.Parse(Regex.Replace(num, "[^0-9.]", ""));
    }

    //Get coordinates of the neighboring characters.
    static List<int[]> GetNeighborCoordinates(int x, int y, int maxX, int maxY)
    {
        List<int[]> ret = new List<int[]>();
        for (int i = x - 1; i <= x + 1; i++)
        {
            if (i < 0 || i >= maxX) continue;
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (j < 0 || j >= maxY) continue;
                if (i == x && j == y) continue;
                ret.Add(new int[] { i, j });
            }
        }
        return ret;
    }
}