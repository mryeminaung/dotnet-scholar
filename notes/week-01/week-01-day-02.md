# Week 1 · Day 2 — Variables, Data Types, Type Conversion & Memory Basics

> **Track:** .NET Full-Stack Developer · **Level:** Beginner  
> **Language:** မြန်မာဘာသာ + English technical terms  
> **Estimated study time:** 2 hours

## 🎯 Learning Objectives

- Variables ရဲ့ Declaration, Initialization, Assignment ကို ခွဲခြားနိုင်ခြင်း
- C# Data Types တွေကို Use Case အလိုက် ရွေးနိုင်ခြင်း
- Value Type နဲ့ Reference Type ရဲ့ Copy Behavior ကို နားလည်ခြင်း
- Implicit/Explicit Conversion, `Parse()` နဲ့ `TryParse()` ကို ခွဲသိခြင်း
- Stack, Managed Heap နဲ့ Garbage Collection အခြေခံကို နားလည်ခြင်း
- **Student Grade Calculator** ကို Input Validation နဲ့ Methods အသုံးပြုပြီး ရေးနိုင်ခြင်း

---

## 1. Variables

**Variable** ဆိုတာ Program ထဲမှာ Value တစ်ခုကို နာမည်ပေးပြီး သိမ်းဆည်း/ပြန်အသုံးပြုနိုင်ဖို့ အသုံးပြုတဲ့ Storage Location ဖြစ်ပါတယ်။

```csharp
int age;          // Declaration
age = 20;         // Assignment
age = 21;         // Reassignment

int score = 90;   // Declaration + Initialization
```

Local Variable ကို Value Assign မလုပ်ခင် ဖတ်လို့မရပါ။

### Naming Conventions

| Element | Convention | Example |
|---|---|---|
| Local Variable | camelCase | `averageScore` |
| Method | PascalCase | `CalculateAverage()` |
| Class | PascalCase | `StudentProfile` |
| Property | PascalCase | `FirstName` |
| Interface | `I` + PascalCase | `IStudentService` |

### `var` Keyword

```csharp
var age = 24;          // Compiler infers int
var name = "Alex";     // Compiler infers string
var price = 19.99m;    // Compiler infers decimal

// age = "twenty-four"; // Compile-time error
```

`var` က **Compile-time Type Inference** ဖြစ်ပြီး Dynamic Type မဟုတ်ပါ။

## 2. Common C# Data Types

| Type | Size / Notes | Example | Typical Use |
|---|---|---|---|
| `byte` | 8-bit unsigned | `255` | Small nonnegative values |
| `int` | 32-bit signed | `24` | Whole Numbers |
| `long` | 64-bit signed | `9000000000L` | Large Integers |
| `float` | 32-bit floating-point | `3.14f` | Approximate Floating Point |
| `double` | 64-bit floating-point | `3.14159` | General Numerical Calculations |
| `decimal` | 128-bit decimal representation | `19.99m` | Money/Decimal Precision |
| `bool` | `true` / `false` | `true` | Conditions |
| `char` | UTF-16 Code Unit | `'A'` | Character Unit |
| `string` | Variable length, Reference Type | `"Hello"` | Text |

> `bool` ရဲ့ Actual Memory Layout က Context ပေါ်မူတည်နိုင်ပါတယ်။ `string` က Fixed-size Value Type မဟုတ်ပါ။

### `int` vs `double` vs `decimal`

```csharp
int quantity = 5;
double temperature = 36.5;
decimal unitPrice = 199.99m;
```

- `int` → Whole Numbers
- `double` → Approximate Floating-Point Calculations
- `decimal` → Base-10 Decimal Arithmetic (Financial Values အတွက် ပိုသင့်တော်)

```csharp
Console.WriteLine(0.1 + 0.2 == 0.3);      // False
Console.WriteLine(0.1m + 0.2m == 0.3m);   // True
```

`double` က Binary Floating-Point Representation ကို သုံးလို့ Decimal Fractions အချို့ကို Exact Represent မလုပ်နိုင်ပါ။ `decimal` ကို သုံးလည်း Business-specific Rounding Rules တွေကို သီးခြားစဉ်းစားရပါမယ်။

## 3. Value Types vs Reference Types

### Value Type — Value Copy

```csharp
int a = 10;
int b = a;
b = 50;

Console.WriteLine(a); // 10
Console.WriteLine(b); // 50
```

