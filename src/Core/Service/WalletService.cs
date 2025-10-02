using Core.Models;
using Core.Repository;

namespace Core.Service;

public class WalletService : IWalletService
{
    private readonly IRepositoryWallet _repositoryWallet;

    public WalletService(IRepositoryWallet repositoryWallet)
    {
        _repositoryWallet = repositoryWallet;
    }

    public void AddWallet(Wallet wallet)
    {
        _repositoryWallet.Add(wallet);
    }

    public void AddTransactionToWallet(Transaction transaction, int walletId)
    {
        _repositoryWallet.GetById(walletId)?.AddTransaction(transaction);
    }

    public IEnumerable<Transaction> GetTopExpenses(int year, int month, int topN = 3)
    {
        var topExpenses = new List<Transaction>();
        foreach (var wallet in _repositoryWallet.List())
        {
            topExpenses
                .AddRange(wallet.Transactions
                    .Where(t => t.Date.Year == year && t.Date.Month == month)
                    .Where(t => t.Type == TransactionType.Expense)
                    .OrderByDescending(t => t.Amount)
                    .Take(topN)
                    .ToList());
        }

        return topExpenses;
    }
}