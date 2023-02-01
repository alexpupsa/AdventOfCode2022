string[] commands;

Setup();
Part2();

void Setup()
{
    commands = File.ReadAllLines("input.txt");
}

void Part1()
{
    var hX = 0;
    var hY = 0;
    var tX = 0;
    var tY = 0;
    var tailPositions = new List<string>
    {
        $"{tX}#{tY}"
    };
    foreach (var command in commands)
    {
        var splits = command.Split(' ');
        var position = splits[0];
        var steps = int.Parse(splits[1]);
        for (var i = 0; i < steps; i++)
        {
            switch (position)
            {
                case "U": hY -= 1; break;
                case "D": hY += 1; break;
                case "L": hX -= 1; break;
                case "R": hX += 1; break;
            }
            (tX, tY) = MoveTail(tX, tY, hX, hY);
            tailPositions.Add($"{tX}#{tY}");
        }
    }
    var count = tailPositions.Distinct().Count();
    Console.WriteLine(count);
}

void Part2()
{
    var hX = 0;
    var hY = 0;
    var tX = new int[9];
    var tY = new int[9];
    var tail9Positions = new List<string>
    {
        $"{tX[8]}#{tY[8]}"
    };
    foreach (var command in commands)
    {
        var splits = command.Split(' ');
        var position = splits[0];
        var steps = int.Parse(splits[1]);
        foreach (var step in Enumerable.Range(1, steps))
        {
            switch (position)
            {
                case "U": hY -= 1; break;
                case "D": hY += 1; break;
                case "L": hX -= 1; break;
                case "R": hX += 1; break;
            }
            var fX = hX;
            var fY = hY;
            foreach (var tail in Enumerable.Range(0, 9))
            {
                (tX[tail], tY[tail]) = MoveTail(tX[tail], tY[tail], fX, fY);
                fX = tX[tail];
                fY = tY[tail];
                if (tail == 8)
                {
                    tail9Positions.Add($"{tX[tail]}#{tY[tail]}");
                }
            }

        }
    }
    var count = tail9Positions.Distinct().Count();
    Console.WriteLine(count);
}

static (int, int) MoveTail(int tX, int tY, int hX, int hY)
{
    if (tX != hX || tY != hY)
    {
        if (tX == hX)
        {
            if (hY - tY > 1)
            {
                tY++;
            }
            else if (hY - tY < -1)
            {
                tY--;
            }
        }
        else if (tY == hY)
        {
            if (hX - tX > 1)
            {
                tX++;
            }
            else if (hX - tX < -1)
            {
                tX--;
            }
        }
        else if (hY - tY > 1 || hY - tY < -1 || hX - tX > 1 || hX - tX < -1)
        {
            if (hY > tY)
            {
                tY++;
            }
            else if (hY < tY)
            {
                tY--;
            }
            if (hX > tX)
            {
                tX++;
            }
            else if (hX < tX)
            {
                tX--;
            }
        }
    }
    return (tX, tY);
}