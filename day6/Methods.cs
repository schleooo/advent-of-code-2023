

public static class Methods
{
    //Part One
    public static decimal GetPartOne(string[] lines)
    {
        List<Race> races = new List<Race>();
        List<string> splitLinesA = RemoveEmpty(lines[0].Split(" "));
        List<string> splitLinesB = RemoveEmpty(lines[1].Split(" "));
        for (int i = 1; i < splitLinesA.Count; i++)
        {
            races.Add(new Race(decimal.Parse(splitLinesA[i]), decimal.Parse(splitLinesB[i])));
        }

        decimal productA = 1;

        foreach (Race race in races)
        {
            decimal raceSum = 0;
            for (decimal i = 1; i < race.time; i++)
            {
                if (GetDistance(race.time, i) > race.distance) raceSum++;
            }
            productA *= raceSum;
        }

        return productA;
    }

    //Part Two
    public static decimal GetPartTwo(string[] lines)
    {
        Race bigRace = new Race(GetNumInLineCombined(lines[0]), GetNumInLineCombined(lines[1]));

        decimal raceSum = 0;
        for (decimal i = 1; i < bigRace.time; i++)
        {
            if (GetDistance(bigRace.time, i) > bigRace.distance) raceSum++;
        }
        return raceSum;
    }


    //returns distance travelled using time and chargetime values
    static decimal GetDistance(decimal time, decimal chargeTime)
    {
        return (time - chargeTime) * chargeTime;
    }

    //returns a list of string without the empty arrays element
    static List<string> RemoveEmpty(string[] arr)
    {
        List<string> ret = new List<string>();

        foreach (string s in arr)
        {
            if (s != "") ret.Add(s);
        }
        return ret;
    }

    //returns all numbers in a string combined to one
    static decimal GetNumInLineCombined(string s)
    {
        return decimal.Parse(System.Text.RegularExpressions.Regex.Replace(s, "[^0-9]", ""));
    }
}

//stores values of a race
class Race
{
    public decimal time;
    public decimal distance;

    public Race(decimal t, decimal d)
    {
        time = t;
        distance = d;
    }
}
