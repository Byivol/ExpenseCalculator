using System.Text.Json;

namespace ExpenseCalculator;
public class ExpenseCalculatorJson
{
    private static ExpenseCalculatorJson Instance;
    public static JsonSerializerOptions Options = new JsonSerializerOptions { WriteIndented = true };
    public string PathJson { get; set; } = "test.json";

    private ExpenseCalculatorJson(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new NullReferenceException("Value cannot be null.");
        else if (!filePath.EndsWith(".json"))
            throw new ArgumentException("PathJson is specified without an extension \".json\"");

        this.PathJson = filePath;
    }
    public static ExpenseCalculatorJson Initialization(string path)
    {
        if (Instance == null)
            Instance = new ExpenseCalculatorJson(path);
        return Instance;
    }
    public async Task ConvertExpenseJsonToCsvAsync(List<Expense> expenses, string filePath)
    {
        if (expenses.Count == 0) return;
        if (string.IsNullOrEmpty(filePath))
            throw new NullReferenceException("Value cannot be null.");
        else if (!filePath.EndsWith(".csv"))
            throw new ArgumentException("PathJson is specified without an extension \".json\"");
        try
        {
            using (var writer = new StreamWriter(filePath, append: true, encoding: System.Text.Encoding.UTF8))
            {
                await writer.WriteLineAsync("Category;Amount;Date");
                foreach (Expense expense in expenses)
                {
                    await writer.WriteLineAsync($"{expense.Category};{expense.Cost};{expense.DateTime:yyyy-MM-dd HH:mm:ss}");
                }
            }
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync($"Error: {ex.Message}");
        }
    }
    public async Task SaveToJsonAsync(Expense data)
    {
        try
        {
            List<Expense> existingData = await GetJsonDataAsync();
            existingData.Add(data);
            using (FileStream fs = new FileStream(PathJson, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                await JsonSerializer.SerializeAsync(fs, existingData, Options);
            }
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync($"Error: {ex.Message}");
        }
    }
    public async Task SaveToJsonAsync(IEnumerable<Expense> data)
    {
        try
        {
            List<Expense> existingData = await GetJsonDataAsync();
            existingData.AddRange(data); 
            using (FileStream fs = new FileStream(PathJson, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                await JsonSerializer.SerializeAsync(fs, existingData, Options);
            }
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync($"Error: {ex.Message}");
        }
    }
    public async Task<List<Expense>> GetJsonDataAsync()
    {
        try
        {
            using (FileStream fs = new FileStream(PathJson, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
            {
                
                return await JsonSerializer.DeserializeAsync<List<Expense>>(fs) ?? new List<Expense>();
            }
        }
        catch
        {
            return new List<Expense>();
        }
    }
    public async Task<List<Expense>> GetJsonDataFilterAsync(string category = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        try
        {
            if (string.IsNullOrEmpty(category) && !startDate.HasValue && !endDate.HasValue)
            {
                return await GetJsonDataAsync();
            }

            List<Expense> expenses = await GetJsonDataAsync();

            if (!string.IsNullOrEmpty(category))
            {
                expenses = expenses.Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (startDate.HasValue || endDate.HasValue)
            {
                expenses = expenses.Where(e =>
                    (!startDate.HasValue || e.DateTime >= startDate.Value) &&
                    (!endDate.HasValue || e.DateTime <= endDate.Value)).ToList();
            }

            return expenses;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync($"Error: {ex.Message}");
            return new List<Expense>();
        }
    }
    public async Task<double> GetTotalExpensesAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        try
        {
            List<Expense> expenses = await GetJsonDataFilterAsync(startDate: startDate, endDate: endDate);
            double total = expenses.Sum(e => e.Cost);
            return total;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync($"Error: {ex.Message}");
            return 0;
        }
    }
}
