using AdventOfCode11;

var monkeysInput = File.ReadAllLines("input.txt");
var monkeys = new List<Monkey>();

Setup();
Part2();

void Setup()
{
    var count = 0;
    while (true)
    {
        var monkeyLines = monkeysInput.Skip(count * 7).Take(7).ToList();
        if (!monkeyLines.Any()) break;
        var monkeyNumber = int.Parse(monkeyLines[0]["Monkey ".Length..].Trim(':'));
        var itemsString = monkeyLines[1]["  Starting items: ".Length..];
        var items = itemsString.Split(", ", StringSplitOptions.RemoveEmptyEntries);
        var operationString = monkeyLines[2]["  Operation: new = old ".Length..];
        var divisibleBy = int.Parse(monkeyLines[3]["  Test: divisible by ".Length..]);
        var monkeyTrue = int.Parse(monkeyLines[4]["    If true: throw to monkey ".Length..]);
        var monkeyFalse = int.Parse(monkeyLines[5]["    If false: throw to monkey ".Length..]);
        var operation = Operation.Add;
        var operationValue = 0;
        if (operationString.StartsWith("*"))
        {
            operation = Operation.Multiply;
        }
        var splits = operationString.Split(' ');
        if (splits[1] == "old")
        {
            operation = Operation.Power;
        }
        else
        {
            operationValue = int.Parse(splits[1]);
        }
        var monkey = new Monkey
        {
            Number = monkeyNumber,
            Items = items.Select(x => long.Parse(x)).ToList(),
            Operation = operation,
            OperationValue = operationValue,
            DivisibleBy = divisibleBy,
            MonkeyTrue = monkeyTrue,
            MonkeyFalse = monkeyFalse
        };
        monkeys.Add(monkey);
        count++;
    }
}

void Part1()
{
    for (var i = 0; i < 20; i++)
    {
        foreach (var monkey in monkeys)
        {
            if (!monkey.Items.Any()) continue;
            foreach (var item in monkey.Items)
            {
                long itemValue = 0;
                switch (monkey.Operation)
                {
                    case Operation.Add: itemValue = item + monkey.OperationValue; break;
                    case Operation.Multiply: itemValue = item * monkey.OperationValue; break;
                    case Operation.Power: itemValue = item * item; break;
                }
                itemValue /= 3;
                if (itemValue % monkey.DivisibleBy == 0)
                {
                    GiveItemToAnotherMonkey(itemValue, monkey.MonkeyTrue);
                }
                else
                {
                    GiveItemToAnotherMonkey(itemValue, monkey.MonkeyFalse);
                }
            }
            monkey.InspectedItems += monkey.Items.Count;
            monkey.Items.Clear();
        }
    }

    var monkeyBusiness = monkeys.OrderByDescending(x => x.InspectedItems).Take(2).Select(x => x.InspectedItems).Aggregate((x, y) => x * y);
    Console.WriteLine(monkeyBusiness);
}

void Part2()
{
    var factor = monkeys.Select(x => x.DivisibleBy).Aggregate((x, y) => x * y);

    for (var i = 0; i < 10000; i++)
    {
        foreach (var monkey in monkeys.OrderBy(x => x.Number).ToList())
        {
            if (monkey.Items.Count == 0) continue;
            foreach (var item in monkey.Items)
            {
                long itemValue = 0;
                switch (monkey.Operation)
                {
                    case Operation.Add: itemValue = item + monkey.OperationValue; break;
                    case Operation.Multiply: itemValue = item * monkey.OperationValue; break;
                    case Operation.Power: itemValue = item * item; break;
                }
                itemValue %= factor;
                if (itemValue % monkey.DivisibleBy == 0)
                {
                    GiveItemToAnotherMonkey(itemValue, monkey.MonkeyTrue);
                }
                else
                {
                    GiveItemToAnotherMonkey(itemValue, monkey.MonkeyFalse);
                }
            }
            monkey.InspectedItems += monkey.Items.Count;
            monkey.Items.Clear();
        }
    }

    var monkeyBusiness = monkeys.OrderByDescending(x => x.InspectedItems).Take(2).Select(x => x.InspectedItems).Aggregate((x, y) => x * y);
    Console.WriteLine(monkeyBusiness);
}


void GiveItemToAnotherMonkey(long item, int monkeyNumber)
{
    var monkey = monkeys.First(x => x.Number == monkeyNumber);
    monkey.Items.Add(item);
}