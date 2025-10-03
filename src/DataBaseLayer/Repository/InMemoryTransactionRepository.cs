using Core.Models;
using Core.Repository;

namespace DataBaseLayer.Repository;

public class InMemoryTransactionRepository : IRepositoryTransaction
{
    private IEnumerable<Transaction> _transactions;

    public InMemoryTransactionRepository(IEnumerable<Transaction> transactions)
    {
        _transactions = transactions;
    }

    public Transaction? GetById(int id)
    {
        return _transactions.FirstOrDefault(t => t.Id == id);
    }

    public IEnumerable<Transaction> List()
    {
        return _transactions.ToList();
    }

    public void Add(Transaction entity)
    {
        _transactions = _transactions.Append(entity);
    }

    public void Update(Transaction entity)
    {
        _transactions.ToList().Remove(_transactions.FirstOrDefault(t => entity.Id == t.Id) ?? throw new InvalidOperationException());
        _transactions = _transactions.Append(entity);
    }

    public void Delete(Transaction entity)
    {
        _transactions.ToList().Remove(_transactions.FirstOrDefault(t => entity.Id == t.Id) ?? throw new InvalidOperationException());
    }
}