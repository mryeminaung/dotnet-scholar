# Week 2 · Day 3 — Debugging, Exceptions & Error Handling

> **Course:** .NET Full-Stack Developer Roadmap  
> **Lesson:** Week 2, Day 3 (Lesson 08)  
> **Duration:** ~2 hours  
> **Status:** Lesson delivered; homework pending

## Learning Objectives

- Compile-time, runtime, logical errors ကို ခွဲခြားနိုင်ရန်
- Debugger, breakpoint, step over/into/out ကို သုံးနိုင်ရန်
- `try`, `catch`, `finally`, `throw` ကို နားလည်ရန်
- Expected invalid input အတွက် `TryParse()` သုံးရန်
- Exception propagation, stack trace နဲ့ graceful exit ကို နားလည်ရန်
- Student Score Manager v7 မှာ defensive error handling ထည့်ရန်

---

## 1. Error Types

### Compile-time error

Compiler က code ကို build လုပ်စဉ် တွေ့တဲ့ error။

```csharp
int score = "85"; // Cannot implicitly convert string to int
```

### Runtime error

Build အောင်မြင်ပြီး run နေစဉ် ဖြစ်တဲ့ error။

```csharp
List<int> scores = new() { 85, 70 };
Console.WriteLine(scores[5]); // ArgumentOutOfRangeException
```

> **Note:** `List<T>` ရဲ့ invalid index မှာ `ArgumentOutOfRangeException` ဖြစ်တယ်။ Array ရဲ့ invalid index မှာတော့ `IndexOutOfRangeException` ဖြစ်တယ်။

### Logical error

Program က run နိုင်ပေမဲ့ output မှားနေတာ။

```csharp
int total = 200;
int count = 4;
double average = (double)total / 3; // Wrong divisor
// Correct: (double)total / count
```

### Quick comparison

| Type | Build? | Run? | Typical example |
|---|---|---|---|
| Compile-time | ❌ | — | Wrong type assignment |
| Runtime | ✅ | May fail | Invalid collection index |
| Logical | ✅ | ✅ | Incorrect average formula |

---

## 2. Debugging with Breakpoints

**Debugging** = Bug ရဲ့ root cause ကို ရှာဖို့ program state နဲ့ execution flow ကို စစ်ခြင်း။

```csharp
List<int> scores = new() { 85, 70, 45 };
int total = 0;

foreach (int score in scores)
{
    total += score; // Set breakpoint here
}

double average = (double)total / scores.Count;
Console.WriteLine($"Average: {average:F2}");
```

Trace:

| Iteration | score | total |
|---|---:|---:|
| Before loop | — | 0 |
| 1 | 85 | 85 |
| 2 | 70 | 155 |
| 3 | 45 | 200 |

Final average = `200 / 3 ≈ 66.67`.

### Visual Studio debugging shortcuts

| Action | Shortcut | Purpose |
|---|---|---|
| Toggle breakpoint | F9 | Stop at a statement |
| Start / Continue | F5 | Run until breakpoint |
| Step Over | F10 | Execute statement without entering called method |
| Step Into | F11 | Enter called method |
| Step Out | Shift+F11 | Finish current method and return |

VS Code keybindings may vary. Use **Locals**, **Watch**, **Call Stack**, and **Exception** panels to inspect state.

**Debugging checklist:** Reproduce → Set breakpoint → Inspect variables → Find first incorrect value → Fix → Retest boundary cases.

---

## 3. Exceptions in C#

**Exception** = Runtime မှာ exceptional condition တစ်ခုကို ဖော်ပြတဲ့ object။ Unhandled exception ဖြစ်ရင် program execution ကို အဆုံးသတ်စေနိုင်တယ်။

| Exception | Common cause |
|---|---|
| `FormatException` | `int.Parse("abc")` |
| `DivideByZeroException` | Integer divide by zero |
| `IndexOutOfRangeException` | Array index out of bounds |
| `ArgumentOutOfRangeException` | Invalid `List<T>` index |
| `KeyNotFoundException` | Missing dictionary key via indexer |
| `NullReferenceException` | Access member on null reference |
| `FileNotFoundException` | Missing file |
| `EndOfStreamException` | Input/stream ended when more data required |

```csharp
int number = int.Parse("hello"); // FormatException
```

User ရိုက်တဲ့ text မှားတာလို **expected failure** တွေအတွက် exception ကို normal branching အစား မသုံးဘဲ `TryParse()` သုံးတာ ပိုကောင်းတယ်။

---

## 4. `try`, `catch`, `finally`

