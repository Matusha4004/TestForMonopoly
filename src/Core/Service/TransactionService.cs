using Core.Models;
using Core.Repository;
using Core.ResultType;

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

    public void AddTransaction(int walletId, Transaction transaction)
    {
        if (_walletService.AddTransactionToWallet(transaction, walletId) is ResultAddTransaction.Success)
        {
            _repository.Add(transaction);
        }
        else
        {
            throw new Exception("Failed to add transaction");
        }
    }

    public void DeleteTransaction(int walletId, Transaction transaction)
    {
        _repository.Delete(transaction);
        _walletService.DeleteTransaction(transaction, walletId);
    }

    public void UpdateTransaction(int walletId, Transaction transaction)
    {
        _repository.Update(transaction);
    }

    public Transaction GetTransactionById(int transactionId)
    {
       return _repository.GetById(transactionId) ?? throw new KeyNotFoundException();
    }

    public IEnumerable<Transaction> GetTransactions()
    {
        return _repository.List();
    }

    public IEnumerable<Transaction> GetTransactionsInMonthWithSort(int year, int month)
    {
        var transactions = _repository
            .List()
            .Where(t => t.Date.Year == year && t.Date.Month == month)
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