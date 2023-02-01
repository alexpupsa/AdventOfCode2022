Part2();

static void Part1()
{
    var lines = File.ReadAllLines("input.txt");
    var pairsThatFullyContainTheOther = 0;
    foreach (var line in lines)
    {
        var isPairFullyContained = false;
        var ranges = line.Split(',');
        var range1 = ranges[0].Split('-');
        var min1 = int.Parse(range1[0]);
        var max1 = int.Parse(range1[1]);
        var range2 = ranges[1].Split('-');
        var min2 = int.Parse(range2[0]);
        var max2 = int.Parse(range2[1]);
        if (DoesFirstRangeContainSecondRange(min1, max1, min2, max2))
        {
            isPairFullyContained = true;
        }
        else if (DoesFirstRangeContainSecondRange(min2, max2, min1, max1))
        {
            isPairFullyContained = true;
        }
        if (isPairFullyContained)
        {
            pairsThatFullyContainTheOther++;
        }
    }
    Console.WriteLine(pairsThatFullyContainTheOther);
}

static void Part2()
{
    var lines = File.ReadAllLines("input.txt");
    var pairsThatIntersectTheOther = 0;
    foreach (var line in lines)
    {
        var ranges = line.Split(',');
        var range1 = ranges[0].Split('-');
        var min1 = int.Parse(range1[0]);
        var max1 = int.Parse(range1[1]);
        var range2 = ranges[1].Split('-');
        var min2 = int.Parse(range2[0]);
        var max2 = int.Parse(range2[1]);
        if (DoesPairInteresectTheOther(min1, max1, min2, max2))
        {
            pairsThatIntersectTheOther++;
        }
    }
    Console.WriteLine(pairsThatIntersectTheOther);
}

static bool DoesFirstRangeContainSecondRange(int min1, int max1, int min2, int max2)
{
    return min2 >= min1 && max2 <= max1;
}

static bool DoesPairInteresectTheOther(int min1, int max1, int min2, int max2)
{
    return !(max1 < min2 || max2 < min1);
}