namespace ExpenseCalculator.CSV;

public partial class ExpenseCalculator
{
    public async Task ConvertExpenseJsonToCsvAsync(List<ExpenseClass> expenses, string filePath)
    {
        if (expenses.Count == 0 && string.IsNullOrEmpty(filePath)) 
            throw new NullReferenceException("Parameter cannot be null.");
        else if (!filePath.EndsWith(".csv"))
            throw new ArgumentException("PathJson is specified without an extension \".json\"");
        try
        {
            using (var writer = new StreamWriter(filePath, append: true, encoding: System.Text.Encoding.UTF8))
            {
                string textTo = "";
                expenses.ForEach(expense =>
                {
                    textTo += $"{expense.Category};{expense.Cost};{expense.DateTime:yyyy-MM-dd HH:mm:ss}\n";
                });
                await writer.WriteLineAsync("Category;Amount;Date\n" + textTo);
            }
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync($"Error: {ex.Message}");
            throw;
        }
    }
}
