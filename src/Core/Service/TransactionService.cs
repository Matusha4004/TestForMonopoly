using Core.Models;
using Core.Repository;

namespace Core.Service;

public class TransactionService : ITransactionService
{
    private readonly IRepositoryTransaction _repository;
    private readonly IWalletService _walletService;

    public TransactionService(IRepositoryTransaction repository, IWalletService walletService)
    {
        _repository = repository;
        _walletService = walletService;
    }

    public void AddTransaction(int walletId, int id, DateTime transactionDate, decimal amount, TransactionType type, string description)
    {
        var newTransaction = new Transaction(id, transactionDate, amount, type, description);
        _repository.Add(newTransaction);
        _walletService.AddTransactionToWallet(newTransaction, walletId);
    }

    public IEnumerable<Transaction> GetTransactionsInMonthWithSort(int year, int month)
    {
        var transactions = _repository
            .GetTransactionsInMonth(year, month)
            .ToList();

        var incomeTransactions = transactions
            .Where(t => t.Type == TransactionType.Income)
            .OrderByDescending(t => t.Date)
            .ToList();

        var expenseTransactions = transactions
            .Where(t => t.Type == TransactionType.Expense)
            .OrderByDescending(t => t.Date)
            .ToList();

        if (incomeTransactions.Sum(t => t.Amount) >= expenseTransactions.Sum(t => t.Amount))
        {
            return incomeTransactions.Concat(expenseTransactions);
        }

        return expenseTransactions.Concat(incomeTransactions);
    }
}