`b = a` မှာ Value Copy ဖြစ်လို့ `b` ကို ပြောင်းလည်း `a` မပြောင်းပါ။

### Reference Type — Reference Copy

```csharp
int[] scores1 = { 70, 80, 90 };
int[] scores2 = scores1;
scores2[0] = 100;

Console.WriteLine(scores1[0]); // 100
Console.WriteLine(scores2[0]); // 100
```

Array Reference နှစ်ခုလုံးက **တူညီတဲ့ Array Object** ကို ရည်ညွှန်းနေတာဖြစ်ပါတယ်။

### `string` က Reference Type ဖြစ်ပေမဲ့ Immutable ဖြစ်တယ်

```csharp
string first = "Hello";
string second = first;
second = "World";

Console.WriteLine(first);  // Hello
Console.WriteLine(second); // World
```

`second` ကို Reassign လုပ်တာက Original String Content ကို ပြောင်းတာ မဟုတ်ပါ။

> **Important:** “Value Type = Stack, Reference Type = Heap” လို့ အမြဲသတ်မှတ်တာ မမှန်ပါ။ Value Type တွေက Object Field အဖြစ် Heap ပေါ်မှာလည်း ပါဝင်နိုင်ပါတယ်။ Type ကို **Copy Semantics** နဲ့ အဓိကခွဲနားလည်ပါ။

## 4. Type Conversion

### Implicit Conversion

```csharp
int number = 100;
long bigNumber = number;
double result = number;
```

Compiler က ခွင့်ပြုထားတဲ့ Conversion ဖြစ်လို့ Cast ရေးစရာမလိုပါ။ ဒါပေမဲ့ Implicit Conversion အားလုံး Precision မပျောက်ဘူးလို့ မဆိုလိုပါ။

### Explicit Conversion (Casting)

```csharp
double price = 99.99;
int wholePrice = (int)price;
Console.WriteLine(wholePrice); // 99
```

ဒီ Casting က Decimal Fraction ကို Truncate လုပ်ပါတယ်။ Round မလုပ်ပါ။

### Integer Division Trap

```csharp
int a = 5;
int b = 2;

double result1 = a / b;
double result2 = (double)a / b;

Console.WriteLine(result1); // 2
Console.WriteLine(result2); // 2.5
```

`a / b` မှာ Operands နှစ်ခုလုံး `int` ဖြစ်လို့ Integer Division အရင်ဖြစ်ပါတယ်။ Average တွက်တဲ့အခါ Cast ကို **Division မလုပ်ခင်** သုံးဖို့ သတိထားပါ။

### `Parse()` vs `TryParse()` vs `Convert.ToInt32()`

| API | Valid Input | Invalid Numeric Text | Special Note |
|---|---|---|---|
| `int.Parse("25")` | `25` | Exception | Known-valid Input အတွက် |
| `int.TryParse("25", out int age)` | `true`, `age = 25` | `false` | User Input Validation အတွက် သင့်တော် |
| `Convert.ToInt32("25")` | `25` | Exception | `Convert.ToInt32((string?)null)` က `0` |

```csharp
Console.Write("Enter age: ");
string? input = Console.ReadLine();

if (int.TryParse(input, out int age) && age >= 0)
{
    Console.WriteLine($"Accepted: {age}");
}
else
{
    Console.WriteLine("Invalid age.");
}
```

## 5. Memory Basics

### Stack

Stack က Method Call Frames နဲ့ အချို့ Local Data တွေကို Manage လုပ်ရာမှာ အသုံးပြုပါတယ်။

### Managed Heap

Reference Type Objects တွေကို ပုံမှန်အားဖြင့် Managed Heap မှာ Allocate လုပ်ပါတယ်။

### Garbage Collector (GC)

```csharp
int[] scores = { 80, 90, 100 };
scores = new int[] { 60, 70, 80 };
```

ပထမ Array ကို တခြား References မရှိတော့ဘူးဆိုရင် GC အတွက် Eligible ဖြစ်လာနိုင်ပါတယ်။ GC က Memory ကို **ချက်ချင်း** Reclaim လုပ်မယ်လို့ မဆိုလိုပါ။

## 6. Practical Project — Student Grade Calculator v2

### Requirements

