using AdventOfCode07;

var lines = File.ReadAllLines("input.txt");
var root = new Node("/", NodeType.Folder);

Setup();
Part2();

void Setup()
{
    var currentNode = root;

    foreach (var line in lines)
    {
        if (line.StartsWith("$ cd"))
        {
            var folderName = line[5..];
            if (folderName == "..")
            {
                currentNode = currentNode?.Parent;
            }
            else if (folderName == "/")
            {
                currentNode = root;
            }
            else
            {
                var folder = currentNode?.Children.FirstOrDefault(x => x.Name == folderName && x.NodeType == NodeType.Folder);
                if (folder != null)
                {
                    currentNode = folder;
                }
            }
        }
        else if (!line.StartsWith("$"))
        {
            if (line.StartsWith("dir"))
            {
                var folderName = line[4..];
                AddFolder(folderName, ref currentNode);
            }
            else
            {
                var splits = line.Split(' ');
                var size = int.Parse(splits[0]);
                var fileName = splits[1];
                AddFile(fileName, size, ref currentNode);
            }
        }
    }
}

void Part1()
{
    var folderSizes = new List<int>();
    LookForFolders(root, ref folderSizes);
    var finalSum = folderSizes.Where(x => x <= 100000).Sum();
    Console.WriteLine(finalSum);
}

void Part2()
{
    var folderSizes = new List<int>();
    LookForFolders(root, ref folderSizes);
    var freeSpace = 70000000 - folderSizes.First();
    var spaceNeeded = 30000000 - freeSpace;
    var smallestFolderToDelete = folderSizes.Where(x => x >= spaceNeeded).Min();
    Console.WriteLine(smallestFolderToDelete);
}

void AddFolder(string folderName, ref Node? currentNode)
{
    var folder = new Node(folderName, NodeType.Folder, currentNode);
    currentNode?.Children.Add(folder);
}

void AddFile(string fileName, int size, ref Node? currentNode)
{
    var file = new Node(fileName, NodeType.File, currentNode);
    file.Size = size;
    currentNode?.Children.Add(file);
}

void LookForFolders(Node node, ref List<int> folderSizes)
{
    var size = node.GetSize();
    folderSizes.Add(size);
    foreach (var child in node.Children.Where(x => x.NodeType == NodeType.Folder))
    {
        LookForFolders((Node)child, ref folderSizes);
    }
}