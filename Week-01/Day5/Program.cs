
List<int> scores = new();

while (true)
{
  ShowMenu();

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
      scores.Add(ReadScore());
      break;

    case 2:
      ViewScores(scores);
      break;

    case 3:
      {
        if (scores.Count == 0)
        {
          Console.WriteLine("\nNo scores available.");
          break;
        }

        int total = 0;
        foreach (int score in scores)
        {
          total += score;
        }

        Console.WriteLine("\n===== SCORE STATISTICS V4 =====");
        Console.WriteLine($"Total Score: {total}");
        Console.WriteLine($"Average Score: {CalculateAverage(scores):F2}");
        Console.WriteLine($"Highest Score: {FindHighestScore(scores)}");
        Console.WriteLine($"Lowest Score: {FindLowestScore(scores)}");
        Console.WriteLine($"Pass Count: {CountPassingScores(scores)}");
        Console.WriteLine($"Fail Count: {CountFailingScores(scores)}");
        break;
      }

    case 4:
      RemoveScore(scores);
      break;

    case 5:
      {
        if (scores.Count == 0)
        {
          Console.WriteLine("\nNo scores available.");
          break;
        }

        ViewScores(scores);

        while (true)
        {
          Console.Write("\nEnter score number: ");
          string? scoreInput = Console.ReadLine();

          if (scoreInput is null)
          {
            throw new EndOfStreamException("Input stream closed.");
          }

          if (int.TryParse(scoreInput, out int scoreNumber)
              && scoreNumber > 0 && scoreNumber <= scores.Count)
          {
            Console.WriteLine($"Score {scores[scoreNumber - 1]}");
            Console.WriteLine($"Grade: {GetGrade(scores[scoreNumber - 1])}");
            break;
          }
          Console.WriteLine($"Enter a valid score number(1-{scores.Count}).");
        }
        break;
      }

    case 6:
      Console.WriteLine("Goodbye!");
      return;

    default:
      Console.WriteLine("Invalid option.");
      break;
  }
}

static void ShowMenu()
{
  Console.WriteLine("\n===== STUDENT SCORE MANAGER V4 =====");
  Console.WriteLine("1. Add Score");
  Console.WriteLine("2. View All Scores");
  Console.WriteLine("3. Calculate Statistics");
  Console.WriteLine("4. Remove Score");
  Console.WriteLine("5. Check Grade");
  Console.WriteLine("6. Exit");
  Console.Write("Choose an option: ");
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

static double CalculateAverage(List<int> scores)
{
  if (scores.Count == 0)
  {
    Console.WriteLine("\nNo scores available.");
    return 0;
  }

  long total = 0;
  foreach (int score in scores)
  {
    total += score;
  }
  return (double)total / scores.Count;
}

static int FindHighestScore(List<int> scores)
{
  int maxScore = scores[0];
  foreach (int score in scores)
  {
    maxScore = score >= maxScore ? score : maxScore;
  }
  return maxScore;
}

static int FindLowestScore(List<int> scores)
{
  int minScore = scores[0];
  foreach (int score in scores)
  {
    minScore = score <= minScore ? score : minScore;
  }
  return minScore;
}

static int CountPassingScores(List<int> scores)
{
  int passCount = 0;
  foreach (int score in scores)
  {
    if (score >= 50)
    {
      passCount++;
    }
  }
  return passCount;
}

static int CountFailingScores(List<int> scores)
{
  int failCount = 0;
  foreach (int score in scores)
  {
    if (score < 50)
    {
      failCount++;
    }
  }
  return failCount;
}

static string GetGrade(int score)
{
  if (score <= 0 || score > 100)
  {
    return "Invalid Score";
  }
  
  if (score >= 80) return "A";
  if (score >= 70) return "B";
  if (score >= 60) return "C";
  if (score >= 50) return "D";
  return "F";
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