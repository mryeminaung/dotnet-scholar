# Week 1 — Day 3: Operators, Conditional Statements & Logical Thinking

> **Course:** .NET Full-Stack Developer Roadmap  
> **Level:** Beginner | **Duration:** ~2 hours  
> **Language:** မြန်မာ + English Technical Terms  
> **Status:** Completed — ATM Simulator v2 reviewed

## Learning Objectives

- Arithmetic, assignment, comparison, and logical operators အသုံးပြုနိုင်ရန်
- `if` / `else if` / `else`, `switch` statement, `switch` expression တို့ကို နားလည်ရန်
- Short-circuit evaluation နှင့် guard conditions အသုံးပြုရန်
- Boundary cases နှင့် input validation ကို စဉ်းစားနိုင်ရန်
- Console-based ATM simulator ကို implementation နှင့် refactoring လုပ်နိုင်ရန်

## 1. Arithmetic Operators

| Operator | Meaning | Example | Result |
|---|---|---|---|
| `+` | Addition | `10 + 3` | `13` |
| `-` | Subtraction | `10 - 3` | `7` |
| `*` | Multiplication | `10 * 3` | `30` |
| `/` | Division | `10 / 3` | `3` (`int` division) |
| `%` | Remainder | `10 % 3` | `1` |

```csharp
int a = 10;
int b = 3;

Console.WriteLine(a / b);         // 3
Console.WriteLine((double)a / b); // 3.333333...
Console.WriteLine(a % b);         // 1
```

**သတိပြုရန်:** `%` သည် Percentage မဟုတ်ဘဲ **Remainder Operator** ဖြစ်သည်။

```csharp
int number = 8;
Console.WriteLine(number % 2 == 0 ? "Even" : "Odd");
```

### Compound Assignment and Increment

```csharp
int balance = 1000;
balance += 500; // 1500
balance -= 200; // 1300

int x = 5;
int first = x++; // first = 5; x = 6
int second = ++x; // second = 7; x = 7
```

- `x++` (postfix) — လက်ရှိ Value ကို Expression Result အဖြစ် အသုံးပြုပြီးမှ တိုးသည်။
- `++x` (prefix) — အရင်တိုးပြီးမှ Expression Result အဖြစ် အသုံးပြုသည်။

## 2. Comparison Operators

Comparison Result သည် `bool` (`true` / `false`) ဖြစ်သည်။

| Operator | Meaning | Example |
|---|---|---|
| `==` | Equal | `score == 50` |
| `!=` | Not equal | `score != 0` |
| `>` | Greater than | `score > 80` |
| `<` | Less than | `score < 50` |
| `>=` | Greater than or equal | `score >= 50` |
| `<=` | Less than or equal | `score <= 100` |

`=` သည် **assignment** ဖြစ်ပြီး `==` သည် **equality comparison** ဖြစ်သည်။

## 3. Logical Operators

| Operator | Meaning | Rule |
|---|---|---|
| `&&` | AND | နှစ်ခုလုံး `true` ဖြစ်မှ `true` |
| `||` | OR | အနည်းဆုံးတစ်ခု `true` ဖြစ်ရင် `true` |
| `!` | NOT | `true`/`false` ကို ပြောင်းပြန်လုပ်သည် |

### Truth Table

| A | B | A `&&` B | A `\|\|` B |
|---|---|---|---|
| false | false | false | false |
| false | true | false | true |
| true | false | false | true |
| true | true | true | true |

```csharp
string? input = Console.ReadLine();

if (int.TryParse(input, out int score)
    && score >= 0
    && score <= 100)
{
    Console.WriteLine("Valid score");
}
```

**Short-circuit evaluation:** `&&` ၏ ဘယ်ဘက်က `false` ဖြစ်လျှင် ညာဘက်ကို မစစ်တော့ပါ။ `||` ၏ ဘယ်ဘက်က `true` ဖြစ်လျှင်လည်း ညာဘက်ကို မစစ်တော့ပါ။

```csharp
string? name = Console.ReadLine();
if (name != null && name.Length > 0)
{
    Console.WriteLine($"Hello, {name}");
}
```

## 4. Conditional Statements

### `if / else`

```csharp
int score = 45;

if (score >= 50)
{
    Console.WriteLine("Pass");
}
else
{
    Console.WriteLine("Fail");
}
```

### `else if` and Condition Order

