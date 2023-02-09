using AdventOfCode17;

var input = File.ReadAllText("input.txt");

var commands = new List<int>();
int[,] world = new int[50000, 7];
var shapes = new List<Shape>
{
    new Shape
    {
        Height = 1,
        Width = 4,
        Form = new int[1, 4] { { 1, 1, 1, 1 } }
    },
    new Shape
    {
        Height = 3,
        Width = 3,
        Form = new int[3, 3] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 1, 0 } }
    },
    new Shape
    {
        Height = 3,
        Width = 3,
        Form = new int[3, 3] { { 0, 0, 1 }, { 0, 0, 1 }, { 1, 1, 1 } }
    },
    new Shape
    {
        Height = 4,
        Width = 1,
        Form = new int[4, 1] { { 1 }, { 1 } , { 1 } , { 1 } }
    },
     new Shape
    {
        Height = 2,
        Width = 2,
        Form = new int[2, 2] { { 1, 1 }, { 1, 1 } }
    },
};

Setup();
Part1();

void Setup()
{
    foreach (var symbol in input)
    {
        commands.Add(symbol == '>' ? 1 : -1);
    }
}

void Part1()
{
    var maxPieces = 2022;
    var piece = 0;
    var shapeIndex = 0;
    var commandIndex = 0;
    var lastEmptyRow = world.GetLength(0) - 1;
    int x;
    int y;
    while (piece < maxPieces)
    {
        var shape = shapes[shapeIndex % shapes.Count];
        shapeIndex++;
        var row = HeighestRockRow(world, lastEmptyRow);
        lastEmptyRow = row - 1;
        x = row - 4 - shape.Height;
        y = 2;
        var isRowBelowEmpty = true;
        while (isRowBelowEmpty)
        {
            x++;
            var command = commands[commandIndex % commands.Count];
            commandIndex++;
            if (command == 1 && y + shape.Width < 7 && CanShapeMoveRight(world, shape, x, y))
            {
                y++;
            }
            else if (command == -1 && y > 0 && CanShapeMoveLeft(world, shape, x, y))
            {
                y--;
            }
            if (x + shape.Height >= world.GetLength(0))
            {
                isRowBelowEmpty = false;
            }
            else
            {
                isRowBelowEmpty = IsRowEmpty(world, x + shape.Height, y, y + shape.Width);
            }
            if (!isRowBelowEmpty)
            {
                for (var i = x; i < x + shape.Height; i++)
                {
                    for (var j = y; j < y + shape.Width; j++)
                    {
                        world[i, j] = shape.Form[i - x, j - y];
                    }
                }
            }
        }
        piece++;
        //PrintWorld(world, x);
        //Console.ReadKey();
    }
    var heighestRockRow = HeighestRockRow(world, world.GetLength(0) - 1);
    Console.WriteLine(50000 - heighestRockRow);
}

static int HeighestRockRow(int[,] w, int lastEmptyRow)
{
    for (var i = lastEmptyRow; i >= 0; i--)
    {
        var isRowEmpty = true;
        for (var j = 0; j < w.GetLength(1); j++)
        {
            if (w[i, j] == 1)
            {
                isRowEmpty = false;
                break;
            }
        }
        if (isRowEmpty)
        {
            return i + 1;
        }
    }
    return -1;
}

static bool IsRowEmpty(int[,] w, int row, int startY, int endY)
{
    var isEmpty = true;
    for (var j = startY; j < endY; j++)
    {
        if (w[row, j] == 1)
        {
            isEmpty = false;
            break;
        }
    }
    return isEmpty;
}

static void PrintWorld(int[,] w, int row)
{
    Console.Clear();
    for (var i = row; i < w.GetLength(0); i++)
    {
        for (var j = 0; j < w.GetLength(1); j++)
        {
            Console.Write(w[i, j] == 1 ? '#' : '.');
        }
        Console.WriteLine();
    }
}

static bool CanShapeMoveLeft(int[,] w, Shape s, int row, int col)
{
    var canMove = true;
    for (var i = 0; i < s.Height; i++)
    {
        for (var j = 0; j < s.Width; j++)
        {
            if (s.Form[i, j] == 1)
            {
                if (w[row + i, col + j - 1] == 1)
                {
                    return false;
                }
                break;
            }
        }
    }
    return canMove;
}

static bool CanShapeMoveRight(int[,] w, Shape s, int row, int col)
{
    var canMove = true;
    for (var i = 0; i < s.Height; i++)
    {
        for (var j = s.Width - 1; j >= 0; j--)
        {
            if (s.Form[i, j] == 1)
            {
                if (w[row + i, col + j + 1] == 1)
                {
                    return false;
                }
                break;
            }
        }
    }
    return canMove;
}