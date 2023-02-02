var lines = File.ReadAllLines("input.txt");

char[,] map;

var rows = 0;
var cols = 0;

var sandEntryCol = 0;

Setup(true);
Part2();

void Setup(bool addFloor = false)
{
    var points = new List<(int, int)>();
    foreach (var line in lines)
    {
        var splits = line.Split("->", StringSplitOptions.RemoveEmptyEntries);
        var origin = GetPointFromPair(splits[0]);
        for (var i = 1; i < splits.Length; i++)
        {
            var point = GetPointFromPair(splits[i]);
            var fixedCoord = 0;
            var movingCoordStart = 0;
            var movingCoordEnd = 0;
            var isCol = origin.Item1 == point.Item1;
            if (isCol)
            {
                fixedCoord = origin.Item1;
                movingCoordStart = Math.Min(origin.Item2, point.Item2);
                movingCoordEnd = Math.Max(origin.Item2, point.Item2);
            }
            else
            {
                fixedCoord = origin.Item2;
                movingCoordStart = Math.Min(origin.Item1, point.Item1);
                movingCoordEnd = Math.Max(origin.Item1, point.Item1);
            }
            for (var j = movingCoordStart; j <= movingCoordEnd; j++)
            {
                if (isCol)
                {
                    points.Add((fixedCoord, j));
                }
                else
                {
                    points.Add((j, fixedCoord));
                }
            }
            origin = point;
        }
    }

    var maxRow = points.Max(x => x.Item2);
    var minCol = points.Min(x => x.Item1);
    var maxCol = points.Max(x => x.Item1);

    rows = maxRow + 1;
    cols = maxCol - minCol + 1;
    sandEntryCol = 500 - minCol;

    var shiftCols = 0;
    if (addFloor)
    {
        rows += 2;
        cols *= 30;
        shiftCols = cols / 2 - sandEntryCol;
        sandEntryCol += shiftCols;
    }

    map = new char[rows, cols];

    for (var i = 0; i < map.GetLength(0); i++)
    {
        for (var j = 0; j < map.GetLength(1); j++)
        {
            if (i == 0 && j == sandEntryCol)
            {
                map[i, j] = '+';
            }
            else if (addFloor && i == rows - 1)
            {
                map[i, j] = '#';
            }
            else
            {
                map[i, j] = '.';
            }
        }
    }

    foreach (var point in points)
    {
        map[point.Item2, point.Item1 - minCol + shiftCols] = '#';
    }
}

void Part1()
{
    var sandX = 0;
    var sandY = sandEntryCol;
    var ifInfiniteFall = false;
    var countStillSand = 0;
    while (!ifInfiniteFall)
    {
        if (map[sandX, sandY] == 'x')
        {
            map[sandX, sandY] = '.';
        }
        var possibleCoords = new List<(int, int)>
        {
            (sandX + 1, sandY),
            (sandX + 1, sandY - 1),
            (sandX + 1, sandY + 1)
        };

        var newX = -1;
        var newY = -1;
        foreach (var coord in possibleCoords)
        {
            if (coord.Item1 >= rows || coord.Item2 < 0 || coord.Item2 >= cols)
            {
                ifInfiniteFall = true;
                break;
            }
            if (map[coord.Item1, coord.Item2] == '.')
            {
                newX = coord.Item1;
                newY = coord.Item2;
                break;
            }
        }
        if (newX >= 0 && newY >= 0)
        {
            sandX = newX;
            sandY = newY;
            map[sandX, sandY] = 'x';
        }
        else if (!ifInfiniteFall)
        {
            map[sandX, sandY] = 'o';
            countStillSand++;
            sandX = 0;
            sandY = sandEntryCol;
        }
        else
        {
            map[sandX, sandY] = '~';
        }
    }
    PrintMap();
    Console.WriteLine(countStillSand);
}

void Part2()
{
    var sandX = 0;
    var sandY = sandEntryCol;
    var countStillSand = 0;
    var isEntryBlocked = false;
    while (!isEntryBlocked)
    {
        if (map[sandX, sandY] == 'x')
        {
            map[sandX, sandY] = '.';
        }
        var possibleCoords = new List<(int, int)>
        {
            (sandX + 1, sandY),
            (sandX + 1, sandY - 1),
            (sandX + 1, sandY + 1)
        };

        var newX = -1;
        var newY = -1;
        foreach (var coord in possibleCoords)
        {
            if (coord.Item1 >= rows || coord.Item2 < 0 || coord.Item2 >= cols)
            {
                break;
            }
            if (map[coord.Item1, coord.Item2] == '.')
            {
                newX = coord.Item1;
                newY = coord.Item2;
                break;
            }
        }
        if (newX >= 0 && newY >= 0)
        {
            sandX = newX;
            sandY = newY;
            map[sandX, sandY] = 'x';
        }
        else
        {
            map[sandX, sandY] = 'o';
            countStillSand++;
            if (sandX == 0 && sandY == sandEntryCol)
            {
                isEntryBlocked = true;
            }
            else
            {
                sandX = 0;
                sandY = sandEntryCol;
            }

        }
    }
    //PrintMap();
    Console.WriteLine(countStillSand);
}



(int, int) GetPointFromPair(string input)
{
    var splits = input.Split(",");
    return (int.Parse(splits[0]), int.Parse(splits[1]));
}

void PrintMap()
{
    Console.Clear();
    for (var i = 0; i < map.GetLength(0); i++)
    {
        for (var j = 0; j < map.GetLength(1); j++)
        {
            Console.Write(map[i, j]);
        }
        Console.WriteLine();
    }
}