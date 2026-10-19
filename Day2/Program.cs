Console.Write("Enter your name: ");
string? nameInput = Console.ReadLine();

string name = string.IsNullOrWhiteSpace(nameInput)
    ? "Unknown"
    : nameInput.Trim();

int mathScore = ReadScore("Math");
int englishScore = ReadScore("English");
int scienceScore = ReadScore("Science");

double avgScore = CalculateAverage(mathScore, englishScore, scienceScore);
string result = avgScore >= 50 ? "Pass" : "Fail";
string grade = CalculateGrade(avgScore);

Console.WriteLine("===== STUDENT RESULT =====");
Console.WriteLine($"Name: {name}");
Console.WriteLine($"Math: {mathScore}");
Console.WriteLine($"English: {englishScore}");
Console.WriteLine($"Science: {scienceScore}");
Console.WriteLine($"Average: {avgScore:F2}");
Console.WriteLine($"Result: {result}");
Console.WriteLine($"You got Grade - {grade}");
Console.WriteLine("==========================");

static string CalculateGrade(double average)
{
  if (average >= 80)
  {
    return "A";
  }
  else if (average >= 70)
  {
    return "B";
  }
  else if (average >= 60)
  {
    return "C";
  }
  else if (average >= 50)
  {
    return "D";
  }
  else
  {
    return "F";
  }
}

static double CalculateAverage(int math, int english, int science)
{
  return (double)(math + english + science) / 3;
}

static int ReadScore(string subject)
{
  while (true)
  {
    Console.Write($"Enter {subject} score (0-100): ");
    string score = Console.ReadLine() ?? "0";

    if (int.TryParse(score, out int validScore) && validScore >= 0 && validScore <= 100)
    {
      return validScore;
    }

    Console.WriteLine($"Invalid score. Please enter a value between 0 and 100!!!");
  }
}