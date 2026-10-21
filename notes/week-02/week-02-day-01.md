# Week 2 — Day 1: Collections & Defensive Programming

> **Course:** .NET Full-Stack Developer Roadmap (20 Weeks / 200 Hours)  
> **Lesson:** 06 | **Duration:** ~2 hours  
> **Status:** Lesson delivered; Student Score Manager v5 homework **pending**  
> **Prerequisites:** Week 1 — loops, `List<T>`, methods, `out`, `TryParse`, scope

## Learning objectives

- `List<T>`, `Dictionary<TKey,TValue>`, and `HashSet<T>` တို့ရဲ့ အသုံးဝင်ပုံကို ခွဲခြားနိုင်ရန်။
- Dictionary ထဲမှာ Key/Value pairs ထည့်ခြင်း၊ ရှာခြင်း၊ ပြင်ခြင်း၊ ဖျက်ခြင်း လုပ်နိုင်ရန်။
- `TryGetValue`, `TryAdd`, `TryParse` နဲ့ defensive input validation သုံးနိုင်ရန်။
- Empty collection, duplicate key, missing key, index boundary စတဲ့ edge cases ကို စဉ်းစားနိုင်ရန်။
- Student Score Manager v4 (`List<int>`) ကို v5 (`Dictionary<int,int>`) အဖြစ် တိုးချဲ့ရန်။

## 1. Why multiple collection types?

Week 1 မှာ score တွေကို ဒီလိုသိမ်းခဲ့တယ်။

```csharp
List<int> scores = new() { 85, 70, 45 };
```

ဒါပေမဲ့ `101 → 85`, `102 → 70`, `103 → 45` လို Student ID နဲ့ Score ကို တွဲသိမ်းဖို့ဆို Dictionary ပိုသင့်တော်တယ်။

| Collection | Purpose | Duplicates | Access |
|---|---|---|---|
| `List<T>` | Ordered sequence | Values can repeat | Zero-based index |
| `Dictionary<TKey,TValue>` | Key → Value mapping | Keys unique; values may repeat | By key |
| `HashSet<T>` | Unique elements | Duplicate values ignored | Membership lookup; no index |

> **Design rule:** Data Structure ကို requirement ပေါ်မူတည်ပြီး ရွေးပါ။

## 2. `List<T>` refresher

```csharp
List<int> scores = new() { 85, 70, 45 };
scores.Add(90);               // [85, 70, 45, 90]
scores.Remove(70);            // remove first matching VALUE
scores.RemoveAt(0);           // remove item at INDEX 0
bool exists = scores.Contains(45);
Console.WriteLine(scores.Count);
```

**`Remove` vs `RemoveAt`:** `Remove(70)` က value `70` ကို ဖယ်တယ်။ `RemoveAt(1)` က index `1` မှ item ကို ဖယ်တယ်။

```csharp
List<int> values = new() { 10, 20, 30 };
values.Remove(20);    // [10, 30]
values.RemoveAt(0);   // [30]
```

Index access မလုပ်ခင် `0 <= index && index < scores.Count` ကို စစ်ပါ။ `scores[scores.Count]` က invalid ဖြစ်တယ်။

## 3. `Dictionary<TKey,TValue>` fundamentals

```csharp
Dictionary<int, int> studentScores = new();
studentScores.Add(101, 85);
studentScores.Add(102, 70);
studentScores.Add(103, 45);

Console.WriteLine(studentScores[101]); // 85
```

- `int` (first generic argument) = **Key** = Student ID
- `int` (second generic argument) = **Value** = Score
- Dictionary keys are unique.
- `studentScores[999]` throws `KeyNotFoundException` if key `999` does not exist.

### Safe lookup — `TryGetValue`

```csharp
if (studentScores.TryGetValue(101, out int score))
{
    Console.WriteLine($"Score: {score}");
}
else
{
    Console.WriteLine("Student not found.");
}
```

`out` parameter ကို Week 1 Day 5 မှာ သင်ခဲ့တယ်။ Method က `bool` ပြန်ပြီး result ကို `out score` နဲ့ ပေးတယ်။

### Safe insertion — `TryAdd`

```csharp
if (studentScores.TryAdd(104, 90))
    Console.WriteLine("Student added.");
else
    Console.WriteLine("Student ID already exists.");
```

`Add` နဲ့ duplicate key ထည့်ရင် exception ဖြစ်တယ်။ `TryAdd` က duplicate key ဖြစ်ရင် `false` ပြန်တယ်။

### Update and remove

```csharp
if (studentScores.ContainsKey(101))
    studentScores[101] = 95; // update existing student

if (studentScores.Remove(103))
    Console.WriteLine("Removed.");
else
    Console.WriteLine("Student not found.");
```

