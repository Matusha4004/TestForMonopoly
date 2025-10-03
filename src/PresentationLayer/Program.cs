#pragma warning disable IDE0008
using Core;
using Core.Models;
using Core.Service;
using DataBaseLayer;
using Microsoft.Extensions.DependencyInjection;

namespace PresentationLayer;

internal class Program
{
    private static IWalletService? _walletService;
    private static ITransactionService? _transactionService;

    public static void Main(string[] args)
    {
        var serviceProvider = BuildServiceProvider();

        Console.WriteLine("Welcome to the Finance Console App!");
        Console.WriteLine("All work!");

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Press 1 to group Transactions");
            Console.WriteLine("Press 2 to get top 3 expenses for each wallet");
            Console.WriteLine("Press 3 to exit");
            Console.Write("> ");

            var input = Console.ReadLine();
            if (input == "1")
            {
                RunGroupedTransactions();
            }
            else if (input == "2")
            {
                RunTopExpensesPerWallet();
            }
            else if (input == "3")
            {
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Invalid input. Try again.");
                Console.ResetColor();
            }
        }
    }

    private static ServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();

        services.AddDataLayer()
            .AddBusinessLayer();

        var provider = services.BuildServiceProvider();

        _walletService = provider.GetRequiredService<IWalletService>();
        _transactionService = provider.GetRequiredService<ITransactionService>();

        _walletService.AddWallet(new Wallet(1, "Cash", "USD", 1000));
        _walletService.AddWallet(new Wallet(2, "Credit Card", "USD", 5000));
        _walletService.AddWallet(new Wallet(3, "Savings", "EUR", 2000));

        var now = DateTime.Now;
        _transactionService.AddTransaction(1, new Transaction(1, now.AddDays(-10), 150, TransactionType.Income, "Salary"));
        _transactionService.AddTransaction(1, new Transaction(2, now.AddDays(-8), 50, TransactionType.Expense, "Groceries"));
        _transactionService.AddTransaction(1, new Transaction(3, now.AddDays(-6), 20, TransactionType.Expense, "Electronics"));
        _transactionService.AddTransaction(2, new Transaction(4, now.AddDays(-5), 75, TransactionType.Income, "Interest"));
        _transactionService.AddTransaction(3, new Transaction(5, now.AddDays(-3), 100, TransactionType.Income, "Dining Out"));

        return provider;
    }

    private static void RunGroupedTransactions()
    {
        try
        {
            Console.Write("Year YYYY? ");
            var year = new Year(Convert.ToInt32(Console.ReadLine()));
            Console.Write("Month MM? ");
            var month = new Month(Convert.ToInt32(Console.ReadLine()));

            if (_transactionService == null)
                throw new InvalidOperationException("Transaction service not initialized.");

            var grouped = _transactionService
                .GetTransactionsInMonthWithSort(year.Value, month.Value);

            Console.WriteLine();
            Console.WriteLine($"Transactions for {year.Value}-{month.Value}:");
            foreach (var tx in grouped)
            {
                var tag = tx.Type == TransactionType.Income ? "[Income]" : "[Expense]";
                Console.WriteLine($"{tx.Date:yyyy-MM-dd}\t{tag}\t{tx.Amount}\t{tx.Description}");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
        }
    }

    private static void RunTopExpensesPerWallet()
    {
        try
        {
            Console.Write("Year YYYY? ");
            var year = new Year(Convert.ToInt32(Console.ReadLine()));
            Console.Write("Month MM? ");
            var month = new Month(Convert.ToInt32(Console.ReadLine()));

            if (_walletService == null)
                throw new InvalidOperationException("Wallet service not initialized.");

            var topExpenses = _walletService.GetTopExpenses(year.Value, month.Value, topN: 3);

            Console.WriteLine();
            Console.WriteLine($"Top 3 expenses for each wallet in {year.Value}-{month.Value}:");
            foreach (var tx in topExpenses)
            {
                Console.WriteLine($"WalletId:{tx.Id}\t{tx.Date:yyyy-MM-dd}\t{tx.Amount}\t{tx.Description}");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
        }
    }
}