1. Student Name Input လက်ခံပါ။ Empty/Whitespace Name ဆို `Unknown` သုံးပါ။
2. Math, English, Science Scores ကို Input လက်ခံပါ။
3. Score တစ်ခုစီ `0–100` အတွင်း Integer ဖြစ်ရပါမယ်။ Invalid ဆို ထပ်မေးပါ။
4. Average ကို `double` နဲ့ Calculate လုပ်ပါ။
5. Average `>= 50` ဆို `Pass`၊ မဟုတ်ရင် `Fail` ဖြစ်ပါမယ်။
6. Grade ကို သတ်မှတ်ထားတဲ့ Boundaries အတိုင်း တွက်ပါ။

| Average | Grade |
|---|---|
| 80–100 | A |
| 70–below 80 | B |
| 60–below 70 | C |
| 50–below 60 | D |
| Below 50 | F |

### Final Refactored Example

```csharp
Console.Write("Enter your name: ");
string? nameInput = Console.ReadLine();
string name = string.IsNullOrWhiteSpace(nameInput)
    ? "Unknown"
    : nameInput.Trim();

int mathScore = ReadScore("Math");
int englishScore = ReadScore("English");
int scienceScore = ReadScore("Science");

double averageScore = CalculateAverage(mathScore, englishScore, scienceScore);
string result = averageScore >= 50 ? "Pass" : "Fail";
string grade = CalculateGrade(averageScore);

Console.WriteLine();
Console.WriteLine("===== STUDENT RESULT =====");
Console.WriteLine($"Name: {name}");
Console.WriteLine($"Math: {mathScore}");
Console.WriteLine($"English: {englishScore}");
Console.WriteLine($"Science: {scienceScore}");
Console.WriteLine($"Average: {averageScore:F2}");
Console.WriteLine($"Result: {result}");
Console.WriteLine($"Grade: {grade}");
Console.WriteLine("==========================");

static string CalculateGrade(double average)
{
    if (average >= 80) return "A";
    if (average >= 70) return "B";
    if (average >= 60) return "C";
    if (average >= 50) return "D";
    return "F";
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
        string? input = Console.ReadLine();

        if (input is null)
        {
            throw new EndOfStreamException("Input stream was closed.");
        }

        if (int.TryParse(input, out int score) && score >= 0 && score <= 100)
        {
            return score;
        }

        Console.WriteLine("Invalid score. Enter a number between 0 and 100.");
    }
}
```

**Expected Output** (sample):

```text
Enter your name: Alex
Enter Math score (0-100): 80
Enter English score (0-100): 75
Enter Science score (0-100): 90

===== STUDENT RESULT =====
Name: Alex
Math: 80
English: 75
Science: 90
Average: 81.67
Result: Pass
Grade: A
==========================
```

### Code Walkthrough

- **`ReadScore()`** → User Input + Range Validation (Reusable Method)
- **`CalculateAverage()`** → Mathematical Calculation; `(double)` က Integer Division ကို ရှောင်ပေးတယ်။
- **`CalculateGrade()`** → Business Rules ကို Main Program ကနေ ခွဲထားတယ်။
- **`string.IsNullOrWhiteSpace()`** → `null`, Empty, Whitespace-only Names ကို စစ်တယ်။
- **`{averageScore:F2}`** → Output ကို Decimal Places နှစ်နေရာနဲ့ Format လုပ်တယ်။ Grade Decision က Formatted String မဟုတ်ဘဲ Raw Average ကို သုံးတယ်။
- **Ternary Operator** → `condition ? ifTrue : ifFalse`

## 7. Debugging Lessons from Code Review

### Bug: Grade Thresholds မကိုက်ညီခြင်း

အရင် Version မှာ `average >= 90` ကို Grade A လို့ သတ်မှတ်မိခဲ့တယ်။ Homework Requirement က `average >= 80` ဖြစ်လို့ **Business Logic Bug** ဖြစ်ပါတယ်။

### Bug: Boundary Gaps

```csharp
// Avoid: 79.99 နဲ့ 80 ကြား Gap ရှိနိုင်တယ်
else if (average >= 70 && average < 79.99)
```

**Better:** Descending Thresholds ကို `if / else if` (သို့) Early Returns နဲ့ ရေးပါ။

```csharp
if (average >= 80) return "A";
if (average >= 70) return "B";
```

### Bug: Grade D ကို C လို့ မှားသတ်မှတ်ခြင်း

