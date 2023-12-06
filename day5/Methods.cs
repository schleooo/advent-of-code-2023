using System.Text.RegularExpressions;

public static class Methods
{
    //Part One
    public static uint GetPartOne(string[] lines)
    {
        uint currentLowestA = uint.MaxValue;
        foreach (string i in lines[0].Split(' '))
        {
            if (i == "seeds:") continue;
            uint thisVal = MapValueAllMaps(SplitMaps(lines), uint.Parse(i));
            currentLowestA = Math.Min(currentLowestA, thisVal);
        }
        return currentLowestA;
    }

    //Part Two
    public static uint GetPartTwo(string[] lines)
    {
        uint currentLowestB = uint.MaxValue;
        uint currentStart = 0;

        for (int i = 0; i < lines[0].Split(' ').Length; i++)
        {
            if (i == 0) { }
            else if (i % 2 == 1)
                currentStart = uint.Parse(lines[0].Split(' ')[i]);
            else if (i % 2 == 0)
            {
                uint currentLength = uint.Parse(lines[0].Split(' ')[i]);

                // oh mein gott, run nicht diesen code mit dem kompletten input
                // die loop läuft 5 milliarden mal literally es hat a stund geladen
                for (uint j = currentStart; j < currentStart + currentLength; j++)
                {
                    uint thisVal = MapValueAllMaps(SplitMaps(lines), j);
                    currentLowestB = Math.Min(currentLowestB, thisVal);
                }
            }
        }
        return currentLowestB;
    }


    //convert a string into a map object
    static Map MapFromString(string s)
    {
        string[] ss = s.Split(' ');
        return new Map(uint.Parse(ss[0]), uint.Parse(ss[1]), uint.Parse(ss[2]));
    }

    // get all lists of mappings
    static List<List<Map>> SplitMaps(string[] input)
    {
        List<List<Map>> returnList = new List<List<Map>>();
        List<Map> currentMaps = new List<Map>();

        foreach (string s in input)
        {
            if (s == "")
            {
                returnList.Add(currentMaps);
                currentMaps = new List<Map>();
                continue;
            }
            if (s.Contains("map") || s.Contains("seed")) continue;
            currentMaps.Add(MapFromString(s));
        }
        returnList.Add(currentMaps);
        return returnList;
    }

    //do the math for mapping a value
    static uint MapValue(List<Map> maps, uint value)
    {
        foreach (Map map in maps)
            if (value >= map.sourceStart && value < map.sourceStart + map.length)
                return value - map.sourceStart + map.destinationStart;

        return value;
    }

    //iterate through all maps to get the final mapped value
    static uint MapValueAllMaps(List<List<Map>> mapsList, uint value)
    {
        uint newVal = value;
        foreach (List<Map> maps in mapsList)
        {
            newVal = MapValue(maps, newVal);
        }
        return newVal;
    }
}


//object for storing data about each map
class Map
{
    public uint destinationStart;
    public uint sourceStart;
    public uint length;

    public Map(uint ds, uint ss, uint l)
    {
        destinationStart = ds;
        sourceStart = ss;
        length = l;
    }
}