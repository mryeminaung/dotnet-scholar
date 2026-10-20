# Week 1 — Day 5: Methods, Parameters, Return Values & Variable Scope

> **Track:** .NET Full-Stack Developer Roadmap · Week 1 / Day 5  
> **Language:** မြန်မာ + English Technical Terms  
> **Duration:** ~2 hours  
> **Project:** Student Score Manager v4 — Modular Design  
> **Status:** Homework PASSED (9/10, static code review; minor known issues)

## Learning Objectives

- Method Declaration / Invocation ကို ရှင်းပြနိုင်ရန်။
- Parameters နဲ့ Arguments ကို ခွဲခြားနိုင်ရန်။
- `void` နဲ့ Value-Returning Methods ကို သင့်တော်စွာ ရွေးချယ်နိုင်ရန်။
- `return`, Early Return, Method Overloading, Optional Parameters, Named Arguments ကို အသုံးပြုရန်။
- Local, Block, Class-Level Scope ကို နားလည်ရန်။
- Value-Type / Reference-Type Parameter Passing, `ref`, `out` ကို ခွဲခြားရန်။
- Calculation Logic နဲ့ Console Display ကို Separate လုပ်ပြီး Modular Design တည်ဆောက်ရန်။

---

## 1. What Is a Method?

**Method** ဆိုတာ သီးသန့် Task တစ်ခုကို လုပ်ဖို့ Name ပေးထားတဲ့ Reusable Code Block ဖြစ်တယ်။ Code Reusability, Readability, Maintainability, Testability အတွက် အသုံးဝင်တယ်။

```csharp
static int Add(int a, int b)
{
    return a + b;
}

int result = Add(10, 20);
Console.WriteLine(result); // 30
```

| Part | Example | Meaning |
|---|---|---|
| Modifier | `static` | Instance မလိုဘဲ Call လုပ်နိုင် |
| Return Type | `int` | Result Type |
| Method Name | `Add` | Task ကို ဖော်ပြတဲ့ Name |
| Parameters | `int a, int b` | Method Definition ထဲက Input Variables |
| Arguments | `10, 20` | Method Call ထဲက Actual Values |
| `return` | `return a + b;` | Value ပြန်ပေးပြီး Method Exit |

**Execution Flow:** Caller → Arguments Bind → Method Body Execute → Return Value → Caller ဆီ ပြန်သွား။

### `void` vs Value-Returning

```csharp
static void PrintSquare(int number)
{
    Console.WriteLine(number * number); // Print only
}

static int CalculateSquare(int number)
{
    return number * number; // Reusable result
}

int square = CalculateSquare(5);
int doubled = square * 2;
```

`Console.WriteLine()` နဲ့ Print လုပ်တာဟာ Value Return မဟုတ်ဘူး။ **Calculation Logic ကို Return Value အဖြစ် ထုတ်ပေးတာ** က နောက်ထပ် Computation/Testing အတွက် ပို Reusable ဖြစ်တယ်။

### Early Return

```csharp
static string GetGrade(int score)
{
    if (score >= 80) return "A";
    if (score >= 70) return "B";
    if (score >= 60) return "C";
    if (score >= 50) return "D";
    return "F";
}
```

`return` Execute ဖြစ်တာနဲ့ Method ရပ်တယ်။ Condition အောက်က Branches တွေကို ဆက်မလုပ်တော့ဘူး။

---

## 2. Parameters, Arguments & Return Values

```csharp
static double CalculateAverage(int a, int b, int c)
{
    return (a + b + c) / 3.0;
}

double average = CalculateAverage(80, 90, 70);
```

- `a`, `b`, `c` က **Parameters** ဖြစ်တယ်။
- `80`, `90`, `70` က **Arguments** ဖြစ်တယ်။
- `double` Return Type က Caller ကို Numeric Result ပြန်ပေးတယ်။
- `(double)` သို့မဟုတ် `3.0` သုံးတာ Integer Division ကို ရှောင်နိုင်တယ်။

**Important:** Input Count ပြောင်းနိုင်တဲ့ `List<int>` အတွက် `/ 3` Hardcode မလုပ်ရ။ `/ scores.Count` သုံးရမယ်။

### Guard Clauses

```csharp
static double CalculateAverage(List<int> scores)
{
    if (scores.Count == 0)
        return 0; // This project's chosen empty-list policy

    long total = 0;
    foreach (int score in scores)
        total += score;

    return (double)total / scores.Count;
}
```

Empty List အတွက် `0` Return လုပ်တာက ဒီ Console Homework အတွက် ရွေးထားတဲ့ Policy ဖြစ်တယ်။ General-purpose Library မှာ Empty Average ကို Undefined လို့ယူပြီး Exception / Nullable Result သုံးတာလည်း ဖြစ်နိုင်တယ်။