```csharp
static string CalculateGrade(double average)
{
    if (average >= 80) return "A";
    else if (average >= 70) return "B";
    else if (average >= 60) return "C";
    else if (average >= 50) return "D";
    return "F";
}
```

**အရေးကြီး:** Threshold များကို အမြင့်ဆုံးကနေ အနိမ့်ဆုံးသို့ စစ်ပါ။ `average >= 50` ကို အရင်စစ်လျှင် `85` အတွက်တောင် `D` ဖြစ်သွားနိုင်သည်။

### Nested Conditions vs Guard Clauses

```csharp
static void CheckWithdrawal(decimal balance, decimal amount)
{
    if (amount <= 0)
    {
        Console.WriteLine("Invalid amount.");
        return;
    }

    if (amount > balance)
    {
        Console.WriteLine("Insufficient balance.");
        return;
    }

    Console.WriteLine("Withdrawal allowed.");
}
```

Guard Clauses များက Invalid Cases ကို အစောပိုင်း Handle လုပ်ပြီး Nesting ကို လျှော့နိုင်သည်။

## 5. `switch` Statement and Expression

```csharp
int option = 2;

switch (option)
{
    case 1:
        Console.WriteLine("Check Balance");
        break;
    case 2:
        Console.WriteLine("Deposit");
        break;
    case 3:
        Console.WriteLine("Withdraw");
        break;
    default:
        Console.WriteLine("Invalid option");
        break;
}
```

```csharp
string action = option switch
{
    1 => "Check Balance",
    2 => "Deposit",
    3 => "Withdraw",
    _ => "Invalid option"
};
```

- `break`: အနီးဆုံး Loop သို့မဟုတ် `switch` ကနေ ထွက်သည်။
- `continue`: လက်ရှိ Loop Iteration ကို Skip လုပ်သည်။
- `return`: Method တစ်ခုလုံးမှ ထွက်သည်။

## 6. Logical Thinking: ATM Withdrawal

**Requirements**

1. Withdrawal amount သည် `0` ထက် ကြီးရမည်။
2. Withdrawal amount သည် balance ထက် **မကြီးရ**။
3. Valid ဖြစ်မှ balance မှ နုတ်မည်။
4. Invalid ဖြစ်လျှင် သက်ဆိုင်ရာ Error Message ပြမည်။

```text
READ amount
IF amount <= 0
    SHOW "Invalid amount"
ELSE IF amount > balance
    SHOW "Insufficient balance"
ELSE
    balance = balance - amount
    SHOW "Withdrawal successful"
END IF
```

### Boundary Tests

| Balance | Withdrawal | Expected |
|---:|---:|---|
| 100000 | 30000 | Success; remaining 70000 |
| 100000 | 100000 | Success; remaining 0 |
| 100000 | 100001 | Insufficient balance |
| 100000 | 0 | Invalid amount |
| 100000 | -50 | Invalid amount |

**v1 Code Review မှာ တွေ့ခဲ့သော Bug:** `withdrawalAmount < balance` ဟုရေးထားလျှင် balance နှင့် amount တူသည့် Valid Case ကို Reject လုပ်မိသည်။ `withdrawalAmount <= balance` သို့မဟုတ် `withdrawalAmount > balance` ကို Reject လုပ်သည့် Logic သုံးနိုင်သည်။

## 7. Practical Exercises

### Exercise 1 — Predict the Output

```csharp
int x = 10;
int y = 3;

Console.WriteLine(x / y);
Console.WriteLine(x % y);
Console.WriteLine(x > y && y > 5);
```

**Answer:** `3`, `1`, `False`.

### Exercise 2 — Number Analyzer

Integer Input တစ်ခုကို `int.TryParse()` ဖြင့် Validate လုပ်ပြီး Positive/Negative/Zero နှင့် Even/Odd ကို ပြပါ။

### Exercise 3 — Discount Calculator

| Purchase Amount (MMK) | Discount |
|---:|---:|
| `>= 100000` | 15% |
| `>= 50000` | 10% |
| `>= 20000` | 5% |
| `< 20000` | 0% |

`decimal` ဖြင့် Discount Amount နှင့် Final Amount ကို တွက်ပါ။

## 8. Homework — Simple ATM Transaction Simulator

**Initial Balance:** `100000m` MMK  
**Menu:** Check Balance, Deposit, Withdraw, Exit

**Requirements:**

