# Week 1 · Day 1 — Introduction to C# and .NET

> **Track:** .NET Full-Stack Developer · **Level:** Beginner  
> **Language:** မြန်မာဘာသာ + English technical terms  
> **Estimated study time:** 2 hours

## 🎯 Learning Objectives

ဒီ Lesson ပြီးတဲ့အခါ အောက်ပါအချက်တွေကို ရှင်းပြနိုင်၊ လက်တွေ့အသုံးချနိုင်ရပါမယ်။

- C# Programming Language နဲ့ .NET Platform ကို ခွဲခြားနိုင်ခြင်း
- CLR, IL, JIT, Garbage Collector တို့ရဲ့ အခြေခံတာဝန်ကို နားလည်ခြင်း
- .NET SDK နဲ့ .NET Runtime ကွာခြားချက်ကို သိခြင်း
- Console Application တစ်ခု Create, Build, Run လုပ်နိုင်ခြင်း
- Variables, Console I/O နဲ့ String Interpolation ကို စတင်အသုံးပြုနိုင်ခြင်း
- User Input ကို `TryParse()` နဲ့ စစ်ဆေးနိုင်ခြင်း (introductory challenge)

---

## 1. C# နဲ့ .NET ဆိုတာဘာလဲ?

**C# (C-Sharp)** က Microsoft ဖန်တီးထားတဲ့ strongly typed, general-purpose **Programming Language** ဖြစ်ပါတယ်။ **.NET** ကတော့ C# အပါအဝင် Supported Languages တွေနဲ့ Application တွေ Build/Run လုပ်နိုင်တဲ့ **Development Platform** ဖြစ်ပါတယ်။

| Term | အဓိပ္ပာယ် | ဥပမာ |
|---|---|---|
| C# | Programming Language | `Console.WriteLine("Hello");` |
| .NET | Development Platform | Console App, Web API, Desktop App |
| .NET SDK | Development Tools | `dotnet new`, `dotnet build` |
| .NET Runtime | Compiled .NET Apps တွေ Run ဖို့ လိုတဲ့ Components | Runtime Libraries, CLR |

### .NET Framework, .NET Core, Modern .NET

- **.NET Framework:** အဓိကအားဖြင့် Windows ကို Target လုပ်ထားတဲ့ အဟောင်း .NET Implementation ဖြစ်ပါတယ်။
- **.NET Core:** Cross-platform .NET ရဲ့ အစောပိုင်းမျိုးဆက်ဖြစ်ပါတယ်။
- **Modern .NET (.NET 5+):** .NET Core ကို ဆက်ခံထားတဲ့ Unified, Cross-platform Platform ဖြစ်ပါတယ်။

> **Remember:** C# က *Code ရေးတဲ့ Language* ဖြစ်ပြီး .NET က *Application Build/Run လုပ်ဖို့ Platform* ဖြစ်ပါတယ်။

## 2. C# Code ဘယ်လို Run သလဲ?

```text
C# Source Code (.cs)
        ↓ C# Compiler
Intermediate Language (IL)
        ↓ .NET Runtime / CLR
JIT Compilation (typically)
        ↓
Native Machine Code
        ↓
Program Execution
```

- **CLR (Common Language Runtime):** Managed Code Execution, Type Safety, Exception Handling နဲ့ Memory Management စတဲ့ Runtime Services တွေကို ပံ့ပိုးပေးပါတယ်။
- **IL (Intermediate Language):** C# Compiler က ပုံမှန်အားဖြင့် ထုတ်ပေးတဲ့ Intermediate Instructions ဖြစ်ပါတယ်။
- **JIT (Just-In-Time Compiler):** Execution အတွက် IL ကို Native Machine Code အဖြစ် Compile လုပ်ပေးပါတယ်။
- **GC (Garbage Collector):** အသုံးမပြုတော့တဲ့ Managed Objects တွေရဲ့ Memory ကို သင့်တော်တဲ့အချိန်မှာ Reclaim လုပ်ပေးပါတယ်။

