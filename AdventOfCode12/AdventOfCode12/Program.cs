using AdventOfCode12;

Node[,] map;

var startX = 0;
var startY = 0;
var bottomEdge = 0;
var rightEdge = 0;

var visitedNodes = new List<Node>();

var maxQueueCount = 0;

Setup();
Part1();

void Setup()
{
    var lines = File.ReadAllLines("input.txt");
    var rows = lines.Length;
    var cols = lines[0].Length;
    bottomEdge = rows - 1;
    rightEdge = cols - 1;
    map = new Node[rows, cols];
    var rowIndex = 0;
    foreach (var line in lines)
    {
        var colIndex = 0;
        foreach (var c in line)
        {
            var height = c;
            var isStart = false;
            var isEnd = false;
            if (c == 'E')
            {
                height = 'z';
                isEnd = true;
            }
            else if (c == 'S')
            {
                height = 'a';
                isStart = true;
                startX = rowIndex;
                startY = colIndex;
            }
            map[rowIndex, colIndex] = new Node
            {
                X = rowIndex,
                Y = colIndex,
                Height = height,
                IsStart = isStart,
                IsEnd = isEnd
            };
            colIndex++;
        }
        rowIndex++;
    }
}

void Part1()
{
    var startNode = map[startX, startY];
    var path = FindPath(startNode);
    Console.WriteLine($"Path size = {path.Count - 1}");
    Console.WriteLine($"Max queue count = {maxQueueCount}");
}

void Part2()
{
    var aLevelNodes = new List<Node>();
    for (var i = 0; i < map.GetLength(0); i++)
    {
        for (var j = 0; j < map.GetLength(1); j++)
        {
            if (map[i, j].Height == 'a')
            {
                aLevelNodes.Add(map[i, j]);
            }
        }
        Console.WriteLine();
    }

    var minPathLength = 10000;
    foreach (var aLevelNode in aLevelNodes)
    {
        visitedNodes.Clear();
        var startNode = map[aLevelNode.X, aLevelNode.Y];
        var path = FindPath(startNode);
        if (path.Any() && path.Count - 1 < minPathLength)
        {
            minPathLength = path.Count - 1;
        }
    }

    Console.WriteLine($"Path size = {minPathLength}");
}

List<Node>? FindPath(Node startNode)
{
    var queue = new Queue<List<Node>>();
    visitedNodes.Add(startNode);
    queue.Enqueue(new List<Node> { startNode });

    while (queue.Any())
    {
        if(queue.Count > maxQueueCount)
        {
            maxQueueCount = queue.Count;
        }
        //PrintMap(map);
        var path = queue.Dequeue();
        var current = path.Last();
        if (current.IsEnd)
        {
            return path;
        }

        var neighbours = new List<Node>();
        if (current.X < bottomEdge)
        {
            neighbours.Add(map[current.X + 1, current.Y]);
        }
        if (current.X > 0)
        {
            neighbours.Add(map[current.X - 1, current.Y]);
        }
        if (current.Y > 0)
        {
            neighbours.Add(map[current.X, current.Y - 1]);
        }
        if (current.Y < rightEdge)
        {
            neighbours.Add(map[current.X, current.Y + 1]);
        }
        neighbours = ChooseValidNeighbor(current.Height, neighbours);
        foreach (var neighbor in neighbours)
        {
            if (!visitedNodes.Contains(neighbor))
            {
                var newPath = path.ToList();
                newPath.Add(neighbor);
                visitedNodes.Add(neighbor);
                queue.Enqueue(newPath);
            }
        }
    }

    return new List<Node>();
}

List<Node> ChooseValidNeighbor(int currentHeight, List<Node> neighbours)
{
    return neighbours.Where(x => IsNodeReachable(currentHeight, x.Height)).ToList();
}

static bool IsNodeReachable(int currentHeight, int nodeHeight)
{
    return nodeHeight - currentHeight <= 1;
}

void PrintMap(Node[,] map)
{
    Console.Clear();
    for (var i = 0; i < map.GetLength(0); i++)
    {
        for (var j = 0; j < map.GetLength(1); j++)
        {
            Console.Write(visitedNodes.Contains(map[i, j]) ? "v" : ".");
        }
        Console.WriteLine();
    }
}