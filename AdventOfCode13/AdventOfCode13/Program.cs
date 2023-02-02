using AdventOfCode13;

var lines = File.ReadAllLines("input.txt");
var pairs = new List<Tuple<string, string>>();

Setup();
Part2();

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
        if (AreListsInOrder(pair.Item1, pair.Item2))
        {
            indexSum += index;
            Console.WriteLine(index);
        }
        index++;
    }
    Console.WriteLine(indexSum);
}

void Part2()
{
    var items = new List<string>();
    foreach (var pair in pairs)
    {
        items.Add(pair.Item1);
        items.Add(pair.Item2);
    }
    var divider1 = "[[2]]";
    var divider2 = "[[6]]";
    var index1 = 1;
    var index2 = index1 + 1;
    foreach (var item in items)
    {
        if (!AreListsInOrder(divider1, item))
        {
            index1++;
        }
        if (!AreListsInOrder(divider2, item))
        {
            index2++;
        }
    }
    Console.WriteLine($"{index1} * {index2} = {index1 * index2}");
}

static bool AreListsInOrder(string item1, string item2)
{
    var list1 = item1.TrimOneCharacter(new char[] { '[', ']' }).SpecialSplit();
    var list2 = item2.TrimOneCharacter(new char[] { '[', ']' }).SpecialSplit();
    for (int i = 0; i < list1.Length; i++)
    {
        if (i >= list2.Length) return false;
        if (list1[i].StartsWith("[") || list2[i].StartsWith("["))
        {
            if (!list1[i].StartsWith("["))
            {
                list1[i] = $"[{list1[i]}]";
            }
            if (!list2[i].StartsWith("["))
            {
                list2[i] = $"[{list2[i]}]";
            }
            if (list1[i] != "[]" || list2[i] != "[]")
            {
                return AreListsInOrder(list1[i], list2[i]);
            }
        }
        else
        {
            var n1 = int.Parse(list1[i]);
            var n2 = int.Parse(list2[i]);
            if (n1 > n2)
            {
                return false;
            }
            else if (n1 < n2)
            {
                return true;
            }
        }
    }
    return true;
}
