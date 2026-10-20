# Week 1 — Day 4: Loops, Iteration & Collections Basics

> **Track:** .NET Full-Stack Developer Roadmap · Week 1 / Day 4  
> **Language:** မြန်မာ + English Technical Terms  
> **Duration:** ~2 hours  
> **Project:** Student Score Management System v1 → v3  
> **Status:** Homework PASSED (v3: 9.5/10, static code review)

## Learning Objectives

- `while`, `do-while`, `for`, `foreach` ကို သင့်တော်တဲ့နေရာမှာ အသုံးပြုနိုင်ရန်။
- `break`, `continue`, `return` ရဲ့ Control Flow ကို ခွဲခြားနိုင်ရန်။
- Array နဲ့ `List<T>` ရဲ့ Fixed-size / Dynamic-size ကွာခြားချက်ကို နားလည်ရန်။
- Index, `Length`, `Count`, `Add`, `Remove`, `RemoveAt` အသုံးပြုရန်။
- Collection ထဲက Total, Average, Min, Max, Pass/Fail Count ကို Loops ဖြင့် တွက်ရန်။
- Empty Collections, Invalid Inputs နဲ့ Boundary Values ကို စစ်ဆေးရန်။

---

## 1. Why Loops?

Loop ဆိုတာ Condition တစ်ခု သို့မဟုတ် Collection တစ်ခုအပေါ်မူတည်ပြီး Code Block ကို ထပ်ခါထပ်ခါ Execute လုပ်ပေးတဲ့ Control Structure ဖြစ်တယ်။ Repetitive Statements ကို Copy/Paste မလုပ်ရအောင် ကူညီတယ်။

### `while` — condition ကို အရင်စစ်တယ်

```csharp
int count = 1;
while (count <= 5)
{
    Console.WriteLine(count);
    count++;
}
```

- Condition `false` ဖြစ်နေကတည်းက Body ကို **တစ်ကြိမ်မှ မလုပ်နိုင်**။
- `count++` မရှိရင် Infinite Loop ဖြစ်နိုင်တယ်။
- Menu နဲ့ Input Validation အတွက် သင့်တော်တယ်။

### `do-while` — body ကို အရင်လုပ်တယ်

```csharp
int count = 1;
do
{
    Console.WriteLine(count);
    count++;
} while (count <= 5);
```

- Condition `false` ဖြစ်ရင်တောင် Body ကို **အနည်းဆုံး တစ်ကြိမ်** Execute လုပ်တယ်။
- အဆုံးမှာ Semicolon `;` ပါတယ်။

### `for` — iteration count / index ကို ထိန်းချုပ်တယ်

```csharp
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"Index: {i}");
}
```

`initialization → condition → body → update` အစီအစဉ်နဲ့ အလုပ်လုပ်တယ်။ Index နဲ့ Item Number ကို လိုချင်တဲ့အခါ သင့်တော်တယ်။

### `foreach` — collection item တစ်ခုချင်းစီကို ဖတ်တယ်

```csharp
int[] scores = { 85, 70, 92 };
foreach (int score in scores)
{
    Console.WriteLine(score);
}
```

Item တွေကို Traverse လုပ်ဖို့ လွယ်တယ်။ Index မလိုတဲ့ Read-only Traversal အတွက် သင့်တော်တယ်။ `foreach` လည်နေစဉ် `List<T>` ကို `Add()` / `RemoveAt()` ဖြင့် Structural Modification လုပ်ရင် `InvalidOperationException` ဖြစ်နိုင်တယ်။

### `break`, `continue`, `return`

```csharp
for (int i = 1; i <= 5; i++)
{
    if (i == 2) continue; // current iteration ကို ကျော်
    if (i == 4) break;    // loop ကို အဆုံးသတ်
    Console.WriteLine(i); // 1, 3
}
```

- `break`: အနီးဆုံး Loop (သို့) `switch` ကို Exit လုပ်တယ်။
- `continue`: Current Iteration ရဲ့ ကျန် Statements ကို Skip ပြီး Next Iteration သွားတယ်။
- `return`: Method ကို Exit လုပ်တယ်; Main top-level program မှာလည်း Exit လုပ်နိုင်တယ်။

### Nested Loops

```csharp
for (int row = 1; row <= 3; row++)
{
    for (int col = 1; col <= 3; col++)
    {
        Console.Write($"{row * col,4}");
    }
    Console.WriteLine();
}
```

Inner Loop က Outer Loop တစ်ကြိမ်စီအတွက် အပြည့်လည်တယ်။ 3×3 မှာ Inner Body 9 ကြိမ် Execute ဖြစ်တယ်။

---

## 2. Arrays vs `List<T>`

### Array — fixed length

```csharp
int[] scores = { 85, 70, 92 };
Console.WriteLine(scores[0]);      // 85
Console.WriteLine(scores.Length);  // 3
scores[1] = 75;
```

