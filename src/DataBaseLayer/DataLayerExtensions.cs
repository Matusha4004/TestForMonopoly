using Core.Models;
using Core.Repository;
using DataBaseLayer.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DataBaseLayer;

public static class DataLayerExtensions
{
    public static IServiceCollection AddDataLayer(this IServiceCollection services)
    {
        services.AddSingleton<IEnumerable<Wallet>>(_ => new List<Wallet>());
        services.AddSingleton<IEnumerable<Transaction>>(_ => new List<Transaction>());

        services.AddSingleton<IRepositoryWallet, InMemoryWalletRepository>();
        services.AddSingleton<IRepositoryTransaction, InMemoryTransactionRepository>();

        return services;
    }
}