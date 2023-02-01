using AdventOfCode05;

var lines = File.ReadAllLines("input.txt");
var stacks = new Stack<char>[9];
for (var i = 0; i < stacks.Length; i++)
{
    stacks[i] = new Stack<char>();
}
var commands = new List<Command>();

Setup();
Part2();

void Setup()
{
    var isReadCommands = false;
    var stacksLines = new List<string>();
    foreach (var line in lines)
    {
        if (string.IsNullOrEmpty(line))
        {
            isReadCommands = true;
            continue;
        }
        if (!isReadCommands)
        {
            stacksLines.Add(line);
        }
        else
        {
            var splits = line.Split(" from ");
            var quantity = int.Parse(splits[0].Split(" ")[1]);
            var from = int.Parse(splits[1].Split(" to ")[0]);
            var to = int.Parse(splits[1].Split(" to ")[1]);
            commands.Add(new Command
            {
                From = from,
                To = to,
                Quantity = quantity
            });
        }
    }

    stacksLines.Reverse();
    foreach (var line in stacksLines.Skip(1))
    {
        for (var i = 1; i < line.Length; i += 4)
        {
            if (char.IsUpper(line[i]))
            {
                stacks[(i - 1) / 4].Push(line[i]);
            }
        }
    }
}

void Part1()
{
    foreach (var command in commands)
    {
        for (var i = 0; i < command.Quantity; i++)
        {
            var element = stacks[command.From - 1].Pop();
            stacks[command.To - 1].Push(element);
        }
    }
    var topStacks = string.Concat(stacks.Select(x => x.First()));
    Console.WriteLine(topStacks);
}

void Part2()
{
    foreach (var command in commands)
    {
        var poppedElements = new List<char>();
        for (var i = 0; i < command.Quantity; i++)
        {
            var element = stacks[command.From - 1].Pop();
            poppedElements.Insert(0, element);
        }

        foreach (var element in poppedElements)
        {
            stacks[command.To - 1].Push(element);
        }
    }
    var topStacks = string.Concat(stacks.Select(x => x.First()));
    Console.WriteLine(topStacks);
}
