string[] readFileLines(string fileName)
{
    return File.ReadAllText(fileName).Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
}

(List<long>, List<long>) getNumbers(string fileName)
{
    var input = readFileLines(fileName);

    var list1 = new List<long>();
    var list2 = new List<long>();

    foreach (var line in input)
    {
        var numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(n => long.Parse(n)).ToList();

        list1.Add(numbers[0]);
        list2.Add(numbers[1]);
    }

    list1 = list1.Order().ToList();
    list2 = list2.Order().ToList();

    return (list1, list2);
}

long partOne(List<long> list1, List<long> list2)
{
    long differenceScore = 0;

    var differences = list1.Zip(list2, (first, second) => Math.Abs(first - second));

    foreach (var difference in differences)
    {
        differenceScore += difference;
    }

    return differenceScore;
}

long partTwo(List<long> list1, List<long> list2)
{
    long similarityScore = 0;

    var matches = new Dictionary<long, long>();

    foreach (var num in list1)
    {
        long numMatches;

        if (!matches.TryGetValue(num, out numMatches))
        {
            numMatches = list2.FindAll(n => n == num).ToList().Count();
            matches.Add(num, numMatches);
        }

        similarityScore += num * numMatches;
    }

    return similarityScore;
}

var fileName = "sample.txt";
var (list1, list2) = getNumbers(fileName);

var partOneSolution = partOne(list1, list2);
var partTwoSolution = partTwo(list1, list2);

Console.WriteLine("Part One: " + partOneSolution);
Console.WriteLine("Part Two: " + partTwoSolution);
