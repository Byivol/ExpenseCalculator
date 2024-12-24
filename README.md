# ExpenseCalculator
AsyncJson ConvertToCsv.
Example CSV : https://1drv.ms/x/c/f3922317e9a5638e/EflfUNmfGBdPlhzvXxe0g64BJxpwNsIkwQrMkUvedeIRHQ?e=ywBTAI

![image](https://github.com/user-attachments/assets/344949c8-becb-427d-a1b8-2d6da798801f)
![image](https://github.com/user-attachments/assets/98642a32-6d24-4737-8976-c46139502c4f)
![image](https://github.com/user-attachments/assets/51451c27-1ee0-42ab-aacd-e003f9c3facb)


## Add
```C# 
await expenseCalculatorJson.SaveToJsonAsync(new Expense("Category", 1000, DateTime.Now.AddDays(-1)));
```
## AddRange
```C# 
await expenseCalculatorJson.SaveToJsonAsync(new Expense[]
{
    new Expense("Category",1000, DateTime.Now.AddDays(-1)),
    new Expense("Category",1500, DateTime.Now.AddDays(1)),
    new Expense("Category",3000, DateTime.Now.AddDays(2)),
    new Expense("Category1",1000, DateTime.Now.AddDays(-1)),
    new Expense("Category1",1500, DateTime.Now.AddDays(1)),
    new Expense("Category1",3000, DateTime.Now.AddDays(2)),
    new Expense("Category2",1000, DateTime.Now.AddDays(-1)),
    new Expense("Category2",1500, DateTime.Now.AddDays(1)),
    new Expense("Category2",3000, DateTime.Now.AddDays(2))
});
```
## GetJsonDataAsync
```C# 
var json = await expenseCalculatorJson.GetJsonDataAsync();
foreach (var expense in expenses)
{
    Console.WriteLine(expense);
}
```
## GetDoubleExpenses (Format StartDateTime EndDateTime)
```C# 
double totalExpenses = await expenseCalculatorJson.GetTotalExpensesAsync(DateTime.Now.AddHours(-1), DateTime.Now);
Console.WriteLine(totalExpenses);
```
## GetJsonFilterAsync
```C# 
List<Expense> expenses = await expenseCalculatorJson.GetJsonDataFilterAsync(category: "Category",startDate: DateTime.Now.AddDays(-1),endDate:DateTime.Now);
foreach (var expense in expenses)
{
    Console.WriteLine($"{expense.Category};{expense.Cost};{expense.DateTime:yyyy-MM-dd HH:mm:ss}");
}
```
## ConvertToCsv
```C# 
var json = await expenseCalculatorJson.GetJsonDataAsync();
expenseCalculatorJson.ConvertExpenseJsonToCsvAsync(json, "name.csv");
```

# Тестирование
Проводилось на i3-8100 3.6GHz DDR4 2666MHz
```C# 
using ExpenseCalculator;
using System.Diagnostics;
namespace ConsoleApp30
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ExpenseCalculator.ExpenseCalculator expenseCalculator = new("fl.json");

            List<ExpenseClass> list = new List<ExpenseClass>();

            for (int i = -10000; i < 10000; i++)
            {
                list.Add(new ExpenseClass($"Category{i}", i, dateTime: DateTime.Now.AddDays(i)));
            }
            Stopwatch sw = new Stopwatch();

            double total = 0;
            sw.Start();

            total = await expenseCalculator.GetTotalExpensesAsync(startDate: DateTime.Now.AddDays(-500), endDate: DateTime.Now.AddDays(500));
            
            sw.Stop();
            
            Console.WriteLine(total);

            Console.WriteLine("Стресс тестирование GetTotalExpensesAsync 20000 объектов " + sw.ElapsedMilliseconds +"мс");
        }
    }
}
```
![image](https://github.com/user-attachments/assets/526ace1a-c635-4b7c-beac-5b70a2c73604)
![Screenshot_4](https://github.com/user-attachments/assets/aee8d1ab-48fc-4c5e-9cc2-f3a6c1c1a841)
![Screenshot_3](https://github.com/user-attachments/assets/9e87bf34-214e-45a5-90e8-57e41db61af7)
![Screenshot_2](https://github.com/user-attachments/assets/53872cc0-4a15-4037-a2a6-47a402b9272a)
![Screenshot_1](https://github.com/user-attachments/assets/36ae9da9-fb3b-4c3d-b4dd-6f118e2bcd99)
![Screenshot_5](https://github.com/user-attachments/assets/bb6fcd84-d444-4b82-9305-4fa9327b5f92)

