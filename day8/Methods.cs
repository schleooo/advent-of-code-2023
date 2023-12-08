using System.Linq;
using System.Linq.Expressions;

public static class Methods
{
    //Part One
    public static int GetPartOne(string[] lines)
    {
        SetupVars(lines, out string instructions, out Dictionary<string, (string, string)> locations, out List<string> ogcurrentpos, out List<string> currentpos);

        int iteration = 0;
        string currentPos = "AAA";

        while (currentPos != "ZZZ")
        {
            currentPos = GetNextString(instructions, iteration, locations, currentPos);
            iteration++;
        }

        return iteration;
    }

    //Part Two
    public static decimal GetPartTwo(string[] lines)
    {
        SetupVars(lines, out string instructions, out Dictionary<string, (string, string)> locations, out List<string> ogcurrentPos, out List<string> currentPos);
        currentPos = ogcurrentPos;

        int iteration = 0;

        Dictionary<int, List<(string, int)>> positionsVisited = new Dictionary<int, List<(string, int)>>();
        List<int> zpositions = new List<int>();
        bool[] loops = new bool[currentPos.Count];

        while (true)
        {
            for (int i = 0; i < currentPos.Count; i++)
            {
                if (loops[i]) continue;

                currentPos[i] = GetNextString(instructions, iteration, locations, currentPos[i]);

                if (positionsVisited.ContainsKey(i))
                {
                    if (positionsVisited[i].Contains((currentPos[i], iteration % instructions.Length)))
                        loops[i] = true;

                    positionsVisited[i].Add((ogcurrentPos[i], iteration % instructions.Length));
                }
                else positionsVisited.Add(i, new List<(string, int)>() { (currentPos[i], iteration % instructions.Length) });

                if (currentPos[i].EndsWith('Z'))
                    zpositions.Add(iteration + 1);
            }

            if (!loops.Contains(false)) break;

            iteration++;
        }

        return GetLCMOfList(zpositions);
    }



    static void SetupVars(string[] lines, out string instructions, out Dictionary<string, (string, string)> locations, out List<string> ogcurrentpos, out List<string> currentpos)
    {
        instructions = lines[0];
        locations = new Dictionary<string, (string, string)>();

        ogcurrentpos = new List<string>();
        currentpos = new List<string>();

        foreach (string line in lines)
        {
            if (line == instructions || line == "") continue;

            locations.Add(line.Split(" = (")[0],
                (
                    line.Split(" = (")[1].Split(", ")[0],
                    line.Split(" = (")[1].Split(", ")[1].Replace(")", "")
                ));
            if (locations.Last().Key.EndsWith('A')) ogcurrentpos.Add(locations.Last().Key);
        }
    }

    static string GetNextString(string instructions, int iteration, Dictionary<string, (string, string)> locations, string v)
    {
        char thisPos = instructions[iteration % instructions.Length];

        if (thisPos == 'L') return locations[v].Item1;
        else return locations[v].Item2;

    }


    // Copied LCM calculation from the internet

    // Method to calculate the Greatest Common Divisor (GCD) of two numbers
    static decimal GCD(decimal a, decimal b)
    {
        while (b != 0)
        {
            decimal temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    // Method to calculate the least common multiple (LCM) of two numbers
    static decimal LCM(decimal a, decimal b)
    {
        return (a * b) / GCD(a, b);
    }

    // Method to calculate the LCM of a list of integers
    static decimal GetLCMOfList(List<int> numbers)
    {
        decimal lcm = numbers[0];

        for (int i = 1; i < numbers.Count; i++)
        {
            lcm = LCM(lcm, numbers[i]);
        }

        return lcm;
    }
}