```csharp
try
{
    Console.Write("Enter an integer: ");
    string? input = Console.ReadLine();
    int number = int.Parse(input!);
    Console.WriteLine($"Number: {number}");
}
catch (FormatException)
{
    Console.WriteLine("Invalid number format.");
}
```

ဒီ example က `FormatException` ကိုသာ catch လုပ်တယ်။ Null/EOF နဲ့ အခြား failure တွေအတွက် complete handling မဟုတ်သေးဘူး။

### Multiple catches

```csharp
try
{
    int[] numbers = { 10, 20, 30 };
    int index = int.Parse(Console.ReadLine()!);
    Console.WriteLine(numbers[index]);
}
catch (FormatException)
{
    Console.WriteLine("Enter a valid integer.");
}
catch (IndexOutOfRangeException)
{
    Console.WriteLine("Array index is out of range.");
}
```

### `finally`

```csharp
try
{
    Console.WriteLine("Processing...");
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected error: {ex.Message}");
}
finally
{
    Console.WriteLine("Processing finished.");
}
```

`finally` ကို cleanup အတွက် သုံးတယ်။ Normal execution နဲ့ exception unwinding မှာ run ပေမဲ့ forced process termination လို အခြေအနေတိုင်းအတွက် guarantee မရှိဘူး။

**Best practice:** Handle လုပ်နိုင်တဲ့ specific exception ကို catch လုပ်ပါ။ `catch (Exception)` နဲ့ error ကို silent ignore မလုပ်ပါနဲ့။

---

## 5. `throw` and Exception Propagation

Method contract ချိုးဖောက်တဲ့ argument အတွက် exception throw လုပ်နိုင်တယ်။

```csharp
static double CalculateAverage(List<int> scores)
{
    ArgumentNullException.ThrowIfNull(scores);

    if (scores.Count == 0)
    {
        throw new ArgumentException(
            "Cannot calculate average of an empty list.",
            nameof(scores));
    }

    long total = 0;
    foreach (int score in scores)
    {
        total += score;
    }

    return (double)total / scores.Count;
}
```

Caller:

```csharp
try
{
    Console.WriteLine(CalculateAverage(new List<int>()));
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}
```

### `throw;` vs `throw ex;`

```csharp
try
{
    DoWork();
}
catch (Exception)
{
    // Log here if needed.
    throw; // Preserves original stack trace
}

static void DoWork() => throw new InvalidOperationException("Failed");
```

`throw ex;` က original stack trace ကို ထိခိုက်စေနိုင်လို့ rethrow လုပ်တဲ့အခါ `throw;` ကို သုံးပါ။

---

## 6. Validation vs Exceptions

| Situation | Better approach |
|---|---|
| User types `abc` for a number | `int.TryParse()` |
| Score outside 0–100 | Range validation |
| Student ID not found | `Dictionary.TryGetValue()` |
| Empty scores passed to strict average method | Guard + documented exception |
| File missing while reading | Handle `FileNotFoundException` where recovery is possible |
| Input stream ends unexpectedly | Graceful exit or explicit EOF contract |

**Rule:** Expected input errors ကို normal validation flow နဲ့ handle လုပ်ပြီး exceptional program conditions အတွက် exceptions ကို သုံးပါ။

---

## 7. Complete Code Walkthrough — Robust Score Input

```csharp
try
{
    int score = ReadScore();
    Console.WriteLine($"Score accepted: {score}");
}
catch (EndOfStreamException)
{
    Console.WriteLine("Input stream closed. Exiting gracefully.");
}

static int ReadScore()
{
    while (true)
    {
        Console.Write("Enter score (0-100): ");
        string? input = Console.ReadLine();

        if (input is null)
        {
            throw new EndOfStreamException("Input stream closed.");
        }

        if (!int.TryParse(input, out int score))
        {
            Console.WriteLine("Please enter a valid integer.");
            continue;
        }

        if (score < 0 || score > 100)
        {
            Console.WriteLine("Score must be between 0 and 100.");
            continue;
        }

        return score;
    }
}
```

**Walkthrough:**

1. `ReadLine()` မှာ EOF ဖြစ်နိုင်လို့ nullable string လက်ခံတယ်။
2. EOF ဖြစ်ရင် `EndOfStreamException` throw လုပ်တယ်။
3. `TryParse()` က invalid integer ကို safe reject လုပ်တယ်။
4. Score ကို inclusive range `0..100` နဲ့ validate လုပ်တယ်။
5. Valid score ရမှ `return` လုပ်တယ်။
6. Caller က EOF ကို catch လုပ်ပြီး friendly exit message ပြတယ်။

### Boundary tests

