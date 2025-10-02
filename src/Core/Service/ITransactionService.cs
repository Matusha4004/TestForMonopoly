using Core.Models;

namespace Core.Service;

public interface ITransactionService
{
    public void AddTransaction(int walletId, int id, DateTime transactionDate, decimal amount, TransactionType type, string description);

    public IEnumerable<Transaction> GetTransactionsInMonthWithSort(int year, int month);
}