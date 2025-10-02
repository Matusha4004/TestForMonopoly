namespace Core.Models;

public class Transaction
{
    public int Id { get; }

    public DateTime Date { get; }

    public decimal Amount { get; }

    public TransactionType Type { get; }

    public string Description { get; }

    public Transaction(int id, DateTime date, decimal amount, TransactionType type, string description)
    {
        Id = id;
        Date = date;
        Amount = amount;
        Type = type;
        Description = description;
    }
}