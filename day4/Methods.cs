using System.Text.RegularExpressions;

public static class Methods
{
    static List<Card> cards = new List<Card>();

    //Part One
    public static int GetPartOne(string[] lines)
    {
        cards = GetCards(lines);
        int sum = 0;

        foreach (Card card in cards)
        {
            int matches = GetMatches(card);
            if (matches > 0)
                sum += (int)Math.Pow(2, matches - 1);
        }
        return sum;
    }

    //Part Two
    public static int GetPartTwo(string[] lines)
    {
        cards = GetCards(lines);
        int sum = 0;
        int increment = 0;

        foreach (Card card in cards)
        {
            int matches = GetMatches(card);
            for (int i = 0; i < matches; i++)
            {
                if (cards.Count > increment + 1 + i)
                {
                    cards[increment + 1 + i].count += card.count;
                }
            }
            sum += card.count;
            increment++;
        }
        return sum;
    }

    static List<Card> GetCards(string[] lines)
    {
        List<Card> cards = new List<Card>(); ;

        foreach (string l in lines)
            cards.Add(new Card(l));

        return cards;
    }

    static int GetMatches(Card card)
    {
        string[] cardParts = card.content.Split(':')[1].Split('|');

        List<int> scratches = new List<int>();
        foreach (string s in cardParts[0].Split(' '))
            if (int.TryParse(s, out int add)) scratches.Add(add);
        List<int> numbers = new List<int>();
        foreach (string s in cardParts[1].Split(' '))
            if (int.TryParse(s, out int add)) numbers.Add(add);

        int matches = 0;
        foreach (int i in scratches)
        {
            if (numbers.Contains(i)) matches++;
        }

        return matches;
    }
}

class Card
{
    public string content;
    public int count = 1;

    public Card(string c)
    {
        content = c;
    }
}
