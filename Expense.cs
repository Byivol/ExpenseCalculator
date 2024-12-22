namespace ExpenseCalculator;
public class Expense
{
    public string Category { get; set; }
    public double Cost { get; set; }
    public DateTime DateTime { get; set; }
    public Expense(string Category,double cost,DateTime dateTime)
    {
        this.Category = Category; this.Cost = cost; this.DateTime = dateTime;
    }
    public override string ToString()
    {
        return $"Category: {this.Category}, Cost: {this.Cost}, Date: {this.DateTime:yyyy-MM-dd HH:mm:ss}";
    }
}