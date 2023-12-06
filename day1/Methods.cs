using System.Text.RegularExpressions;

public static class Methods
{
    //Part One
    public static int GetPartOne(string[] lines)
    {
        int sum = 0;
        foreach (string s in lines)
        {
            string numberOnly = Regex.Replace(s, "[^0-9.]", "");
            sum += int.Parse(numberOnly[0] + "" + numberOnly[numberOnly.Length - 1]);
        }
        return sum;
    }

    //Part Two
    public static int GetPartTwo(string[] lines)
    {
        int sum = 0;
        foreach (string s in lines)
        {
            string numberOnly = Regex.Replace(ReplaceWordWithNum(s), "[^0-9.]", "");
            sum += int.Parse(numberOnly[0] + "" + numberOnly[numberOnly.Length - 1]);
        }
        return sum;
    }


    //replaces all number-words with word-digit-word sandwich
    public static string ReplaceWordWithNum(string s)
    {
        var words = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        string ret = s;
        for (int i = 0; i <= 9; i++)
        {
            ret = ret.Replace(words[i], words[i] + i + words[i]);
        }
        return ret;
    }
}