Grade Thresholds တွေကို Test Cases နဲ့ Verify လုပ်ပါ။ Code က Compile ဖြစ်တာနဲ့ Requirements မှန်တယ်လို့ မဆိုလိုပါ။

### Improvement: Method Naming

```csharp
// Less idiomatic for C# methods
scoreInput();
calculateAverage();

// Prefer PascalCase and descriptive names
ReadScore();
CalculateAverage();
```

### Improvement: Avoid Redundant Validation

`ReadScore()` က `0–100` အတွင်း Score ပဲ Return ပြန်ရင် Main Program မှာ `mathScore >= 0` ကို ထပ်စစ်ဖို့ မလိုပါ။

## 8. Manual Test Cases

| Input / Scenario | Expected Behavior |
|---|---|
| `80, 80, 80` | Average 80.00 → A / Pass |
| `70, 70, 70` | Average 70.00 → B / Pass |
| `60, 60, 60` | Average 60.00 → C / Pass |
| `50, 50, 50` | Average 50.00 → D / Pass |
| `49, 49, 49` | Average 49.00 → F / Fail |
| `101` as score | Reject and Retry |
| `-1` as score | Reject and Retry |
| `abc` as score | Reject and Retry |
| Empty Score | Reject and Retry |
| Empty/Whitespace Name | Display `Unknown` |
| Input Stream Closes | `EndOfStreamException` in example |

> **Testing Reminder:** Code Review က Runtime Testing အစားမထိုးနိုင်ပါ။ `dotnet run` နဲ့ တကယ်စမ်းပါ။

## 9. Homework / Extra Practice

**Grade Calculator v3 (Optional)**

- `ReadScore()` ကို Subject အသစ်တွေမှာ Reuse လုပ်ပါ။
- Subject Count ပြောင်းနိုင်အောင် Design စဉ်းစားပါ။
- Name Validation နဲ့ Grade Calculation ကို သီးခြား Methods တွေအဖြစ် Refactor လုပ်ကြည့်ပါ။
- Grade Boundary Tests တွေကို Input မျိုးစုံနဲ့ စမ်းပါ။

## 10. Quick Quiz

1. `var age = 24;` မှာ `age` ရဲ့ Type က ဘာလဲ?
2. Financial Calculations အတွက် `double` နဲ့ `decimal` ဘယ်ဟာ ပိုသင့်တော်လဲ?
3. `double result = 5 / 2;` ရဲ့ Value က ဘာလဲ?
4. Value Type Assignment နဲ့ Array Reference Assignment ဘာကွာလဲ?
5. `int.TryParse("abc", out int value)` က ဘာ Return ပြန်မလဲ?
6. Average `79.67` ဆို Required Grade က ဘာလဲ?
7. `Console.ReadLine() ?? "Unknown"` က Empty String ကို Handle လုပ်ပေးသလား?

<details>
<summary>Show Answers</summary>

1. `int`။
2. `decimal` (Money/Decimal Arithmetic အတွက် ပိုသင့်တော်)။
3. `2` (`double` Variable ထဲမှာ `2.0` အဖြစ် သိမ်းမယ်)။
4. Value Type က Value Copy; Array Assignment က Reference Copy။
5. `false`။
6. B။
7. မလုပ်ပါ။ `string.IsNullOrWhiteSpace()` ကို သုံးနိုင်ပါတယ်။

</details>

## 11. Key Takeaways

- **Data Type** ကို Data ရဲ့ Meaning နဲ့ Calculation လိုအပ်ချက်အလိုက် ရွေးပါ။
- **`var`** က Dynamic Typing မဟုတ်ပါ။
- **Value vs Reference** ကို Copy Behavior နဲ့ အရင်နားလည်ပါ။
- **Integer Division** ကို သတိထားပါ။
- **User Input** ကို `TryParse()` နဲ့ Validate လုပ်ပါ။
- **Business Rules** ကို Methods ထဲ ခွဲထားရင် Maintain/Test လုပ်ရလွယ်ပါတယ်။
- **Grade Boundaries** လို Edge Cases တွေကို Test လုပ်ပါ။

**Previous:** [Week 1 · Day 1 — Introduction to C# and .NET](week-01-day-01.md)  
**Next:** [Week 1 · Day 3 — Operators, Conditional Statements & Logical Thinking](week-01-day-03.md)
