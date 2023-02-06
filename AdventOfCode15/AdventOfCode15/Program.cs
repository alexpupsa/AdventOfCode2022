using AdventOfCode15;

var lines = File.ReadAllLines("input.txt");

var sensorBeaconPairs = new List<SensorBeacon>();

var checkedY = 2000000;
var MAX_SIZE = 4000000;

Setup();
Part2();

void Setup()
{
    foreach (var line in lines)
    {
        var splits = line.Split(",");
        var sX = int.Parse(splits[0].Substring(splits[0].LastIndexOf('=') + 1));
        var indexOfEquals = splits[1].IndexOf('=');
        var indexOfSemiColumn = splits[1].IndexOf(":");
        var sY = int.Parse(splits[1].Substring(splits[1].IndexOf('=') + 1, indexOfSemiColumn - indexOfEquals - 1));
        var bX = int.Parse(splits[1].Substring(splits[1].LastIndexOf('=') + 1));
        var bY = int.Parse(splits[2].Substring(splits[2].LastIndexOf('=') + 1));
        sensorBeaconPairs.Add(new SensorBeacon((sX, sY), (bX, bY)));
    }
}

void Part1()
{
    var noSensors = new List<(int, int)>();
    var beaconsOnCheckedLine = new List<(int, int)>();
    foreach (var pair in sensorBeaconPairs)
    {
        var startX = pair.SensorCoords.Item1 - pair.ManhattanDistance;
        var endX = pair.SensorCoords.Item1 + pair.ManhattanDistance;
        for (var i = startX; i <= endX; i++)
        {
            if (Utils.ManhattanDistance(pair.SensorCoords.Item1, pair.SensorCoords.Item2, i, checkedY) <= pair.ManhattanDistance)
            {
                noSensors.Add((i, checkedY));
            }
        }
        if (pair.BeaconCoords.Item2 == checkedY)
        {
            beaconsOnCheckedLine.Add(pair.BeaconCoords);
        }
    }
    noSensors = noSensors.Distinct().Where(x => !beaconsOnCheckedLine.Contains(x)).ToList();
    Console.WriteLine(noSensors.Count);
}

void Part2()
{
    var noSensors = new List<(int, int)>();
    var beaconsOnCheckedLine = new List<(int, int)>();
    var foundX = 0;
    var foundY = 0;

    var pairCount = 0;
    foreach (var pair in sensorBeaconPairs)
    {
        var startX = pair.SensorCoords.Item1 - pair.ManhattanDistance;
        var endX = pair.SensorCoords.Item1 + pair.ManhattanDistance;
        var startY = pair.SensorCoords.Item2 - pair.ManhattanDistance;
        var endY = pair.SensorCoords.Item2 + pair.ManhattanDistance;
        var step = 1;
        var done = false;
        for (var i = startX; i <= (endX + 1) / 2 && !done; i++, step++)
        {
            for (var j = startY; j <= (endY + 1) / 2 - step; j++)
            {
                var found = true;
                foreach (var otherPair in sensorBeaconPairs.Where(x => x != pair))
                {
                    if (Utils.ManhattanDistance(otherPair.SensorCoords.Item1, otherPair.SensorCoords.Item2, i, j) <= otherPair.ManhattanDistance)
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    foundX = i;
                    foundY = j;
                    done = true;
                    break;
                }
            }
        }
        if (done)
        {
            break;
        }
        pairCount++;
        Console.WriteLine(pairCount);
    }

    Console.WriteLine(foundX);
    Console.WriteLine(foundY);
    Console.WriteLine(foundX * MAX_SIZE + foundY);
}