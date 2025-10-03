using Core.Models;
using Core.ResultType;

namespace Core.Service;

public interface IWalletService
{
    public void AddWallet(Wallet wallet);

    public ResultAddTransaction AddTransactionToWallet(Transaction transaction, int walletId);

    public void DeleteWallet(Wallet wallet);

    public void ChangeWallet(Wallet wallet);

    public void DeleteTransaction(Transaction transaction, int walletId);

    public IEnumerable<Wallet> GetWallets();

    public Wallet GetWalletById(int id);

    public IEnumerable<Transaction> GetTopExpenses(int year, int month, int topN = 3);
}