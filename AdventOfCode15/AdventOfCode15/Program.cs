using AdventOfCode15;

var lines = File.ReadAllLines("input.txt");

var sensorBeaconPairs = new List<SignalBeacon>();

var checkedY = 2000000;

Setup();
Part1();

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
        sensorBeaconPairs.Add(new SignalBeacon((sX, sY), (bX, bY)));
    }
}

void Part1()
{
    var noSensors = new List<(int, int)>();
    var beaconsOnCheckedLine = new List<(int, int)>();
    foreach (var pair in sensorBeaconPairs)
    {
        var startX = pair.SignalCoords.Item1 - pair.ManhattanDistance;
        var endX = pair.SignalCoords.Item1 + pair.ManhattanDistance;
        for (var i = startX; i <= endX; i++)
        {
            if (Utils.ManhattanDistance(pair.SignalCoords.Item1, pair.SignalCoords.Item2, i, checkedY) <= pair.ManhattanDistance)
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