- `while (true)` နှင့် `switch` ဖြင့် Menu Loop ရေးရန်
- Deposit/Withdraw Amount များကို `decimal.TryParse()` ဖြင့် Validate လုပ်ရန်
- Amount သည် Positive ဖြစ်ရမည်
- Withdrawal Amount သည် Balance ထက် မကြီးရ
- Transaction ပြီးလျှင် Updated Balance ပြရန်
- Invalid Input များကို Handle လုပ်ရန်

### Final Submitted v2 Code

အောက်ပါ Code သည် Day 3 Homework အတွက် သင်တန်းသားတင်သွင်းခဲ့သော **v2 Implementation** ဖြစ်သည်။ Review အရ **9/10 — PASSED** ဖြစ်ပြီး နောက်ပိုင်း Refinements များ ရှိသေးသည်။

```csharp
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
                while (true)
                {
                    decimal depositAmount = ReadPositiveAmount("\nEnter deposit amount: ");
                    balance += depositAmount;
                    Console.WriteLine("Deposit successful!");
                    Console.WriteLine($"Current Balance: {balance:F2} MMK");
                    break;
                }
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
```

### Code Review Notes

**Strengths**

- `decimal` သုံးထားခြင်း
- Main Menu, Deposit/Withdrawal Flow များကို `switch` နှင့် Loops ဖြင့် စနစ်တကျ ခွဲထားခြင်း
- `ReadPositiveAmount()` ကို Reusable Method အဖြစ် Extract လုပ်ထားခြင်း (DRY)
- Withdrawal Equal-to-Balance Boundary ကို ပြင်ထားခြင်း
- Insufficient Balance အတွက် သီးခြား Message ပြခြင်း

**Optional Improvements**

1. Deposit Case ၏ Outer `while (true)` သည် တစ်ကြိမ်ပဲ Run ပြီး `break` လုပ်သောကြောင့် ဖယ်နိုင်သည်။
2. Main Menu `Console.ReadLine()` က `null` ဖြစ်လျှင် `continue` မလုပ်ဘဲ Gracefully Exit လုပ်ပါ။
3. `ReadPositiveAmount()` သည် EOF အတွက် `EndOfStreamException` Throw လုပ်သည်။ Caller မှ Catch မလုပ်ထားသေးသောကြောင့် Error ဖြင့် Terminate ဖြစ်နိုင်သည်။
4. Very large deposits များအတွက် `decimal` Overflow/Transaction Limits ကို နောက်ပိုင်းတွင် ထည့်စဉ်းစားရန်။
5. Withdrawal Loop မှ Main Menu သို့ Cancel လုပ်နိုင်မည့် Option ကို Future Improvement အဖြစ် ထည့်နိုင်သည်။

### Recommended Menu EOF Fix

```csharp
string? input = Console.ReadLine();

if (input is null)
{
    Console.WriteLine("Input closed. Goodbye!");
    return;
}

if (!int.TryParse(input, out int option))
{
    Console.WriteLine("Invalid menu option.");
    continue;
}
```

## 9. Knowledge Check

1. `10 % 3` ၏ Result ဘာလဲ? **1**
2. `true && false` ၏ Result ဘာလဲ? **false**
3. `score >= 50` ကို `score >= 80` ထက် အရင်စစ်လျှင် ဘာ Bug ဖြစ်နိုင်သလဲ? **Higher Grades ကို Lower Grade အဖြစ် သတ်မှတ်မိနိုင်သည်။**
4. Method မှ အစောပိုင်းထွက်ရန်? **`return`**
5. `false && SomeMethod()` မှာ `SomeMethod()` Call လုပ်မလား? **မလုပ်ပါ။**
6. Balance = Withdrawal Amount ဖြစ်လျှင်? **Allowed; remaining balance = 0.**

## 10. Key Takeaways

- Operators တွေက Data တွေကို Calculate, Compare, Combine လုပ်ပေးသည်။
- `if` / `else if` ၏ Condition Order သည် Result ကို သက်ရောက်စေသည်။
- `switch` သည် Menu-based Programs တွေအတွက် သင့်တော်သည်။
- `TryParse()` + Logical Operators ဖြင့် Input Validation လုပ်နိုင်သည်။
- Boundary Tests (`0`, Equal, Greater Than) ကို အမြဲ စဉ်းစားပါ။
- Reusable Methods နှင့် Clear Error Messages က Code Maintainability ကို တိုးတက်စေသည်။

> **Senior Developer Lesson:** Good developers write code that works. Great developers also think about what happens when the input is wrong.

---

**Next:** [Week 1 — Day 4: Loops, Iteration & Collections Basics](week-01-day-04.md)