`studentScores[id] = score` က key မရှိရင် new entry လည်း ဖန်တီးနိုင်တာကြောင့် **update-only** operation အတွက် `ContainsKey` သို့မဟုတ် `TryGetValue` နဲ့ အရင်စစ်ပါ။

### Iterate dictionary

```csharp
foreach (KeyValuePair<int, int> student in studentScores)
{
    Console.WriteLine($"ID: {student.Key}, Score: {student.Value}");
}
```

Dictionary iteration order ကို application requirement အတွက် အာမမခံထားပါနဲ့။ Sorted output လိုရင် sorting သီးခြား လုပ်ပါ။

## 4. `HashSet<T>` — unique values

```csharp
HashSet<int> studentIds = new();
studentIds.Add(101);
studentIds.Add(102);
bool addedAgain = studentIds.Add(101); // false
Console.WriteLine(studentIds.Count);   // 2
Console.WriteLine(studentIds.Contains(102)); // True
```

`HashSet<T>` မှာ indexer မရှိဘူး။ Dictionary key တွေက unique ဖြစ်ပြီးသားမို့ Student ID uniqueness အတွက် HashSet တစ်ခု သီးခြားထပ်မလိုပါ။

## 5. Defensive programming and null safety

```csharp
string? input = Console.ReadLine();
if (input is null)
{
    Console.WriteLine("Input closed.");
    return;
}

if (!int.TryParse(input, out int score))
{
    Console.WriteLine("Enter an integer.");
    return;
}

if (score < 0 || score > 100)
{
    Console.WriteLine("Score must be 0–100.");
    return;
}
```

**Input validation** = text ကို number အဖြစ် parse လုပ်နိုင်/မနိုင်။  
**Business validation** = parsed number က business rule (score `0–100`) ကို လိုက်နာ/မလိုက်နာ။

Boundary tests: `-1` invalid, `0` valid, `50` pass, `100` valid, `101` invalid. **`score <= 0` လို့ မရေးပါနဲ့ — `0` က valid F score ပါ။**

## 6. Complete mini-program: Dictionary-based Student Manager

> ဒီ Example က Add, Find, View ကို ပြတယ်။ Homework မှာ Update, Remove, Statistics ကို ကိုယ်တိုင်ဖြည့်ရမယ်။

```csharp
Dictionary<int, int> studentScores = new();

while (true)
{
    Console.WriteLine("\n===== STUDENT MANAGER =====");
    Console.WriteLine("1. Add Student");
    Console.WriteLine("2. Find Student");
    Console.WriteLine("3. View All Students");
    Console.WriteLine("4. Exit");
    Console.Write("Choose: ");

    string? input = Console.ReadLine();
    if (input is null) { Console.WriteLine("Input closed."); return; }
    if (!int.TryParse(input, out int option))
    {
        Console.WriteLine("Invalid option.");
        continue;
    }

    switch (option)
    {
        case 1: AddStudent(studentScores); break;
        case 2: FindStudent(studentScores); break;
        case 3: ViewStudents(studentScores); break;
        case 4: Console.WriteLine("Goodbye!"); return;
        default: Console.WriteLine("Invalid option."); break;
    }
}

static void AddStudent(Dictionary<int, int> students)
{
    int id = ReadNumber("Student ID: ", 1, int.MaxValue);
    int score = ReadNumber("Score: ", 0, 100);

    if (students.TryAdd(id, score))
        Console.WriteLine("Student added successfully.");
    else
        Console.WriteLine("Student ID already exists.");
}

static void FindStudent(Dictionary<int, int> students)
{
    int id = ReadNumber("Enter Student ID: ", 1, int.MaxValue);
    if (students.TryGetValue(id, out int score))
        Console.WriteLine($"Student {id}: {score}");
    else
        Console.WriteLine("Student not found.");
}

static void ViewStudents(Dictionary<int, int> students)
{
    if (students.Count == 0)
    {
        Console.WriteLine("No students available.");
        return;
    }

    foreach (KeyValuePair<int, int> student in students)
        Console.WriteLine($"ID: {student.Key}, Score: {student.Value}");
}

static int ReadNumber(string prompt, int min, int max)
{
    while (true)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        if (input is null)
            throw new EndOfStreamException("Input stream closed.");

        if (int.TryParse(input, out int number)
            && number >= min && number <= max)
            return number;

        Console.WriteLine($"Enter a valid number ({min}-{max}).");
    }
}
```

**Walkthrough:** `ReadNumber` က reusable validation method ဖြစ်တယ်။ `TryAdd` က duplicate ID ကို ကာကွယ်တယ်။ `TryGetValue` က missing ID exception ကို ရှောင်တယ်။ `foreach` က key/value pair တွေကို ထုတ်ပြတယ်။ ဒီ lesson example မှာ nested input EOF ကို `EndOfStreamException` ပစ်ထားတယ်; graceful exception handling ကို နောက် lesson မှာ သင်မယ်။

