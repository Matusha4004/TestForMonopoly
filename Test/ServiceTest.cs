#pragma warning disable CA1515
using Core.Models;
using Core.ResultType;
using Core.Service;
using DataBaseLayer.Repository;

namespace Test;

public class ServiceTest
{
    [Fact]
    public void WalletServiceTest()
    {
        var wallet = new Wallet(1, "Test", "Test", 0);
        var walletService = new WalletService(new InMemoryWalletRepository(new List<Wallet>()));
        walletService.AddWallet(wallet);

        Assert.Single(walletService.GetWallets().ToList());
        Assert.Contains(wallet, walletService.GetWallets().ToList());
        Assert.Equal(wallet, walletService.GetWalletById(1));

        wallet = new Wallet(1, "Test", "Test", 10);
        walletService.ChangeWallet(wallet);

        Assert.Single(walletService.GetWallets().ToList());
        Assert.Contains(wallet, walletService.GetWallets().ToList());
        Assert.Equal(10, walletService.GetWalletById(1).InitialBalance);

        var transaction = new Transaction(1, DateTime.Now, 5, TransactionType.Income, null);
        walletService.AddTransactionToWallet(transaction, 1);

        Assert.Contains(transaction, walletService.GetWalletById(1).Transactions.ToList());

        walletService.DeleteTransaction(transaction, 1);

        Assert.Empty(walletService.GetWalletById(1).Transactions.ToList());

        transaction = new Transaction(1, DateTime.Now, 50, TransactionType.Expense, null);

        Assert.IsType<ResultAddTransaction.NotEnoughMoney>(walletService.AddTransactionToWallet(transaction, 1));
        Assert.Empty(walletService.GetWalletById(1).Transactions.ToList());
    }

    // Должен был быть тест самого задания с правильными выводами, но их можно посмотреть в консоли при запуске
    // Почти такие же базовые тесты для TransactionService, я чуть устал от этого(
}