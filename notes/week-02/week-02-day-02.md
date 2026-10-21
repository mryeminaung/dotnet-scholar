 # Week 2 — Day 2: Strings, StringBuilder & Data Processing

> **Course:** .NET Full-Stack Developer Roadmap (20 Weeks / 200 Hours)  
> **Lesson:** 07 | **Duration:** ~2 hours  
> **Status:** Lesson delivered; Student Score Manager v6 homework **pending**  
> **Prerequisites:** Week 2 Day 1 (`Dictionary`, input validation); Week 1 Day 5 (`out` parameters, methods)

## Learning objectives

- `string` က reference type ဖြစ်ပြီး immutable ဖြစ်တာကို နားလည်ရန်။
- `Trim`, `ToUpper`, `ToLower`, `Contains`, `Replace`, `Substring`, `Split`, `Join` သုံးနိုင်ရန်။
- Case-insensitive comparison ကို `StringComparison.OrdinalIgnoreCase` နဲ့ ပြုလုပ်နိုင်ရန်။
- `StringBuilder` ကို repeated text construction အတွက် သုံးနိုင်ရန်။
- `ID,Name,Score` text record ကို parse, normalize, validate လုပ်နိုင်ရန်။

## 1. Strings and immutability

```csharp
string message = "Hello";
message = message + " World";
Console.WriteLine(message); // Hello World
```

`string` က **reference type** ဖြစ်ပေမဲ့ **immutable** ဖြစ်တယ်။ `"Hello"` Object ကို တိုက်ရိုက် ပြောင်းတာမဟုတ်ဘဲ `"Hello World"` String အသစ်ရပြီး Variable ကို reassign လုပ်တာပါ။

```csharp
string original = "CSharp";
string copy = original;
copy = "DotNet";
Console.WriteLine(original); // CSharp
Console.WriteLine(copy);     // DotNet
```

`copy` ကို reassign လုပ်ခြင်းက `original` ကို မပြောင်းဘူး။

## 2. Essential string operations

### `Trim()` — surrounding whitespace

```csharp
string name = "   Aung Aung   ";
Console.WriteLine($"[{name.Trim()}]"); // [Aung Aung]
```

`Trim()` က အစနဲ့ အဆုံးက whitespace ကို ဖယ်တယ်; အလယ်က space ကို မဖယ်ဘူး။

### `ToUpper()` / `ToLower()`

```csharp
string language = "CSharp";
Console.WriteLine(language.ToUpper()); // CSHARP
Console.WriteLine(language.ToLower()); // csharp
```

### `Contains()`

```csharp
string email = "student@example.com";
Console.WriteLine(email.Contains('@')); // True
```

`@` ပါတယ်ဆိုရုံနဲ့ email address မှန်တယ်လို့ မဆိုနိုင်ဘူး။

### `Replace()`

```csharp
string message = "I like Java";
string updated = message.Replace("Java", "C#");
Console.WriteLine(updated); // I like C#
```

### `Substring(startIndex, length)`

```csharp
string text = "CSharp";
Console.WriteLine(text.Substring(0, 1)); // C
Console.WriteLine(text.Substring(1, 5)); // Sharp
```

Index/length invalid ဖြစ်ရင် `ArgumentOutOfRangeException` ဖြစ်နိုင်တယ်။

## 3. `Split()` and `Join()`

Input record: `101,Aung Aung,85`.

```csharp
string record = "101,Aung Aung,85";
string[] parts = record.Split(',');
Console.WriteLine(parts[0]); // 101
Console.WriteLine(parts[1]); // Aung Aung
Console.WriteLine(parts[2]); // 85
```

`Split(',')` → `parts[0]`, `parts[1]`, `parts[2]` ဆိုတဲ့ 3 parts ဖြစ်တယ်။ **`parts.Length` ကို စစ်ပြီးမှ index access လုပ်ပါ။**

```csharp
string[] subjects = { "C#", "SQL", "React" };
Console.WriteLine(string.Join(", ", subjects)); // C#, SQL, React
```

`Split` = one string → multiple parts; `Join` = multiple parts → one string.

## 4. Safe string comparison

```csharp
string answer = "YES";
Console.WriteLine(answer == "yes"); // False
```

Case-insensitive technical command compare:

```csharp
if (string.Equals(answer, "yes", StringComparison.OrdinalIgnoreCase))
    Console.WriteLine("Confirmed");
```

