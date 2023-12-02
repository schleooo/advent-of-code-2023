string[] lines = System.IO.File.ReadAllLines(@"input.txt");

int sumA =0;
int sumB =0;

for (int i = 0; i < lines.Length; i++)
{
    //Part one
    if(IsInRange(lines[i])) sumA+=i+1;

    //Part two
    sumB += GetBallPower(lines[i]);

}

System.Console.WriteLine(sumA); //2283

System.Console.WriteLine(sumB); //78669




bool IsInRange(string game){
    foreach (string part in game.Split(';'))
        {
            string thisPart = part.Split(':')[part.Split(':').Length-1];
            
            int[] theseBalls = new int[3];
            foreach (string balls in thisPart.Split(','))
            {
                if(balls.Contains(" red")) theseBalls[0] = int.Parse(balls.Replace(" red", ""));
                if(balls.Contains(" green")) theseBalls[1] = int.Parse(balls.Replace(" green", ""));
                if(balls.Contains(" blue")) theseBalls[2] = int.Parse(balls.Replace(" blue", ""));
            }
            
            if(theseBalls[0] > 12) return false;
            if(theseBalls[1] > 13) return false;
            if(theseBalls[2] > 14) return false;
        }
    return true;
}

int GetBallPower(string game){

    List<int[]> arrays = new List<int[]>();

    foreach (string part in game.Split(';'))
    {
        string thisPart = part.Split(':')[part.Split(':').Length-1];
        
        int[] theseBalls = new int[3];
        foreach (string balls in thisPart.Split(','))
        {
            if(balls.Contains(" red")) theseBalls[0] = int.Parse(balls.Replace(" red", ""));
            if(balls.Contains(" green")) theseBalls[1] = int.Parse(balls.Replace(" green", ""));
            if(balls.Contains(" blue")) theseBalls[2] = int.Parse(balls.Replace(" blue", ""));
        }
        arrays.Add(theseBalls);
    }

    int minR = 0;
    int minG = 0;
    int minB = 0;

    foreach (int[] arr in arrays)
    {
        if(arr[0]>minR) minR = arr[0];
        if(arr[1]>minG) minG = arr[1];
        if(arr[2]>minB) minB = arr[2];
    }

    return minR*minG*minB;
}