---

## 3. Method Overloading

Method Name တူပြီး Parameter List (Number, Types, Order) ကွာတာကို **Overloading** လို့ ခေါ်တယ်။ Return Type တစ်ခုတည်း ကွာရုံနဲ့ Overload မဖြစ်ဘူး။

```csharp
static int Add(int a, int b) => a + b;
static int Add(int a, int b, int c) => a + b + c;
static double Add(double a, double b) => a + b;

Console.WriteLine(Add(10, 20));     // 30
Console.WriteLine(Add(10, 20, 30)); // 60
Console.WriteLine(Add(1.5, 2.5));   // 4
```

### Optional Parameters

```csharp
static void Greet(string name, string message = "Hello")
{
    Console.WriteLine($"{message}, {name}!");
}

Greet("Alex");
Greet("Alex", "Good morning");
```

### Named Arguments

```csharp
static void PrintStudent(string name, int age, int score)
{
    Console.WriteLine($"{name}: {age}, {score}");
}

PrintStudent(score: 90, name: "Alex", age: 20);
```

Named Arguments သုံးရင် Call Site မှာ Meaning ပိုရှင်းတယ်။

---

## 4. Variable Scope

**Scope** ဆိုတာ Variable ကို Code ဘယ်နေရာက Access လုပ်နိုင်သလဲ ဆိုတာပါ။ **Lifetime** က Data ဘယ်လောက်ကြာ ရှိနေသလဲ ဆိုတာဖြစ်လို့ မတူဘူး။

### Local Scope

```csharp
static void ShowMessage()
{
    string message = "Hello";
    Console.WriteLine(message);
}
// message ကို Method အပြင်မှာ Access မလုပ်နိုင်
```

### Block Scope

```csharp
int score = 85;
if (score >= 50)
{
    string result = "Pass";
    Console.WriteLine(result);
}
// result ကို ဒီနေရာမှာ Access မလုပ်နိုင်
```

### Class-Level Scope (OOP Preview)

```csharp
class Counter
{
    private int count = 0;

    public void Increment() => count++;
    public int GetCount() => count;
}
```

`count` က Instance Field ဖြစ်ပြီး Object ရဲ့ Methods တွေက Access လုပ်နိုင်တယ်။ `private` ကြောင့် Class အပြင်ကနေ Direct Access မလုပ်နိုင်ဘူး။

---

## 5. Value Types, Reference Types, `ref` and `out`

### Value Type — default by-value passing

```csharp
static void ChangeNumber(int number) => number = 100;

int original = 10;
ChangeNumber(original);
Console.WriteLine(original); // 10
```

`int` Value Copy ကို Method ထဲ ပို့တာကြောင့် Original Variable မပြောင်းဘူး။

### Reference Type — reference itself passed by value

```csharp
static void AddScore(List<int> scores) => scores.Add(100);

List<int> myScores = new() { 80, 90 };
AddScore(myScores);
Console.WriteLine(myScores.Count); // 3
```

Caller နဲ့ Callee က တူညီတဲ့ List Object ကို ညွှန်းထားတာကြောင့် `Add()` က Original Object ကို Modify လုပ်တယ်။ ဒါပေမဲ့ Parameter ကို New List နဲ့ Reassign လုပ်ရင် Caller Variable မပြောင်းဘူး။

### `ref`

```csharp
static void ChangeNumber(ref int number) => number = 100;
int x = 10;
ChangeNumber(ref x);
Console.WriteLine(x); // 100
```

`ref` အတွက် Caller Variable ကို Method Call မလုပ်ခင် Initialize လုပ်ထားရတယ်။

### `out`

```csharp
static bool TryDivide(int a, int b, out double result)
{
    if (b == 0)
    {
        result = 0;
        return false;
    }

    result = (double)a / b;
    return true;
}

if (TryDivide(10, 2, out double answer))
    Console.WriteLine(answer); // 5
```

`out` Parameter ကို Method က Normal Return မလုပ်ခင် Assign လုပ်ထားရတယ်။ `int.TryParse(text, out int number)` က ဒီ Pattern ရဲ့ Example ဖြစ်တယ်။

| Feature | `ref` | `out` |
|---|---|---|
| Caller Initialize? | Yes | No |
| Method must assign before normal return? | No | Yes |
| Common purpose | Existing variable ကို Modify | Additional result ပြန်ပို့ |

---

## 6. Modular Design: SRP and DRY

**SRP (Single Responsibility Principle):** Method / Component တစ်ခုဟာ Responsibility ရှင်းလင်းစွာ ရှိသင့်တယ်။