`ToLower() == "yes"` လည်း လုပ်နိုင်ပေမဲ့ case-insensitive ordinal comparison ကို တိုက်ရိုက် ဖော်ပြချင်ရင် `StringComparison.OrdinalIgnoreCase` က ပိုရှင်းတယ်။

### Blank and null inputs

```csharp
string? name = Console.ReadLine();
if (name is null)
{
    Console.WriteLine("Input stream closed.");
    return;
}
if (string.IsNullOrWhiteSpace(name))
{
    Console.WriteLine("Name cannot be empty.");
    return;
}
```

`IsNullOrWhiteSpace` က `null`, `""`, `"   "` ကို detect လုပ်တယ်။ Console EOF (`null`) နဲ့ blank input ကို သီးခြား handle လုပ်တာ ပိုကောင်းတယ်။

## 5. `StringBuilder` and performance

Repeated string concatenation:

```csharp
string report = "";
for (int i = 1; i <= 1000; i++)
    report += $"Student {i}\n";
```

`string` immutable ဖြစ်တာကြောင့် repeated concatenation မှာ intermediate strings များစွာ ဖြစ်နိုင်တယ်။

```csharp
using System.Text;

StringBuilder report = new();
for (int i = 1; i <= 1000; i++)
    report.AppendLine($"Student {i}");
Console.WriteLine(report.ToString());
```

| Scenario | Good choice |
|---|---|
| Name, email, short text | `string` |
| A few interpolations | `string` |
| Long report in a loop | `StringBuilder` |

`StringBuilder` က အမြဲပိုမြန်တယ်လို့ မဆိုလိုပါ; small text မှာ `string` က ပိုရိုးရှင်းတယ်။

## 6. Complete code walkthrough: Student record parser

**Format:** `StudentID,Name,Score`  
**Example:** `101,Aung Aung,85`

```csharp
Console.Write("Enter student record (ID,Name,Score): ");
string? input = Console.ReadLine();

if (input is null)
{
    Console.WriteLine("Input closed.");
    return;
}

if (TryParseStudent(input, out int studentId,
                    out string studentName, out int studentScore))
{
    Console.WriteLine("\nValid Student Record");
    Console.WriteLine($"ID: {studentId}");
    Console.WriteLine($"Name: {studentName}");
    Console.WriteLine($"Score: {studentScore}");
}
else
{
    Console.WriteLine("Invalid student record.");
}

static bool TryParseStudent(string input, out int id,
                            out string name, out int score)
{
    id = 0;
    name = "";
    score = 0;

    string[] parts = input.Split(',');
    if (parts.Length != 3)
        return false;

    string idText = parts[0].Trim();
    string nameText = parts[1].Trim();
    string scoreText = parts[2].Trim();

    if (!int.TryParse(idText, out int parsedId) || parsedId <= 0)
        return false;

    if (string.IsNullOrWhiteSpace(nameText))
        return false;

    if (!int.TryParse(scoreText, out int parsedScore)
        || parsedScore < 0 || parsedScore > 100)
        return false;

    id = parsedId;
    name = nameText;
    score = parsedScore;
    return true;
}
```

### Walkthrough

1. `out` parameters တွေကို Method အစမှာ assign လုပ်တယ် (`id=0`, `name=""`, `score=0`)။
2. `Split(',')` နဲ့ fields ခွဲတယ်။
3. `parts.Length == 3` ဖြစ်မှ index access လုပ်တယ်။
4. `Trim()` နဲ့ surrounding spaces ဖယ်တယ်။
5. ID positive, name nonblank, score `0–100` ဆိုတဲ့ rules စစ်တယ်။
6. All valid ဖြစ်မှ output parameters ကို parsed values assign လုပ်ပြီး `true` ပြန်တယ်။

ဒါက .NET မှာ အသုံးများတဲ့ **Try-pattern** ဖြစ်တယ်။ ဒီ parser က *simple comma-separated input* အတွက်သာ ဖြစ်ပြီး quoted comma ပါတဲ့ full CSV ကို မထောက်ပံ့သေးဘူး။

### Sample test cases

| Input | Expected |
|---|---|
| `101,Aung Aung,85` | Valid |
| `102,Su Su,0` | Valid (`0` is allowed) |
| `103, ,90` | Invalid (blank name) |
| `104,Ko Ko,120` | Invalid (score too high) |
| `0,Aung,80` | Invalid (ID not positive) |
| `105,Aung` | Invalid (missing field) |
| `106,Aung,70,Extra` | Invalid (extra field) |

