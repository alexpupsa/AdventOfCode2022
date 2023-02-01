int[,] forest;

Setup();
Part2();

void Setup()
{
    var lines = File.ReadAllLines("input.txt");
    forest = new int[lines.Length, lines[0].Length];
    var row = 0;
    foreach (var line in lines)
    {
        var col = 0;
        foreach (var treeSize in line)
        {
            forest[row, col] = treeSize;
            col++;
        }
        row++;
    }
}

void Part1()
{
    var cols = forest.GetLength(0);
    var rows = forest.GetLength(1);
    var visibleTreesCount = cols * 2 + (rows - 2) * 2;
    for (var i = 1; i < cols - 1; i++)
    {
        for (var j = 1; j < rows - 1; j++)
        {
            var isVisible = IsVisibleOnColumn(i, j, true, cols);
            if (!isVisible) isVisible = IsVisibleOnColumn(i, j, false, cols);
            if (!isVisible) isVisible = IsVisibleOnRow(i, j, true, rows);
            if (!isVisible) isVisible = IsVisibleOnRow(i, j, false, rows);
            if (isVisible) visibleTreesCount++;
        }
    }
    Console.WriteLine(visibleTreesCount);
}

void Part2()
{
    var cols = forest.GetLength(0);
    var rows = forest.GetLength(1);
    var maxScenicScore = 0;
    for (var i = 1; i < cols - 1; i++)
    {
        for (var j = 1; j < rows - 1; j++)
        {
            var countUp = CountSmallerNeighborsUp(i, j);
            var countDown = CountSmallerNeighborsDown(i, j, cols);
            var countLeft = CountSmallerNeighborsLeft(i, j);
            var countRight = CountSmallerNeighborsRight(i, j, rows);
            var scenicScore = countUp * countDown * countLeft * countRight;
            if (scenicScore > maxScenicScore) maxScenicScore = scenicScore;
        }
    }
    Console.WriteLine(maxScenicScore);
}

bool IsVisibleOnColumn(int treeRow, int treeCol, bool fromTop, int bottomEdge)
{
    for (var i = fromTop ? 0 : treeRow + 1; i < (fromTop ? treeRow : bottomEdge); i++)
    {
        if (forest[i, treeCol] >= forest[treeRow, treeCol])
        {
            return false;
        }
    }
    return true;
}

bool IsVisibleOnRow(int treeRow, int treeCol, bool fromLeft, int rightEdge)
{
    for (var i = fromLeft ? 0 : treeCol + 1; i < (fromLeft ? treeCol : rightEdge); i++)
    {
        if (forest[treeRow, i] >= forest[treeRow, treeCol])
        {
            return false;
        }
    }
    return true;
}

int CountSmallerNeighborsUp(int treeRow, int treeCol)
{
    var count = 0;
    for (var i = treeRow - 1; i >= 0; i--)
    {
        count++;
        if (forest[i, treeCol] >= forest[treeRow, treeCol])
        {
            break;
        }
    }
    return count;
}

int CountSmallerNeighborsDown(int treeRow, int treeCol, int totalRows)
{
    var count = 0;
    for (var i = treeRow + 1; i < totalRows; i++)
    {
        count++;
        if (forest[i, treeCol] >= forest[treeRow, treeCol])
        {
            break;
        }
    }
    return count;
}

int CountSmallerNeighborsLeft(int treeRow, int treeCol)
{
    var count = 0;
    for (var i = treeCol - 1; i >= 0; i--)
    {
        count++;
        if (forest[treeRow, i] >= forest[treeRow, treeCol])
        {
            break;
        }
    }
    return count;
}

int CountSmallerNeighborsRight(int treeRow, int treeCol, int totalCols)
{
    var count = 0;
    for (var i = treeCol + 1; i < totalCols; i++)
    {
        count++;
        if (forest[treeRow, i] >= forest[treeRow, treeCol])
        {
            break;
        }
    }
    return count;
}
