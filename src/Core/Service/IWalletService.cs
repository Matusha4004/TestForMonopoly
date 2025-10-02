using Core.Models;

namespace Core.Service;

public interface IWalletService
{
    public void AddWallet(Wallet wallet);

    public void AddTransactionToWallet(Transaction transaction, int walletId);

    public IEnumerable<Transaction> GetTopExpenses(int year, int month, int topN = 3);
}