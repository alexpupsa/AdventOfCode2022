using System.Text;

string[] commands;

Setup();
Part2();

void Setup()
{
    commands = File.ReadAllLines("input.txt");
}

void Part1()
{
    var cycle = 1;
    var addxStarted = false;
    var increment = 0;
    var x = 1;
    var signalStrengthSum = 0;
    var commandCount = 0;
    while (cycle <= 220)
    {
        if ((cycle - 20) % 40 == 0)
        {
            signalStrengthSum += cycle * x;
        }
        if (addxStarted)
        {
            x += increment;
            addxStarted = false;
        }
        else
        {
            var command = commands.Skip(commandCount).FirstOrDefault();
            if (command != null && command.StartsWith("addx"))
            {
                var splits = command.Split(' ');
                increment = int.Parse(splits[1]);
                addxStarted = true;
            }
            commandCount++;
        }
        cycle++;
    }
    Console.WriteLine(signalStrengthSum);
}

void Part2()
{
    var cycle = 1;
    var addxStarted = false;
    var increment = 0;
    var spritePosition = 1;
    var commandCount = 0;
    var crtLines = new List<string>();
    var currentCrtLine = new StringBuilder();
    while (cycle <= 241)
    {
        var cycleInCurrentRow = cycle % 40;
        if (cycleInCurrentRow == 0) cycleInCurrentRow = 40;
        // change row
        if (cycleInCurrentRow == 1)
        {
            crtLines.Add(currentCrtLine.ToString());
            currentCrtLine.Clear();
        }

        // add current character to CRT line
        if (cycleInCurrentRow - 1 >= spritePosition - 1 && cycleInCurrentRow - 1 <= spritePosition + 1)
        {
            currentCrtLine.Append('#');
        }
        else
        {
            currentCrtLine.Append('.');
        }

        // handle command
        if (addxStarted)
        {
            spritePosition += increment;
            addxStarted = false;
        }
        else
        {
            var command = commands.Skip(commandCount).FirstOrDefault();
            if (command != null && command.StartsWith("addx"))
            {
                var splits = command.Split(' ');
                increment = int.Parse(splits[1]);
                addxStarted = true;
            }
            commandCount++;
        }
        cycle++;
    }

    foreach (var line in crtLines)
    {
        Console.WriteLine(line);
    }
}