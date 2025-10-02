namespace Core.Repository;

public interface IRepository<T>
{
    T? GetById(int id);

    IEnumerable<T> List();

    void Add(T entity);

    void Update(T entity);

    void Delete(T entity);
}