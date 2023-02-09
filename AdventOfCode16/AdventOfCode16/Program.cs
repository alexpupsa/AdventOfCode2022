using AdventOfCode16;

var lines = File.ReadAllLines("input.txt");

Dictionary<string, Valve> valvesLookup = new Dictionary<string, Valve>();
var totalMinutes = 30;
var maxRate = 0;
var totalValvesWithFlow = 0;

Setup();
Part1();

void Setup()
{
    foreach (var line in lines)
    {
        var valveName = line.Substring(6, 2);
        var flowRate = int.Parse(line.Split('=', ';')[1]);
        var splits = line.Split("to valves ");
        if (splits.Length == 1)
        {
            splits = line.Split("to valve ");
        }
        var connectedValves = splits[1].Split(", ");
        var valve = new Valve(valveName, flowRate, connectedValves);
        valvesLookup.Add(valveName, valve);
        if (valve.FlowRate > 0)
        {
            totalValvesWithFlow++;
        }
    }
}

void Part1()
{
    var queue = new Queue<QueueItem>();
    queue.Enqueue(new QueueItem
    {
        Minutes = 0,
        OpenValves = new List<string>(),
        ValveName = "AA"
    });
    while (queue.Count > 0)
    {
        var item = queue.Dequeue();
        if (item.Minutes > 30 || item.OpenValves.Count == totalValvesWithFlow)
        {
            if (item.CurrentRate > maxRate)
            {
                maxRate = item.CurrentRate;
                Console.WriteLine(maxRate);
            }
            continue;
        }
        var valve = valvesLookup[item.ValveName];
        if (valve.FlowRate > 0 && !item.OpenValves.Contains(valve.Name))
        {
            var openValves = item.OpenValves.ToList();
            openValves.Add(valve.Name);
            var newItem = new QueueItem
            {
                Minutes = item.Minutes + 1,
                OpenValves = openValves,
                ValveName = valve.Name,
                CurrentRate = item.CurrentRate + (totalMinutes - item.Minutes) * valve.FlowRate
            };
            queue.Enqueue(newItem);
        }
        else
        {
            foreach (var connectedValve in valve.ConnectedValves)
            {
                var newItem = new QueueItem
                {
                    Minutes = item.Minutes + 1,
                    OpenValves = item.OpenValves.ToList(),
                    ValveName = connectedValve,
                    CurrentRate = item.CurrentRate
                };
                queue.Enqueue(newItem);
            }
        }
    }
    Console.WriteLine(maxRate);
}

void UseValve(Valve valve, int minutes, List<string> openValves)
{
    if (valve.FlowRate > 0)
    {
        if (!openValves.Contains(valve.Name))
        {
            openValves.Add(valve.Name);
            UseValve(valve, minutes + 1, openValves);
        }
    }

}
