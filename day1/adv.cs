using System.Text.RegularExpressions;

string[] lines = System.IO.File.ReadAllLines(@"input.txt");

int sumA = 0;
int sumB = 0;

foreach (string s in lines)
{
    //Part One
    string numberOnlyA = Regex.Replace(s, "[^0-9.]", "");
    sumA += int.Parse(numberOnlyA[0]+""+numberOnlyA[numberOnlyA.Length-1]);

    //Part Two
    string numberOnlyB = Regex.Replace(s, "one", "one1one");
    numberOnlyB = Regex.Replace(numberOnlyB, "two", "two2two");
    numberOnlyB = Regex.Replace(numberOnlyB, "three", "three3three");
    numberOnlyB = Regex.Replace(numberOnlyB, "four", "four4four");
    numberOnlyB = Regex.Replace(numberOnlyB, "five", "five5five");
    numberOnlyB = Regex.Replace(numberOnlyB, "six", "six6six");
    numberOnlyB = Regex.Replace(numberOnlyB, "seven", "seven7seven");
    numberOnlyB = Regex.Replace(numberOnlyB, "eight", "eight8eight");
    numberOnlyB = Regex.Replace(numberOnlyB, "nine", "nine9nine");
    numberOnlyB = Regex.Replace(numberOnlyB, "zero", "zero0zero");

    numberOnlyB = Regex.Replace(numberOnlyB, "[^0-9.]", "");

    sumB += int.Parse(numberOnlyB[0]+""+numberOnlyB[numberOnlyB.Length-1]);
}

System.Console.WriteLine("---");
System.Console.WriteLine("Part one: " + sumA);
System.Console.WriteLine("Part two: " + sumB);
System.Console.WriteLine("---");


