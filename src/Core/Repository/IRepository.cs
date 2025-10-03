namespace Core.Repository;

public interface IRepository<T>
{
    public T? GetById(int id);

    public IEnumerable<T> List();

    public void Add(T entity);

    public void Update(T entity);

    public void Delete(T entity);
}