> **Note:** .NET မှာ Ahead-of-Time (AOT) Compilation လည်း ရှိပါတယ်။ အပေါ်က Diagram က Beginner တွေအတွက် Typical JIT Execution Flow ဖြစ်ပါတယ်။ GC က Files, Database Connections လို External Resources တွေကို အလိုအလျောက် အချိန်မီ Dispose လုပ်ပေးမယ်လို့ မယူဆသင့်ပါ။

## 3. Development Environment Setup

1. [.NET SDK](https://dotnet.microsoft.com/download) ကို Install လုပ်ပါ။
2. Editor အဖြစ် Visual Studio, VS Code (C# tooling ပါဝင်ရန်လို), သို့မဟုတ် Rider ကို သုံးနိုင်ပါတယ်။
3. Terminal မှာ SDK ကို စစ်ဆေးပါ။

```bash
dotnet --version
```

### First Console App

```bash
dotnet new console -n MyFirstApp
cd MyFirstApp
dotnet run
```

အသုံးဝင်တဲ့ CLI Commands:

| Command | Purpose |
|---|---|
| `dotnet --version` | Installed SDK Version ကြည့်ရန် |
| `dotnet new console -n MyFirstApp` | Console Project အသစ် Create လုပ်ရန် |
| `dotnet build` | Project Compile/Build လုပ်ရန် |
| `dotnet run` | Application Run လုပ်ရန် |

## 4. First C# Program

`Program.cs`:

```csharp
string studentName = "Alex";
int studentAge = 20;

Console.WriteLine("Welcome to C# Programming!");
Console.WriteLine($"Name: {studentName}");
Console.WriteLine($"Age: {studentAge}");
```

**Expected Output**

```text
Welcome to C# Programming!
Name: Alex
Age: 20
```

### Code Walkthrough

| Syntax | Explanation |
|---|---|
| `string` | Text သိမ်းဆည်းတဲ့ Data Type |
| `int` | Whole Number သိမ်းဆည်းတဲ့ Data Type |
| `studentName` | Variable Name |
| `=` | Assignment Operator |
| `;` | Statement Terminator |
| `Console.WriteLine()` | Output ပြပြီး New Line ဆင်းရန် |
| `$"Name: {studentName}"` | String Interpolation |

## 5. Console Input/Output

- `Console.Write()` က Output ပြပေမဲ့ New Line မဆင်းပါ။
- `Console.WriteLine()` က Output ပြပြီး New Line ဆင်းပါတယ်။
- `Console.ReadLine()` က User Input ကို `string?` အဖြစ် ပြန်ပေးပါတယ် (`null` ဖြစ်နိုင်ပါတယ်)။

```csharp
Console.Write("Enter your name: ");
string name = Console.ReadLine() ?? "Unknown";
Console.WriteLine($"Hello, {name}!");
```

`??` က **Null-Coalescing Operator** ဖြစ်ပြီး ဘယ်ဘက် Value က `null` ဖြစ်မှ ညာဘက် Value ကို သုံးပါတယ်။ **Empty String (`""`) ကို မစစ်ပါ။**

### Integer Input Conversion

```csharp
Console.Write("Enter your age: ");
string ageInput = Console.ReadLine() ?? "";

if (int.TryParse(ageInput, out int age) && age >= 0)
{
    Console.WriteLine($"Age accepted: {age}");
}
else
{
    Console.WriteLine("Invalid age.");
}
```

- `int.Parse("24")` က Integer ပြောင်းပေးပေမဲ့ Invalid Input ဆို Exception ဖြစ်နိုင်ပါတယ်။
- `int.TryParse("24", out int age)` က Success/Failure ကို `bool` အဖြစ် ပြန်ပေးပါတယ်။
- `&&` က Conditions နှစ်ခုလုံး `true` ဖြစ်မှ `true` ဖြစ်ပါတယ်။

## 6. Practical Exercise — Developer Profile

**Task:** Name, Age, University, Career Goal တို့ကို Variables ထဲ သိမ်းပြီး Profile Output ထုတ်ပါ။

```csharp
string name = "Ye Min Aung";
int age = 24;
string university = "MIIT";
string careerGoal = ".NET Full-Stack Developer";

Console.WriteLine("====== MY DEVELOPER PROFILE ======");
Console.WriteLine($"Name: {name}");
Console.WriteLine($"Age: {age}");
Console.WriteLine($"University: {university}");
Console.WriteLine($"Career Goal: {careerGoal}");
Console.WriteLine("==================================");
```

**Practice Upgrade:** Hardcoded Values တွေအစား `Console.ReadLine()` နဲ့ Input လက်ခံပါ။

### Challenge — Valid Age ရတဲ့အထိ ပြန်မေးခြင်း

```csharp
while (true)
{
    Console.Write("Enter your age: ");
    string? input = Console.ReadLine();

    if (input is null)
    {
        Console.WriteLine("Input stream closed.");
        break;
    }

    if (int.TryParse(input, out int age) && age >= 0)
    {
        Console.WriteLine($"Age accepted: {age}");
        break;
    }

    Console.WriteLine("Invalid age. Try again.");
}
```

`while (true)` က Loop ကို ဆက်လည်စေပြီး `break` က Loop ထဲက ထွက်စေပါတယ်။

## 7. Common Mistakes

| Mistake | Why | Better Approach |
|---|---|---|
| `int.Parse(Console.ReadLine()!)` | Invalid Text ဆို Exception ဖြစ်နိုင် | `int.TryParse()` |
| `Console.ReadLine() ?? "0"` | `null` ကို Valid Score `0` အဖြစ် လက်ခံမိနိုင် | `null` ကို သီးခြား Handle လုပ်ရန် |
| `?? "Unknown"` နဲ့ Empty Name ကို Handle လုပ်တယ်လို့ ထင်ခြင်း | `??` က `null` ကိုပဲ စစ် | `string.IsNullOrWhiteSpace()` |
| `careerGoal` ကို `CareerGoal` လို့ Local Variable အမည်ပေးခြင်း | Local Variables အတွက် C# Convention မဟုတ် | `camelCase` |
| `.Net` လို့ Product Name ရေးခြင်း | Official Branding မကိုက်ညီ | `.NET` |

## 8. Quick Quiz

1. C# က Programming Language လား၊ Runtime လား?
2. CLR ရဲ့ အဓိကတာဝန်က ဘာလဲ?
3. JIT က ဘာကို Native Machine Code အဖြစ် Compile လုပ်လဲ?
4. `.NET SDK` နဲ့ `.NET Runtime` ဘာကွာလဲ?
5. `Console.Write()` နဲ့ `Console.WriteLine()` ဘာကွာလဲ?
6. `Console.ReadLine() ?? "0"` က Empty Input ကို `"0"` အဖြစ် ပြောင်းပေးသလား?

<details>
<summary>Show Answers</summary>

1. Programming Language။
2. Managed Code Execution နဲ့ Runtime Services တွေ ပံ့ပိုးပေးခြင်း။
3. IL (typical JIT execution)။
4. SDK က Develop/Build လုပ်ဖို့၊ Runtime က Apps Run ဖို့။
5. `WriteLine()` က New Line ဆင်းတယ်။
6. မပြောင်းပါ။ `??` က `null` ကိုပဲ Handle လုပ်ပါတယ်။

</details>

## 9. Key Takeaways

- **C# ≠ .NET:** Language နဲ့ Platform ကို ခွဲသိပါ။
- **SDK ≠ Runtime:** Build/Develop နဲ့ Run တာကို ခွဲသိပါ။
- **String Interpolation** က Readable Output အတွက် အသုံးဝင်ပါတယ်။
- **User Input** ကို Trust မလုပ်ဘဲ Validate လုပ်ပါ။
- **Readable Code** က Code တိုတိုရေးခြင်းထက် ပိုအရေးကြီးပါတယ်။

**Next:** [Week 1 · Day 2 — Variables, Data Types, Type Conversion & Memory Basics](week-01-day-02.md)