## 7. Practice exercises

1. `List<int> numbers = new() { 10, 20, 30 }; numbers.Add(40); numbers.Remove(20); numbers.RemoveAt(0);` ပြီးရင် `Count` နဲ့ `numbers[0]` ကို ခန့်မှန်းပါ။
2. `Dictionary<int,int>` ထဲက ID `103` ကို `TryGetValue` နဲ့ safe lookup လုပ်ပါ။
3. `[101, 102, 101, 103, 102]` ကို `HashSet<int>` ထဲသိမ်းပြီး unique count ကို print လုပ်ပါ။

<details><summary>Exercise answers (ကိုယ်တိုင်ဖြေပြီးမှ ဖွင့်ပါ)</summary>

1. Remaining `[30, 40]`; `Count = 2`, `numbers[0] = 30`.
2. `if (scores.TryGetValue(103, out int score)) Console.WriteLine(score); else Console.WriteLine("Not found");`
3. `new HashSet<int>(ids).Count` → `3`.

</details>

## 8. Quick quiz (5 questions)

1. Student ID → Score lookup အတွက် ဘယ် collection ကို သုံးမလဲ? **A** List / **B** Dictionary / **C** Array / **D** String
2. Dictionary indexer နဲ့ missing key access လုပ်ရင်? **A** 0 / **B** null / **C** KeyNotFoundException / **D** false
3. `HashSet<int>` ထဲ `10, 20, 10` ထည့်ရင် Count? **A** 1 / **B** 2 / **C** 3 / **D** 0
4. Score 0 ကို valid အဖြစ် လက်ခံဖို့ invalid lower-bound check က? **A** `score <= 0` / **B** `score < 0` / **C** `score == 0` / **D** `score >= 0`
5. `TryGetValue` ရဲ့ အဓိကရည်ရွယ်ချက်? **A** Sort / **B** Remove / **C** Safe lookup / **D** Allow duplicate keys

**Answer key:** 1-B, 2-C, 3-B, 4-B, 5-C.

## 9. Homework — Student Score Manager v5 (PENDING)

**Goal:** Week 1 v4 ရဲ့ `List<int>` ကို `Dictionary<int,int>` အဖြစ် Upgrade လုပ်ပါ။

| Menu | Feature | Required behavior |
|---|---|---|
| 1 | Add Student | Positive ID, score 0–100, reject duplicates |
| 2 | View Students | Print ID + Score |
| 3 | Find Student | Lookup by ID; handle not found |
| 4 | Update Score | Update only if ID exists |
| 5 | Remove Student | Remove by ID; handle not found |
| 6 | Statistics | Average, highest, lowest, pass/fail |
| 7 | Exit | End program |

### Required method signatures

```csharp
static void ShowMenu();
static int ReadNumber(string prompt, int min, int max);
static void AddStudent(Dictionary<int, int> students);
static void ViewStudents(Dictionary<int, int> students);
static void FindStudent(Dictionary<int, int> students);
static void UpdateScore(Dictionary<int, int> students);
static void RemoveStudent(Dictionary<int, int> students);
static void ShowStatistics(Dictionary<int, int> students);
```

### Expected statistics test

Input records: `101→85`, `102→70`, `103→45`, `104→0`.

```text
Total Students: 4
Total Score: 200
Average Score: 50.00
Highest Score: 85
Lowest Score: 0
Pass Count: 2
Fail Count: 2
```

### Starter switch

```csharp
Dictionary<int, int> students = new();
while (true)
{
    ShowMenu();
    string? input = Console.ReadLine();
    if (input is null) return;
    if (!int.TryParse(input, out int option)) continue;

    switch (option)
    {
        case 1: AddStudent(students); break;
        case 2: ViewStudents(students); break;
        case 3: FindStudent(students); break;
        case 4: UpdateScore(students); break;
        case 5: RemoveStudent(students); break;
        case 6: ShowStatistics(students); break;
        case 7: return;
        default: Console.WriteLine("Invalid option."); break;
    }
}
// TODO: Implement required methods.
```

### Rubric

| Category | Marks |
|---|---:|
| Dictionary usage | 20 |
| Method design / SRP | 20 |
| Input validation | 20 |
| CRUD operations | 20 |
| Statistics and edge cases | 15 |
| Readability | 5 |
| **Total** | **100** |

Passing score: **70/100**. Homework is **not yet submitted**.

## 10. Senior developer takeaways

- Choose collections by the operations the application needs.
- Prefer `TryGetValue` for uncertain keys and `TryAdd` for duplicate-safe insertion.
- Separate parsing from business rules.
- Always test empty data, missing IDs, duplicate IDs, and boundary scores.
- Avoid hardcoded average divisors: use the actual collection count.

**Next:** Week 2 Day 2 — Strings, StringBuilder & Data Processing.