## 7. Practice exercises

1. `"   aung aung   "` ကို `Trim()` နဲ့ `ToUpper()` သုံးပြီး `AUNG AUNG` ထုတ်ပါ။
2. `"CSharp is a powerful language"` ထဲက words အရေအတွက် တွက်ပါ။ Hint: `Split(' ', StringSplitOptions.RemoveEmptyEntries)`.
3. `205,Su Su,95` ကို Parse လုပ်ပြီး ID, Name, Score နဲ့ Grade `A` ထုတ်ပါ။ Week 1 `GetGrade()` Method ကို reuse လုပ်ပါ။

<details><summary>Exercise hints / answers</summary>

```csharp
Console.WriteLine("   aung aung   ".Trim().ToUpper());

string sentence = "CSharp is a powerful language";
Console.WriteLine(sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length); // 5

// Exercise 3: reuse TryParseStudent(...) and your Week 1 GetGrade(score).
```

</details>

## 8. Quick quiz (5 questions)

1. `string` immutable ဆိုတာ? **A** Reassign မရ / **B** Existing string object content ကို မပြောင်းနိုင် / **C** Numbers မပါရ / **D** Method parameter မပို့ရ
2. `"  CSharp  ".Trim()` result? **A** unchanged / **B** `"CSharp"` / **C** `"CSHARP"` / **D** `"C Sharp"`
3. `"101,Aung,85".Split(',').Length`? **A** 1 / **B** 2 / **C** 3 / **D** 4
4. Case-insensitive technical comparison? **A** `OrdinalIgnoreCase` / **B** `ReferenceEquals` / **C** `Substring` / **D** `Split`
5. Loop မှာ report text အများကြီး ပေါင်းရေးဖို့? **A** `int` / **B** `HashSet<int>` / **C** `StringBuilder` / **D** `bool`

**Answer key:** 1-B, 2-B, 3-C, 4-A, 5-C.

## 9. Homework — Student Score Manager v6 (PENDING)

**Title:** Student Record Text Import. Day 1 Dictionary-based Student Manager ကို upgrade လုပ်ပါ။

### Required features

- `ID,Name,Score` text record ကို input လက်ခံပါ။
- `Split()` နဲ့ `Trim()` သုံးပြီး parse လုပ်ပါ။
- ID > 0, Name nonblank, Score 0–100 validate လုပ်ပါ။
- Duplicate Student ID ကို reject လုပ်ပါ။
- Student Names ကို case-insensitive search လုပ်ပါ။
- Student List report ကို `StringBuilder` နဲ့ generate လုပ်ပါ။

### Design hint

`Dictionary<int,int>` က Name မသိမ်းနိုင်တော့လို့ tuple value ကို စမ်းသုံးနိုင်တယ်။

```csharp
Dictionary<int, (string Name, int Score)> students = new();

students.TryAdd(101, ("Aung Aung", 85));
if (students.TryGetValue(101, out var student))
    Console.WriteLine($"{student.Name}: {student.Score}");
```

နောက်ပိုင်း OOP မှာ tuple အစား `Student` Class နဲ့ Model လုပ်မယ်။

### Suggested test checklist

- [ ] Valid record `101,Aung Aung,85` accepted
- [ ] Whitespace ` 102 , Su Su , 70 ` trimmed
- [ ] Duplicate ID rejected
- [ ] ID `0` rejected
- [ ] Score `0` accepted and classified as fail
- [ ] Score `101` rejected
- [ ] Blank name rejected
- [ ] Missing/extra comma fields rejected
- [ ] Name search works regardless of letter case
- [ ] Empty student report handled

**Homework status:** Pending — user plans to submit Day 1 and Day 2 assignments later.

## 10. Senior developer takeaways

- Raw input ကို data model ထဲ မထည့်ခင် **parse → normalize → validate** လုပ်ပါ။
- Strings immutable ဖြစ်တယ်; repeated large text building မှာ `StringBuilder` ကို စဉ်းစားပါ။
- `TryParse` pattern က invalid input ကို expected control flow အဖြစ် handle လုပ်နိုင်တယ်။
- Boundary values (`0`, `100`, `101`) နဲ့ malformed text ကို test လုပ်ပါ။
- `Split(',')` သုံးတာ full CSV parser မဟုတ်ဘူး။

**Next:** Week 2 Day 3 — Debugging, Exceptions & Error Handling.
