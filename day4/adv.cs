string[] lines = System.IO.File.ReadAllLines(@"input.txt");


List<Card> cards = new List<Card>();;

foreach (string l in lines)
    cards.Add(new Card(l));

int increment=0;

int sumA = 0;
int sumB = 0;

foreach (Card card in cards)
{
    string[] cardParts = card.content.Split(':')[1].Split('|');

    List<int> scratches = new List<int>();
    foreach (string s in cardParts[0].Split(' '))
        if(int.TryParse(s, out int add)) scratches.Add(add);
    List<int> numbers = new List<int>();
    foreach (string s in cardParts[1].Split(' '))
        if(int.TryParse(s, out int add)) numbers.Add(add);
        
    int matches = 0;
    foreach (int i in scratches)
    {
        if(numbers.Contains(i))matches++;
    }

    // Part 1

    if(matches > 0)
        sumA+=(int)Math.Pow(2,matches-1);

    // Part 2

    for (int i = 0; i < matches; i++)
    {
        if(cards.Count > increment+1+i){
            cards[increment+1+i].count+=card.count;
        }
    }
    sumB += card.count;
    increment++;
}

System.Console.WriteLine("---");
System.Console.WriteLine("Part one: " + sumA);
System.Console.WriteLine("Part two: " + sumB);
System.Console.WriteLine("---");

class Card{
    public string content;
    public int count = 1;

    public Card(string c){
        content = c;
    }
}
