using Core.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class BusinessLayerExtensions
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        services.AddSingleton<IWalletService, WalletService>();
        services.AddSingleton<ITransactionService, TransactionService>();
        return services;
    }
}