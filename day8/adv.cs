string[] lines = File.ReadAllLines(@"input.txt");
string[] lines2 = File.ReadAllLines(@"input2.txt");


System.Console.WriteLine("---");

var watch = System.Diagnostics.Stopwatch.StartNew();
System.Console.WriteLine("Part One: " + Methods.GetPartOne(lines) + TimeElapsed()); //5ms
System.Console.WriteLine("Part Two: " + Methods.GetPartTwo(lines2) + TimeElapsed()); //16500ms

System.Console.WriteLine("---");


//returns a string of the elapsed time and resets the timer
string TimeElapsed(bool afterText = true){
    var elapsed = watch.ElapsedMilliseconds;
    watch.Restart();
    if(afterText) return"   (" + elapsed + "ms)";
    else return"(" + elapsed + "ms)";
}




