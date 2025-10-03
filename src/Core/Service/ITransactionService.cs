using Core.Models;

namespace Core.Service;

public interface ITransactionService
{
    public void AddTransaction(int walletId, Transaction transaction);

    public void DeleteTransaction(int walletId, Transaction transaction);

    public void UpdateTransaction(int walletId, Transaction transaction);

    public Transaction GetTransactionById(int transactionId);

    public IEnumerable<Transaction> GetTransactions();

    public IEnumerable<Transaction> GetTransactionsInMonthWithSort(int year, int month);
}