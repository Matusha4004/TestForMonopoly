using Core.Models;
using Core.Repository;

namespace DataBaseLayer.Repository;

public class InMemoryWalletRepository : IRepositoryWallet
{
    private IEnumerable<Wallet> _wallets;

    public InMemoryWalletRepository(IEnumerable<Wallet> wallets)
    {
        _wallets = wallets;
    }

    public Wallet? GetById(int id)
    {
        return _wallets.FirstOrDefault(t => t.Id == id);
    }

    public IEnumerable<Wallet> List()
    {
        return _wallets.ToList();
    }

    public void Add(Wallet entity)
    {
        _wallets = _wallets.Append(entity);
    }

    public void Update(Wallet entity)
    {
        _wallets = _wallets.Where(t => t.Id != entity.Id);

        _wallets = _wallets.Append(entity);
    }

    public void Delete(Wallet entity)
    {
        _wallets = _wallets.Where(t => t.Id != entity.Id);
    }
}