Index က `0` ကနေ `Length - 1` အထိပဲ Valid ဖြစ်တယ်။ `scores[3]` က `IndexOutOfRangeException` ဖြစ်တယ်။ Array ကို Create လုပ်ပြီးနောက် Length မပြောင်းနိုင်ဘူး။

### List — dynamic size

```csharp
List<int> scores = new();
scores.Add(85);
scores.Add(70);
scores.Add(92);
Console.WriteLine(scores.Count); // 3
scores.RemoveAt(1);              // index 1 ကို ဖယ် → [85, 92]
scores.Remove(85);               // value 85 ရဲ့ first occurrence ကို ဖယ်
```

| Concept | Array | `List<T>` |
|---|---|---|
| Size | Fixed | Dynamic |
| Number of items | `.Length` | `.Count` |
| Read item | `items[index]` | `items[index]` |
| Add/remove | Direct resizing မရှိ | `Add`, `Remove`, `RemoveAt` |
| Invalid index | `IndexOutOfRangeException` | `ArgumentOutOfRangeException` |

**Important:** `Remove(value)` နဲ့ `RemoveAt(index)` မတူဘူး။ User က “Score Number 2” ကို ဖျက်မယ်ဆို `RemoveAt(2 - 1)` ကို သုံးရမယ်။

---

## 3. Collection Statistics Using Loops

```csharp
List<int> scores = new() { 85, 70, 92, 45, 60 };

if (scores.Count == 0)
{
    Console.WriteLine("No scores available.");
    return;
}

long total = 0;
int maxScore = scores[0];
int minScore = scores[0];
int passCount = 0;
int failCount = 0;

foreach (int score in scores)
{
    total += score;
    if (score > maxScore) maxScore = score;
    if (score < minScore) minScore = score;

    if (score >= 50) passCount++;
    else failCount++;
}

double average = (double)total / scores.Count;
Console.WriteLine($"Total: {total}");
Console.WriteLine($"Average: {average:F2}");
Console.WriteLine($"Max: {maxScore}; Min: {minScore}");
Console.WriteLine($"Pass: {passCount}; Fail: {failCount}");
```

**Expected:** Total `352`, Average `70.40`, Max `92`, Min `45`, Pass `4`, Fail `1`.

### Why these details matter

1. `scores.Count == 0` ကို အရင်စစ်ရမယ် — `scores[0]` နဲ့ Division by Zero ကို ကာကွယ်တယ်။
2. `maxScore` / `minScore` ကို First Element နဲ့ Initialize လုပ်တယ် — arbitrary `0` နဲ့ စမထားဘူး။
3. `(double)` Casting သုံးတယ် — Integer Division မဖြစ်စေဘူး။
4. `long total` သုံးတယ် — `int` ထက် ပိုကြီးတဲ့ Sum ကို Handle လုပ်နိုင်တယ်။
5. **Pass condition က `score >= 50`** ဖြစ်တယ်; `score > 50` ဆို Score `50` ကို Fail လို့ မှားတွက်မယ်။
6. `total >= 50` က Individual Pass Count မဟုတ်ဘူး — Business Requirement ကို မှန်ကန်စွာ ဖတ်ရမယ်။
7. Loop တစ်ခုတည်းမှာ Statistics အားလုံးတွက်တာ O(n) Single-Pass Traversal ဖြစ်တယ်။

---

## 4. Input Validation Pattern

```csharp
static int ReadScore()
{
    while (true)
    {
        Console.Write("Enter score (0–100): ");
        string? input = Console.ReadLine();

        if (input is null)
            throw new EndOfStreamException("Input stream closed.");

        if (int.TryParse(input, out int score)
            && score >= 0 && score <= 100)
        {
            return score;
        }

        Console.WriteLine("Enter a valid score (0–100).");
    }
}
```

- `TryParse` က Invalid Text အတွက် Exception မပစ်ဘဲ `false` ပြန်ပေးတယ်။
- `0` နဲ့ `100` ကို Accept လုပ်ရမယ်။
- `Console.ReadLine()` က `null` ပြန်ရင် EOF ဖြစ်နိုင်တယ်။ `throw` လုပ်ထားရင် Caller က Catch မလုပ်သေးပါက Unhandled Exception နဲ့ Terminate ဖြစ်နိုင်တယ် — ဒီ Version ရဲ့ Known Limitation ပါ။

---

## 5. Homework: Student Score Management System v1 → v3

### Menu Requirements

```text
===== STUDENT SCORE MANAGER =====
1. Add Score
2. View All Scores
3. Calculate Statistics
4. Remove Score
5. Exit
```

- **Add Score:** `0–100` Integer Input ကို Validate ပြီး `scores.Add(score)` လုပ်ပါ။
- **View Scores:** Empty List ကို Handle; `1. Score: 85` လို 1-based Display လုပ်ပါ။
- **Statistics:** Total, Average, Max, Min, Pass Count, Fail Count ပြပါ။
- **Remove:** 1-based Score Number ကို Validate ပြီး `scores.RemoveAt(scoreNumber - 1)` လုပ်ပါ။
- **Exit:** Menu Loop ကို Exit လုပ်ပါ။

