
while (true)
{
  Console.Write("Enter your age: ");
  string ageInput = Console.ReadLine() ?? "";

  if (int.TryParse(ageInput, out int age) && age > 0)
  {
    Console.WriteLine($"Age Accepted: {age}");
    break;
  }

  Console.WriteLine("Invalid age. Try Again.");
}