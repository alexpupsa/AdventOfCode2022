var input = File.ReadAllLines("input.txt");

var coords = new HashSet<(int, int, int)>();

Setup();
Part2();

void Setup()
{
    foreach (var line in input)
    {
        var splits = line.Split(',');
        coords.Add(new(int.Parse(splits[0]), int.Parse(splits[1]), int.Parse(splits[2])));
    }
}

void Part1()
{
    var count = 0;
    foreach (var coord in coords)
    {
        var neighbors = new List<(int, int, int)>
        {
            (coord.Item1 - 1, coord.Item2, coord.Item3),
            (coord.Item1 + 1, coord.Item2, coord.Item3),
            (coord.Item1, coord.Item2 - 1, coord.Item3),
            (coord.Item1, coord.Item2 + 1, coord.Item3),
            (coord.Item1, coord.Item2, coord.Item3 - 1),
            (coord.Item1, coord.Item2, coord.Item3 + 1)
        };
        foreach (var neighbor in neighbors)
        {
            if (!coords.Contains(neighbor))
            {
                count++;
            }
        }
    }
    Console.WriteLine(count);
}

void Part2()
{
    var count = 0;
    var maxIncrement = 15;
    foreach (var coord in coords)
    {
        count += IsExteriorSurface(coord, (-1, 0, 0), maxIncrement);
        count += IsExteriorSurface(coord, (1, 0, 0), maxIncrement);
        count += IsExteriorSurface(coord, (0, -1, 0), maxIncrement);
        count += IsExteriorSurface(coord, (0, 1, 0), maxIncrement);
        count += IsExteriorSurface(coord, (0, 0, -1), maxIncrement);
        count += IsExteriorSurface(coord, (0, 0, 1), maxIncrement);
    }
    Console.WriteLine(count);
}

int IsExteriorSurface((int, int, int) sourceCoord, (int, int, int) toAdd, int maxIncrement)
{
    var increment = 1;
    var found = false;
    while (!found && increment < maxIncrement)
    {
        if (coords.Contains((sourceCoord.Item1 + toAdd.Item1 * increment, sourceCoord.Item2 + toAdd.Item2 * increment, sourceCoord.Item3 + toAdd.Item3 * increment)))
        {
            found = true;
        }
        increment++;
    }
    return !found ? 1 : 0;
}