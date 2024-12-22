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