### Recommended Method Responsibilities

```csharp
static int ReadScore() { /* validation */ }
static void ViewScores(List<int> scores) { /* display */ }
static void CalculateStatistics(List<int> scores) { /* compute + display in Day 4 */ }
static void RemoveScore(List<int> scores) { /* remove by number */ }
```

### Lessons from the actual code review

- **v1 — 8/10:** `result = total >= 50 ? "Pass" : "Fail"` က Wrong Metric ဖြစ်တယ်။ Requirement က Pass/Fail **Count** ဖြစ်တယ်။
- **v2 — 9/10:** `passCount`, `failCount` ထည့်ပြီး `ViewScores(scores)` Reuse လုပ်ထားတယ်။ ဒါပေမဲ့ `score > 50` ကြောင့် Boundary `50` ကို Fail လို့ မှားတွက်ခဲ့တယ်။
- **v3 — 9.5/10, PASSED:** `score >= 50`, `long total`, `{average:F2}`, Separate Variable Declarations, `ViewScores()` Reuse — အဓိက Requirements ပြည့်စုံသွားတယ်။
- **Future Improvements:** `ReadScore()` / `RemoveScore()` EOF ကို Graceful Handle, Remove Cancel Option, Confirmation Messages, More Descriptive Naming.

---

## 6. Practical Exercises

### Exercise A — Multiplication Table

`for` Loop သုံးပြီး Input Number တစ်ခုအတွက် 1–10 Multiplication Table ပြပါ။

### Exercise B — Even Number Counter

`1–100` အတွင်း Even Numbers ဘယ်နှခုရှိသလဲ Count လုပ်ပါ။ `%` Operator နဲ့ `if` သုံးပါ။ **Expected: 50**.

### Exercise C — Score Analyzer

`[85, 70, 92, 45, 60]` ကို `foreach` သုံးပြီး Total, Average, Max, Min, Pass/Fail Count တွက်ပါ။ LINQ ကို မသုံးသေးပါနဲ့။

### Exercise D — Remove by Number

`[80, 90, 70]` မှာ User က `2` ရွေးရင် `[80, 70]` ကျန်ရမယ်။ `0`, `4`, `abc` ကို Reject လုပ်ပါ။

---

## 7. Quick Quiz

1. `while` နဲ့ `do-while` ဘာကွာသလဲ?
2. `break` နဲ့ `continue` ဘာကွာသလဲ?
3. Array ရဲ့ Item Count ကို ဘာနဲ့ ဖတ်သလဲ? `List<T>` မှာရော?
4. `Remove(2)` နဲ့ `RemoveAt(2)` ဘာကွာသလဲ?
5. `score >= 50` အတွက် `score = 50` က Pass လား Fail လား?
6. Empty List မှာ `scores[0]` ဖတ်ရင် ဘာဖြစ်မလဲ?

<details>
<summary>Quiz Answers</summary>

1. `while` က Condition အရင်စစ်; `do-while` က Body အနည်းဆုံး တစ်ကြိမ်လုပ်။
2. `break` က Loop Exit; `continue` က Current Iteration Skip။
3. Array: `.Length`; List: `.Count`။
4. `Remove(2)` က Value `2` ကို ဖယ်; `RemoveAt(2)` က Index `2` မှ Item ကို ဖယ်။
5. Pass။
6. `List<T>` အတွက် `ArgumentOutOfRangeException` ဖြစ်နိုင်တယ်။

</details>

## 8. Test Checklist

- [ ] Empty List: View/Statistics/Remove မှာ “No scores available.”
- [ ] Add `0`, `50`, `100`: Accepted
- [ ] Add `-1`, `101`, `abc`: Rejected
- [ ] `[85,70,92,45,60]`: Total 352; Avg 70.40; Pass 4; Fail 1
- [ ] `[49,50,51]`: Pass 2; Fail 1
- [ ] Remove Number 2 from `[80,90,70]`: `[80,70]`
- [ ] Remove Invalid Number 0 or 4: Rejected
- [ ] Exit Menu: Program terminates normally

## Key Takeaways

> **A program can run without errors and still produce logically incorrect results.**

- `for` ကို Index လိုတဲ့အခါ၊ `foreach` ကို Collection Traversal အတွက်၊ `while` ကို Menu/Input Validation အတွက် အသုံးပြုပါ။
- Boundary Values (`0`, `49`, `50`, `100`) ကို Test လုပ်ပါ။
- Empty Collection ကို အရင် Guard လုပ်ပါ။
- Reusable Methods နဲ့ DRY Principle ကို လေ့ကျင့်ပါ။

**Next:** [Week 1 — Day 5: Methods, Parameters, Return Values & Scope](week-01-day-05.md)
