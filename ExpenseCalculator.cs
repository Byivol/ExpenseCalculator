using System.Text.Json;

namespace ExpenseCalculator;
public partial class ExpenseCalculator
{
    public static JsonSerializerOptions Options = new JsonSerializerOptions { WriteIndented = true };
    public string PathJson { get; set; } = "test.json";
    public ExpenseCalculator(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new NullReferenceException("Value cannot be null.");
        else if (!filePath.EndsWith(".json"))
            throw new ArgumentException("PathJson is specified without an extension \".json\"");
        this.PathJson = filePath;
    }
    public async Task SaveToJsonAsync(ExpenseClass data)
    {
        try
        {
            List<ExpenseClass> existingData = await GetJsonDataAsync();
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
    public async Task SaveToJsonAsync(IEnumerable<ExpenseClass> data)
    {
        try
        {
            List<ExpenseClass> existingData = await GetJsonDataAsync();
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
    public async Task<List<ExpenseClass>> GetJsonDataAsync()
    {
        try
        {
            using (FileStream fs = new FileStream(PathJson, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
            {
                
                return await JsonSerializer.DeserializeAsync<List<ExpenseClass>>(fs) ?? new List<ExpenseClass>();
            }
        }
        catch
        {
            return new List<ExpenseClass>();
        }
    }
    public async Task<List<ExpenseClass>> GetJsonDataFilterAsync(string? category = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        try
        {
            if (string.IsNullOrEmpty(category) && !startDate.HasValue && !endDate.HasValue)
            {
                return await GetJsonDataAsync();
            }

            List<ExpenseClass> expenses = await GetJsonDataAsync();

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
            return new List<ExpenseClass>();
        }
    }
    public async Task<double> GetTotalExpensesAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        try
        {
            List<ExpenseClass> expenses = await GetJsonDataFilterAsync(startDate: startDate, endDate: endDate);
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
