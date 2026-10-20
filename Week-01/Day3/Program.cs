
// Exercise 2
static void NumberAnalyzer()
{
  while (true)
  {
    Console.Write("Enter a number: ");
    string input = Console.ReadLine() ?? "";

    if (int.TryParse(input, out int number))
    {
      string sign = number > 0 ? "Positive" : number < 0 ? "Negative" : "Zero";
      string type = number % 2 == 0 ? "Even" : "Odd";

      Console.WriteLine($"Number: {number}");
      Console.WriteLine($"Sign: {sign}");
      Console.WriteLine($"Type: {type}");

      break;
    }

    Console.WriteLine("Invalid input. Please enter a valid integer.");
  }
}

static void DiscountCalculator()
{
  while (true)
  {
    Console.Write("Enter purchase amount: ");
    string input = Console.ReadLine() ?? "0";

    if (decimal.TryParse(input, out decimal amount) && amount > 0)
    {
      decimal discountRate = GetDiscount(amount);
      decimal discountAmount = amount * (discountRate / 100m);
      decimal finalAmount = amount - discountAmount;

      Console.WriteLine($"\nOriginal Amount: {amount:F2} MMK");
      Console.WriteLine($"Discount: {discountRate} %");
      Console.WriteLine($"Discount Amount: {discountAmount:F2} MMK");
      Console.WriteLine($"Final Amount: {finalAmount:F2} MMK");

      break;
    }
    Console.WriteLine("Enter valid amount!!");
  }

}

static decimal GetDiscount(decimal amount)
{
  if (amount >= 100000)
  {
    return 15;
  }
  else if (amount >= 50000)
  {
    return 10;
  }
  else if (amount >= 20000)
  {
    return 5;
  }
  else
  {
    return 0;
  }
}

// NumberAnalyzer();
// DiscountCalculator();

static void ATMTransactionSimulator()
{
  decimal balance = 100000m;

  while (true)
  {
    Console.WriteLine("====== SIMPLE ATM ======");
    Console.WriteLine("1. Check Balance");
    Console.WriteLine("2. Deposit");
    Console.WriteLine("3. Withdraw");
    Console.WriteLine("4. Exit");
    Console.Write("Choose an option: ");

    string? input = Console.ReadLine();

    if (!int.TryParse(input, out int option))
    {
      Console.WriteLine("Invalid menu option.");
      continue;
    }

    switch (option)
    {
      case 1:
        Console.WriteLine($"Current Balance: {balance:F2} MMK");
        break;

      case 2:
        decimal depositAmount = ReadPositiveAmount("\nEnter deposit amount: ");
        balance += depositAmount;
        Console.WriteLine("Deposit successful!");
        Console.WriteLine($"Current Balance: {balance:F2} MMK");
        break;

      case 3:
        while (true)
        {
          decimal withdrawalAmount = ReadPositiveAmount("\nEnter withdrawal amount: ");

          if (withdrawalAmount > balance)
          {
            Console.WriteLine("Insufficient balance.");
          }
          else
          {
            balance -= withdrawalAmount;
            Console.WriteLine("Withdrawal successful!");
            Console.WriteLine($"Remaining Balance: {balance:F2} MMK");
            break;
          }
        }
        break;

      case 4:
        Console.WriteLine("Goodbye!");
        return;

      default:
        Console.WriteLine("Invalid option.");
        break;
    }

    Console.WriteLine();
  }
}

static decimal ReadPositiveAmount(string prompt)
{
  while (true)
  {
    Console.Write(prompt);
    string? input = Console.ReadLine();

    if (input is null)
    {
      throw new EndOfStreamException("Input stream closed.");
    }

    if (decimal.TryParse(input, out decimal amount)
        && amount > 0)
    {
      return amount;
    }

    Console.WriteLine("Enter a valid positive amount.");
  }
}

ATMTransactionSimulator();