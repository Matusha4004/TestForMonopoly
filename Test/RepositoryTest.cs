#pragma warning disable CA1515
#pragma warning disable SK1200
using Core.Models;
using DataBaseLayer.Repository;

namespace Test;

public class RepositoryTest
{
    [Fact]
    public void TestWalletListAddGetByIdUpdate()
    {
        var testWallet = new Wallet(1, "Test", "Test", 0);
        var walletRepository = new InMemoryWalletRepository(new List<Wallet>());
        walletRepository.Add(testWallet);

        Assert.True(walletRepository.List().ToList().Count == 1);
        Assert.Contains(testWallet, walletRepository.List().ToList());
        Assert.Equal(walletRepository.GetById(1), testWallet);

        testWallet = new Wallet(1, "Test", "Test", 10);
        walletRepository.Update(testWallet);

        Assert.True(walletRepository.List().ToList().Count == 1);
        Assert.NotNull(walletRepository.GetById(1));
        if (walletRepository.GetById(1) == null) return;
        Assert.Equal(10, walletRepository.GetById(1)!.InitialBalance);

        walletRepository.Delete(walletRepository.GetById(1)!);
        Assert.True(walletRepository.List().ToList().Count == 0);
    }

    [Fact]
    public void TestTransactionListAddGetByIdUpdate()
    {
        var transaction = new Transaction(1, DateTime.Now, 10, TransactionType.Income, "Test");
        var transactionRep = new InMemoryTransactionRepository(new List<Transaction>());
        transactionRep.Add(transaction);

        Assert.True(transactionRep.List().ToList().Count == 1);
        Assert.Contains(transaction, transactionRep.List().ToList());
        Assert.Equal(transactionRep.GetById(1), transaction);

        transaction = new Transaction(1, DateTime.Now, 11, TransactionType.Income, "Test");
        transactionRep.Update(transaction);

        Assert.True(transactionRep.List().ToList().Count == 1);
        Assert.NotNull(transactionRep.GetById(1));
        if (transactionRep.GetById(1) == null) return;
        Assert.Equal(11, transactionRep.GetById(1)!.Amount);

        transactionRep.Delete(transactionRep.GetById(1)!);
        Assert.True(transactionRep.List().ToList().Count == 0);
    }
}