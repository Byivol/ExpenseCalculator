namespace ExpenseCalculator;

public class ExpenseClass
{
    public string Category { get; set; }
    public double Cost { get; set; }
    public DateTime DateTime { get; set; }
    public ExpenseClass(string Category,double cost,DateTime dateTime)
    {
        this.Category = Category; this.Cost = cost; this.DateTime = dateTime;
    }
    public override string ToString()
    {
        return $"Category: {this.Category}, Cost: {this.Cost}, Date: {this.DateTime:yyyy-MM-dd HH:mm:ss}";
    }
}