using Core.Models;

namespace Core.Repository;

public interface IRepositoryTransaction : IRepository<Transaction>
{
    IEnumerable<Transaction> GetTransactionsInMonth(int year, int month);
}