**DRY (Don't Repeat Yourself):** တူညီတဲ့ Validation / Display Logic ကို Copy/Paste မလုပ်ဘဲ Reuse လုပ်သင့်တယ်။

### Separate Calculation from Display

```csharp
static int CountPassingScores(List<int> scores)
{
    int count = 0;
    foreach (int score in scores)
    {
        if (score >= 50) count++;
    }
    return count;
}

// Display belongs in caller or a display method:
Console.WriteLine($"Pass Count: {CountPassingScores(scores)}");
```

### Shared 1-based Index Validation

```csharp
static int ReadScoreNumber(int count)
{
    while (true)
    {
        Console.Write($"Enter score number (1-{count}): ");
        string? input = Console.ReadLine();

        if (input is null)
            throw new EndOfStreamException("Input stream closed.");

        if (int.TryParse(input, out int number)
            && number >= 1 && number <= count)
        {
            return number;
        }

        Console.WriteLine("Invalid score number.");
    }
}
```

ဒီ Helper ကို **Check Grade** နဲ့ **Remove Score** နှစ်နေရာလုံးမှာ Reuse လုပ်နိုင်တယ်။ `count > 0` ဖြစ်တဲ့ Call Site မှာ အသုံးပြုဖို့ Design လုပ်ထားတယ်။

---

## 7. Homework: Student Score Manager v4

### Menu

```text
===== STUDENT SCORE MANAGER V4 =====
1. Add Score
2. View All Scores
3. Calculate Statistics
4. Remove Score
5. Check Grade
6. Exit
```

### Required Features

1. Score `0–100` Validate ပြီး Add လုပ်ပါ။
2. Numbered Score List ပြပါ။
3. Total, Average, Max, Min, Pass Count, Fail Count ပြပါ။
4. Score Number (1-based) ဖြင့် Remove လုပ်ပါ။
5. Score Number ရွေးပြီး Grade `A/B/C/D/F` ပြပါ။
6. Exit / Invalid Menu / Empty List ကို Handle လုပ်ပါ။

### Recommended Methods

```csharp
static void ShowMenu() { /* menu */ }
static int ReadScore() { /* validated score */ }
static void ViewScores(List<int> scores) { /* display */ }
static double CalculateAverage(List<int> scores) { /* return */ }
static int FindHighestScore(List<int> scores) { /* return */ }
static int FindLowestScore(List<int> scores) { /* return */ }
static int CountPassingScores(List<int> scores) { /* return */ }
static int CountFailingScores(List<int> scores) { /* return */ }
static string GetGrade(int score) { /* return */ }
static void RemoveScore(List<int> scores) { /* mutate list */ }
```

**Grade Boundaries:** A `>=80`, B `>=70`, C `>=60`, D `>=50`, F `<50`. Score `0` က Valid ဖြစ်ပြီး Grade `F` ဖြစ်တယ်။

---

## 8. Actual Homework Code Review History

### First v4 Submission — 8.5/10

**Critical Bug:**

```csharp
return (double)total / 3; // Wrong: list size can change
```

Correct:

```csharp
return (double)total / scores.Count;
```

Additional Review Feedback:

- `FindHighestScore()` / `FindLowestScore()` က `scores[0]` သုံးလို့ Empty List Contract ရှိဖို့လိုတယ်။
- Check Grade နဲ့ Remove Score မှာ 1-based Index Validation Duplicate ဖြစ်တယ်။
- Main `switch` ထဲမှာ Statistics Display Logic ရှည်နေတယ်; `ShowStatistics()` ခွဲနိုင်တယ်။
- EOF ကို Throw လုပ်ထားပေမဲ့ Catch မလုပ်ထားသေးဘူး။

### Revised v4 — 9/10, PASSED

**Fixed:**

- `CalculateAverage()` မှာ `/ scores.Count` ပြောင်းထားတယ်။
- Empty List Handling ထည့်ထားတယ်။
- `GetGrade()` ကို Early Return Pattern နဲ့ ရေးထားတယ်။
- Statistics Helpers တွေက Return Values ပြန်ပေးပြီး Console Display နဲ့ အများအားဖြင့် ခွဲထားတယ်။

**Known Issues accepted for this beginner homework:**

1. `GetGrade()` မှာ `if (score <= 0 || score > 100)` ဆိုထားလို့ **Score 0 ကို Invalid လို့ မှားသတ်မှတ်တယ်**။ Correct: `score < 0 || score > 100`။
2. `CalculateAverage()` ထဲမှာ `Console.WriteLine("No scores available.")` ပါနေသေးတယ် — Pure Calculation / Display Separation မပြည့်စုံသေးဘူး။
3. `ReadScoreNumber()` နဲ့ `ShowStatistics()` ကို Extract မလုပ်ရသေးဘူး — Optional DRY / Readability Improvement။
4. Case 3 မှာ Total ကို `int` နဲ့ Accumulate လုပ်ထားတယ်; Score အများကြီးရှိတဲ့အခါ `long` ပိုသင့်တော်တယ်။
5. EOF က Unhandled Exception ဖြစ်နိုင်တယ်; `try/catch` သို့မဟုတ် Graceful Exit Policy လိုတယ်။

**Review Outcome:** Homework Accepted. အထက်ပါ Issues တွေကို နောက် Lessons တွေမှာ ပြန်လေ့ကျင့်မယ်။ Review က Static Analysis ဖြစ်ပြီး Runtime Tests မလုပ်ထားဘူး။

---

## 9. Practical Exercises

### Exercise A — Temperature Converter

`static double CelsiusToFahrenheit(double celsius)` ရေးပါ။ Formula: `F = C * 9 / 5 + 32`။ Input `30` → Output `86.00`။

### Exercise B — Method Overloading

`CalculateArea(double side)` က Square Area; `CalculateArea(double width, double height)` က Rectangle Area ပြန်ပေးပါ။

### Exercise C — `TryGetGrade`

```csharp
static bool TryGetGrade(int score, out string grade)
{
    // Valid 0–100: true + grade A/B/C/D/F
    // Invalid: false + string.Empty
}
```

### Exercise D — Scope Prediction

```csharp
int x = 10;
if (x > 5)
{
    int y = 20;
    Console.WriteLine(x + y);
}
// Console.WriteLine(y); // Why doesn't this compile?
```

---

## 10. Quick Quiz

1. Parameter နဲ့ Argument ဘာကွာသလဲ?
2. `void` Method နဲ့ Value-Returning Method ဘာကွာသလဲ?
3. Method Overloading အတွက် Return Type တစ်ခုတည်း ပြောင်းရုံနဲ့ ရမလား?
4. Local Variable ကို Method အပြင်က Access လုပ်လို့ရသလား?
5. `int x = 10; Change(x);` မှာ Parameter ကို `100` ပြောင်းရင် Original `x` ဘယ်လောက်လဲ?
6. `List<int>` ကို Method ထဲပို့ပြီး `Add(100)` လုပ်ရင် Caller ရဲ့ List ပြောင်းမလား?
7. `ref` နဲ့ `out` ဘာကွာသလဲ?
8. `GetGrade(0)` ရဲ့ Expected Result ဘာလဲ?

<details>
<summary>Quiz Answers</summary>

1. Parameter က Definition ထဲက Input Variable; Argument က Call ထဲက Actual Value။
2. `void` က Value မပြန်; Value-Returning Method က သတ်မှတ်ထားတဲ့ Type ပြန်ပေးတယ်။
3. မရဘူး — Parameter List ကွာရမယ်။
4. မရဘူး — Scope အပြင်ဖြစ်တယ်။
5. `10` — default by-value passing။
6. ပြောင်းတယ် — တူညီတဲ့ List Object ကို Mutate လုပ်လို့။
7. `ref`: Caller က Initialize လုပ်ထားရ; `out`: Method က Normal Return မလုပ်ခင် Assign လုပ်ရ။
8. `F` — Score 0 က Valid Range ထဲမှာ ပါတယ်။

</details>

## 11. Manual Test Checklist

- [ ] Add 0 → Accepted; Grade F
- [ ] Add 100 → Accepted; Grade A
- [ ] Add -1, 101, abc → Rejected
- [ ] Empty List → Appropriate message
- [ ] `[80]` → Average 80.00
- [ ] `[80,90]` → Average 85.00
- [ ] `[80,90,70,60]` → Average 75.00
- [ ] `[85,70,45]` → Average 66.67; Pass 2; Fail 1
- [ ] Remove Score Number 2 from `[80,90,70]` → `[80,70]`
- [ ] Invalid Index 0 or Count+1 → Rejected
- [ ] Grade Boundary 49/50/59/60/69/70/79/80/100 → Correct

## Key Takeaways

> **A good method should have a clear purpose, meaningful inputs, and a predictable result.**

> **Never hardcode a value that should come from your data.**

- Calculation Methods မှာ Return Values ကို ဦးစားပေးပါ။
- Variable Scope နဲ့ Lifetime ကို မရောပါနဲ့။
- Default Parameter Passing က By Value ဖြစ်တယ် — Reference Type အတွက်လည်း Reference ကို Copy ပို့တာပါ။
- DRY / SRP ကို အသုံးပြုပြီး Reusable, Testable Methods ရေးပါ။
- Boundary Cases (`0`, `49`, `50`, `100`) နဲ့ Empty Lists ကို Test လုပ်ပါ။

**Next:** Week 1 Fundamentals Mini Assessment → Week 2 Advanced Practice.