| Input | Expected |
|---|---|
| `0` | Accept |
| `50` | Accept |
| `100` | Accept |
| `-1` | Reject |
| `101` | Reject |
| `abc` | Reject and retry |
| Empty input | Reject and retry |
| EOF (`null`) | Exit gracefully via caller |

---

## 8. Practical Exercises

### Exercise 1 — Identify the error type

**A**
```csharp
int age = "twenty";
```

**B**
```csharp
List<int> numbers = new() { 10, 20 };
Console.WriteLine(numbers[3]);
```

**C**
```csharp
int total = 200;
int count = 4;
double average = total / 3;
```

Classify each as compile-time, runtime, or logical error.

### Exercise 2 — Safe dictionary lookup

```csharp
Dictionary<int, int> scores = new()
{
    { 101, 85 },
    { 102, 70 }
};

Console.WriteLine(scores[999]);
```

`TryGetValue()` သုံးပြီး safe lookup ပြန်ရေးပါ။

### Exercise 3 — Average contract

- `CalculateAverage(List<int>)` ရေးပါ။
- Empty list ဖြစ်ရင် `ArgumentException` throw လုပ်ပါ။
- Valid list ဖြစ်ရင် average ပြန်ပါ။
- Caller မှာ `try-catch` နဲ့ handle လုပ်ပါ။

---

## 9. Quick Quiz (5 Questions)

**Q1.** Program run နိုင်ပေမဲ့ average result မှားနေတယ်။ ဘယ် error type လဲ?  
A. Compile-time · **B. Logical** · C. Syntax · D. Linker

**Q2.** Visual Studio breakpoint shortcut?  
A. F5 · **B. F9** · C. F10 · D. F11

**Q3.** Dictionary safe lookup?  
A. Add · B. Remove · **C. TryGetValue** · D. Clear

**Q4.** Original stack trace ထိန်းပြီး rethrow လုပ်ရန်?  
A. throw ex; · B. return ex; · **C. throw;** · D. catch;

**Q5.** User enters `abc` for integer; best approach?  
A. Always Parse + catch · **B. TryParse** · C. Force exit · D. Ignore input

**Answer Key:** 1-B, 2-B, 3-C, 4-C, 5-B.

---

## 10. Homework — Student Score Manager v7 (Pending)

**Goal:** Day 1 Dictionary-Based Manager နဲ့ Day 2 Text Parser ကို ပေါင်းပြီး robust error handling ထည့်ရန်။

### Requirements

1. Numeric input ကို `TryParse()` နဲ့ validate လုပ်ပါ။
2. Score range `0..100` ကို စစ်ပါ။
3. Duplicate Student IDs ကို reject လုပ်ပါ။
4. Missing Student IDs ကို `TryGetValue()` နဲ့ safe handle လုပ်ပါ။
5. EOF ကို graceful exit လုပ်ပါ။
6. Empty collection မှာ statistics ကို safe handle လုပ်ပါ။
7. Method တစ်ခုမှာ appropriate exception throw လုပ်ပြီး caller မှာ catch လုပ်ပါ။
8. Meaningful error messages ပြပါ။

**Optional Bonus:** `InvalidStudentRecordException` custom exception class စမ်းရေးပါ။ (OOP မှာ အသေးစိတ် သင်မယ်။)

### Evaluation Rubric

| Category | Marks |
|---|---:|
| Input Validation | 20 |
| Exception Handling | 25 |
| Dictionary Operations | 15 |
| Method Design | 15 |
| Edge Cases | 15 |
| Readability | 10 |
| **Total** | **100** |

**Pass mark:** 70/100. **Submission status:** Pending.

---

## 11. Senior Developer Checklist

- [ ] Compile-time, runtime, logical error ကို ခွဲနိုင်တယ်။
- [ ] Breakpoint ထားပြီး variables နဲ့ call stack ကို inspect လုပ်နိုင်တယ်။
- [ ] `TryParse()` ကို expected invalid input အတွက် သုံးတယ်။
- [ ] Specific exceptions ကိုပဲ catch လုပ်တယ်။
- [ ] Exception ကို silent ignore မလုပ်ဘူး။
- [ ] `throw;` နဲ့ original stack trace ထိန်းတယ်။
- [ ] Boundary values (`0`, `100`, `-1`, `101`) ကို test လုပ်တယ်။
- [ ] Empty collection, missing key, EOF ကို စဉ်းစားထားတယ်။

> **Key takeaway:** Working code တင်မကဘဲ failure ဖြစ်တဲ့အခါ predictable behavior ရှိတဲ့ code ကို ရေးပါ။

**Next on main roadmap:** Week 2 Day 4 — File I/O (currently paused while studying Module 6).
