using AdventOfCode13;

var lines = File.ReadAllLines("input.txt");
var pairs = new List<Tuple<string, string>>();

Setup();
Part1();

void Setup()
{
    var numberOfPairs = (lines.Length + 1) / 3;
    for (var i = 0; i < numberOfPairs; i++)
    {
        var items = lines.Skip(i * 3).Take(2);
        pairs.Add(new Tuple<string, string>(items.First(), items.Last()));
    }
}

void Part1()
{
    var indexSum = 0;
    var index = 1;
    foreach (var pair in pairs)
    {
        if (AreListsInOrder(pair.Item1, pair.Item2, false))
        {
            indexSum += index;
            Console.WriteLine(index);
        }
        index++;
    }
    Console.WriteLine(indexSum);
}

static bool AreListsInOrder(string item1, string item2, bool isListConverted)
{
    var list1 = item1.TrimOneCharacter(new char[] { '[', ']' }).SpecialSplit();
    var list2 = item2.TrimOneCharacter(new char[] { '[', ']' }).SpecialSplit();
    if (!isListConverted && list1.Length > list2.Length) return false;
    for (int i = 0; i < list1.Length; i++)
    {
        if (i >= list2.Length) break;
        isListConverted = false;
        if (list1[i].StartsWith("[") || list2[i].StartsWith("["))
        {
            if (!list1[i].StartsWith("["))
            {
                list1[i] = $"[{list1[i]}]";
                isListConverted = true;
            }
            if (!list2[i].StartsWith("["))
            {
                list2[i] = $"[{list2[i]}]";
                isListConverted = true;
            }
            var checkLists = AreListsInOrder(list1[i], list2[i], isListConverted);
            if (!checkLists) return false;
        }
        else
        {
            if (int.Parse(list1[i]) > int.Parse(list2[i])) return false;
        }
    }
    return true;
}
