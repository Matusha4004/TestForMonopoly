using Core.Models;
using Core.Repository;
using Core.ResultType;

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

    public ResultAddTransaction AddTransactionToWallet(Transaction transaction, int walletId)
    {
        Wallet? wallet = _repositoryWallet.GetById(walletId);
        if (wallet == null) throw new Exception("Wallet not found");
        return wallet.AddTransaction(transaction);
    }

    public void DeleteWallet(Wallet wallet)
    {
        _repositoryWallet.Delete(wallet);
    }

    public void ChangeWallet(Wallet wallet)
    {
        _repositoryWallet.Update(wallet);
    }

    public void DeleteTransaction(Transaction transaction, int walletId)
    {
        Wallet? oldWallet = _repositoryWallet.GetById(walletId);
        if (oldWallet == null) throw new Exception("Wallet not found");
        var newWallet = new Wallet(oldWallet.Id, oldWallet.Name, oldWallet.Currency, oldWallet.InitialBalance);
        foreach (Transaction oldTransactions in oldWallet.Transactions.Where(t => t.Type == TransactionType.Income))
        {
            if (oldTransactions.Id != transaction.Id)
            {
                newWallet.AddTransaction(oldTransactions);
            }
        }

        foreach (Transaction oldTransactions in oldWallet.Transactions.Where(t => t.Type == TransactionType.Expense))
        {
            if (oldTransactions.Id != transaction.Id)
            {
                newWallet.AddTransaction(oldTransactions);
            }
        }

        _repositoryWallet.Update(newWallet);
    }

    public IEnumerable<Wallet> GetWallets()
    {
        return _repositoryWallet.List();
    }

    public Wallet GetWalletById(int id)
    {
        return _repositoryWallet.GetById(id) ?? throw new NullReferenceException();
    }

    public IEnumerable<Transaction> GetTopExpenses(int year, int month, int topN = 3)
    {
        var wallets = _repositoryWallet.List().ToList();

        var lists = new List<Transaction>();

        foreach (Wallet wallet in wallets)
        {
            lists
                .AddRange(wallet.Transactions
                    .Where(data => data.Date.Year == year
                                   && data.Date.Month == month
                                   && data.Type == TransactionType.Expense)
                    .OrderByDescending(t => t.Amount)
                    .Take(topN)
                    .ToList());
        }

        return lists;
    }
}