using AdventOfCode15;

var lines = File.ReadAllLines("input.txt");

var sensorBeaconPairs = new List<SignalBeacon>();

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
    Console.WriteLine(sensorBeaconPairs.Count);
}

