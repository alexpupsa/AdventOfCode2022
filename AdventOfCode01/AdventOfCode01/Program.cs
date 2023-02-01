using AdventOfCode01;
var currentCalories = 0;
var lines = File.ReadAllLines("input.txt");
var elves = new List<Elf>();
foreach (var line in lines)
{
    if (string.IsNullOrEmpty(line))
    {
        elves.Add(new Elf { Calories = currentCalories });
        currentCalories = 0;
    }
    else
    {
        currentCalories += int.Parse(line);
    }
}
var top3CaloriesTotal = elves.OrderByDescending(x => x.Calories).Take(3).Sum(x => x.Calories);
Console.WriteLine(top3CaloriesTotal);