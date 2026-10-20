
List<int> scores = new();

while (true)
{
  Console.WriteLine("\n===== STUDENT SCORE MANAGER =====");
  Console.WriteLine("1. Add Score");
  Console.WriteLine("2. View All Scores");
  Console.WriteLine("3. Calculate Statistics");
  Console.WriteLine("4. Remove Score");
  Console.WriteLine("5. Exit");
  Console.Write("Choose an option: ");

  string? input = Console.ReadLine();

  if (input is null)
  {
    Console.WriteLine("Input closed.");
    return;
  }

  if (!int.TryParse(input, out int option))
  {
    Console.WriteLine("Invalid option.");
    continue;
  }

  switch (option)
  {
    case 1:
      int score = ReadScore();
      scores.Add(score);
      break;

    case 2:
      ViewScores(scores);
      break;

    case 3:
      CalculateStatistics(scores);
      break;

    case 4:
      RemoveScore(scores);
      break;

    case 5:
      Console.WriteLine("Goodbye!");
      return;

    default:
      Console.WriteLine("Invalid option.");
      break;
  }
}

static int ReadScore()
{
  while (true)
  {
    Console.Write("Enter Score: ");
    string? input = Console.ReadLine();

    if (input is null)
    {
      throw new EndOfStreamException("Input stream closed.");
    }

    if (int.TryParse(input, out int score)
        && score >= 0 && score <= 100)
    {
      return score;
    }

    Console.WriteLine("Enter a valid score (0-100).");
  }
}

static void ViewScores(List<int> scores)
{
  if (scores.Count == 0)
  {
    Console.WriteLine("\nNo scores available.");
    return;
  }

  for (int i = 0; i < scores.Count; i++)
  {
    Console.WriteLine($"{i + 1}. Score: {scores[i]}");
  }
}

static void CalculateStatistics(List<int> scores)
{
  if (scores.Count == 0)
  {
    Console.WriteLine("\nNo scores available.");
    return;
  }

  long total = 0;
  int max = scores[0];
  int min = scores[0];
  int passCount = 0;
  int failCount = 0;
  double average;

  foreach (int score in scores)
  {
    total += score;
    max = score >= max ? score : max;
    min = score <= min ? score : min;
    if (score >= 50)
    {
      passCount++;
    }
    else
    {
      failCount++;
    }
  }
  average = (double)total / scores.Count;

  Console.WriteLine($"\nTotal Score: {total}");
  Console.WriteLine($"Average Score: {average:F2}");
  Console.WriteLine($"Highest Score: {max}");
  Console.WriteLine($"Lowest Score: {min}");
  Console.WriteLine($"Pass Count: {passCount}");
  Console.WriteLine($"Fail Count: {failCount}");
}

static void RemoveScore(List<int> scores)
{
  if (scores.Count == 0)
  {
    Console.WriteLine("\nNo scores available.");
    return;
  }

  ViewScores(scores);

  while (true)
  {
    Console.Write("\nEnter score number to remove: ");
    string? input = Console.ReadLine();

    if (input is null)
    {
      throw new EndOfStreamException("Input stream closed.");
    }

    if (int.TryParse(input, out int scoreNumber)
        && scoreNumber > 0 && scoreNumber <= scores.Count)
    {
      scores.RemoveAt(scoreNumber - 1);
      Console.WriteLine("Score removed successfully!");
      break;
    }

    Console.WriteLine($"Enter a valid number. (1-{scores.Count}).");
  }
}