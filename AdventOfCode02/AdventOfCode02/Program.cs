var lines = File.ReadAllLines("input.txt");
var totalScore = 0;
foreach (var line in lines)
{
    var myMove = 0;
    var oppMove = line[0] - 64;
    var outcome = line[2] - 87;
    switch (outcome)
    {
        case 1:
            myMove = oppMove - 1;
            if (myMove == 0) myMove = 3;
            totalScore += myMove;
            break;
        case 2: 
            myMove = oppMove;
            totalScore += myMove + 3;
            break;
        case 3: 
            myMove = oppMove + 1;
            if (myMove == 4) myMove = 1;
            totalScore += myMove + 6;
            break;
    }
}
Console.WriteLine(totalScore);