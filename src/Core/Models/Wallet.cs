using Core.ResultType;

namespace Core.Models;

public class Wallet
{
    public int Id { get; init; }

    public string Name { get; init; }

    public string Currency { get; init; }

    public IEnumerable<Transaction> Transactions { get; private set; }

    public decimal InitialBalance { get; init; }

    public decimal CurrentBalance()
    {
        return InitialBalance
            + Transactions.Where(t => t.Type is TransactionType.Income).Sum(t => t.Amount)
            - Transactions.Where(t => t.Type is TransactionType.Expense).Sum(t => t.Amount);
    }

    public Wallet(int id, string name, string currency, decimal initialBalance, IEnumerable<Transaction> transactions)
    {
        Id = id;
        Name = name;
        Currency = currency;
        InitialBalance = initialBalance;
        Transactions = transactions;
    }

    public ResultAddTransaction AddTransaction(Transaction transaction)
    {
        if (transaction.Type == TransactionType.Expense && transaction.Amount > CurrentBalance())
            return new ResultAddTransaction.NotEnoughMoney();
        Transactions = Transactions.Append(transaction);
        return new ResultAddTransaction.Success();
    }
}