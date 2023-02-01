var signal = File.ReadAllText("input.txt");

Part2();

void Part1()
{
    FindSequence(4);
}

void Part2()
{
    FindSequence(14);
}

void FindSequence(int numberOfCharacters)
{
    var sequenceFound = false;
    var skip = 0;
    while (!sequenceFound)
    {
        var distinctCount = signal.Skip(skip).Take(numberOfCharacters).Distinct().Count();
        if (distinctCount == numberOfCharacters)
        {
            Console.WriteLine(skip + numberOfCharacters);
            sequenceFound = true;
        }
        skip++;
    }
}