

public static class Methods
{
    //Part One
    public static int GetPartOne(string[] lines)
    {
        int sum = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            if (IsInRange(lines[i])) sum += i + 1;
        }
        return sum;
    }

    //Part Two
    public static int GetPartTwo(string[] lines)
    {
        int sum = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            sum += GetBallPower(lines[i]);
        }
        return sum;
    }







    //Methods

    static bool IsInRange(string game)
    {
        foreach (string part in game.Split(';'))
        {
            if (GetTheseBalls(part)[0] > 12) return false;
            if (GetTheseBalls(part)[1] > 13) return false;
            if (GetTheseBalls(part)[2] > 14) return false;
        }
        return true;
    }

    static int GetBallPower(string game)
    {
        int minR = 0, minG = 0, minB = 0;

        foreach (string part in game.Split(';'))
        {
            int[] arr = GetTheseBalls(part);
            if (arr[0] > minR) minR = arr[0];
            if (arr[1] > minG) minG = arr[1];
            if (arr[2] > minB) minB = arr[2];
        }

        return minR * minG * minB;
    }

    static int[] GetTheseBalls(string part)
    {
        string thisPart = part.Split(':')[part.Split(':').Length - 1];

        int[] theseBalls = new int[3];
        foreach (string balls in thisPart.Split(','))
        {
            if (balls.Contains(" red")) theseBalls[0] = int.Parse(balls.Replace(" red", ""));
            if (balls.Contains(" green")) theseBalls[1] = int.Parse(balls.Replace(" green", ""));
            if (balls.Contains(" blue")) theseBalls[2] = int.Parse(balls.Replace(" blue", ""));
        }
        return theseBalls;
    }






}