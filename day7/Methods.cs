

public static class Methods
{
    static List<Hand> hands = new List<Hand>();
    static List<Hand>[] handGroups = new List<Hand>[7];

    static bool useJokers;

    //Part One
    public static int GetPartOne(string[] lines)
    {
        useJokers = false;
        SetupVars(lines);

        foreach (Hand currentHand in hands)
        {
            currentHand.overrideType = Type(currentHand.hand);
            handGroups[currentHand.overrideType - 1].Add(currentHand);
        }

        return GetSumOfHands();
    }

    //Part Two
    public static int GetPartTwo(string[] lines)
    {
        useJokers = true;
        SetupVars(lines);

        foreach (Hand currentHand in hands)
        {
            currentHand.overrideType = TypeWithJoker(currentHand.hand);
            handGroups[currentHand.overrideType - 1].Add(currentHand);
        }

        return GetSumOfHands();
    }


    static void SetupVars(string[] lines)
    {
        hands = new List<Hand>();
        handGroups = new List<Hand>[7];
        
        for (int i = 0; i < handGroups.Length; i++)
            handGroups[i] = new List<Hand>();

        foreach (string s in lines)
            hands.Add(new Hand(s.Split(' ')[0], int.Parse(s.Split(' ')[1])));
    }

    static int GetSumOfHands()
    {
        int sum = 0;
        int rank = 1;

        foreach (List<Hand> group in handGroups)
        {
            group.Sort();
            foreach (Hand hand in group)
            {
                // System.Console.WriteLine(hand.hand + " " + hand.overrideType +  " " + hand.bet + " " + rank);
                sum += hand.bet * rank;
                rank++;
            }
        }
        return sum;
    }


    static int Type(string hand)
    {
        Dictionary<char, int> handVals = new Dictionary<char, int>();
        foreach (char c in hand)
        {
            if (useJokers && c == 'J') continue;
            if (handVals.ContainsKey(c)) handVals[c]++;
            else handVals.Add(c, 1);
        }

        handVals = handVals.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        string valsString = "";
        foreach (KeyValuePair<char, int> kv in handVals)
            valsString += kv.Value;

        if (valsString.Contains('5')) return 7;
        if (valsString.Contains('4')) return 6;
        if (valsString.Contains("23")) return 5;
        if (valsString.Contains('3')) return 4;
        if (valsString.Contains("22")) return 3;
        if (valsString.Contains('2')) return 2;
        return 1;
    }

    static int TypeWithJoker(string hand)
    {
        int ret = Type(hand);
        if (hand.Contains('J'))
            for (int i = 2; i <= 15; i++)
            {
                if (ValueToSymbol(i, out char c))
                {
                    int type = Type(hand.Replace('J', c));
                    if (type > ret)
                        ret = type;
                }
            }

        return ret;
    }

    public static int SymbolToValue(char c)
    {
        switch (c)
        {
            case 'A': return 15;
            case 'K': return 14;
            case 'Q': return 13;
            case 'J': return useJokers ? 1 : 12;
            case 'T': return 10;
            default: return int.Parse(c.ToString());
        }
    }

    public static bool ValueToSymbol(int i, out char c)
    {
        switch (i)
        {
            case 15: c = 'A'; return true;
            case 14: c = 'K'; return true;
            case 13: c = 'Q'; return true;
            case 12: c = ' '; return false;
            case 10: c = 'T'; return true;
            default: c = i.ToString()[0]; return true;
        }
    }
}



class Hand : IComparable<Hand>
{
    public string hand;
    public int bet;
    public int overrideType = 0;

    public Hand(string h, int b)
    {
        hand = h;
        bet = b;
    }

    public int CompareTo(Hand? other)
    {
        if (other == null) return 1;

        for (int i = 0; i < 5; i++)
        {
            if (this.hand[i] == other.hand[i]) continue;
            return Methods.SymbolToValue(this.hand[i]).CompareTo(Methods.SymbolToValue(other.hand[i]));
        }
        return 0;
    }
}



