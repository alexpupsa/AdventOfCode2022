var lines = File.ReadAllLines("input.txt");
var totalSum = 0;

// part 1
//foreach (var line in lines)
//{
//    var compartment1 = line.Take(line.Length / 2);
//    var compartment2 = line.Skip(line.Length / 2);
//    var commonItem = compartment1.Intersect(compartment2).FirstOrDefault();
//    if (commonItem > 0)
//    {
//        if (char.IsUpper(commonItem))
//        {
//            totalSum += commonItem - 38;
//        }
//        else
//        {
//            totalSum += commonItem - 96;
//        }
//    }
//}

// part 2

var itemsDictionary = new Dictionary<char, char[]>();
for (var i = 0; i < lines.Length; i += 3)
{
    UpdateDictionary(lines[i], 1, ref itemsDictionary);
    UpdateDictionary(lines[i + 1], 2, ref itemsDictionary);
    UpdateDictionary(lines[i + 2], 3, ref itemsDictionary);
    var commonItem = itemsDictionary.FirstOrDefault(x => x.Value.All(y => y == '1')).Key;
    if (commonItem > 0)
    {
        if (char.IsUpper(commonItem))
        {
            totalSum += commonItem - 38;
        }
        else
        {
            totalSum += commonItem - 96;
        }
    }
    itemsDictionary.Clear();
}

Console.WriteLine(totalSum);

void UpdateDictionary(string items, int rucksackNumber, ref Dictionary<char, char[]> dictionary)
{
    foreach (var item in items)
    {
        if (!dictionary.ContainsKey(item))
        {
            var code = "000".ToCharArray();
            code[rucksackNumber - 1] = '1';
            dictionary.Add(item, code);
        }
        else
        {
            dictionary[item][rucksackNumber - 1] = '1';
        }
    }
}

