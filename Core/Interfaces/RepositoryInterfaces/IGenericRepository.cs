using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.Interfaces.RepositoryInterfaces;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAll();

    EntityEntry<T> Add(T entity);

    Task AddAsync(T entity);

    void AddRange(IEnumerable<T> entities);

    Task AddRangeAsync(IEnumerable<T> entities);

    void Remove(T entity);

    Task RemoveByIdAsync(int id);

    void RemoveRange(IEnumerable<T> entities);

    Task<T> FindAsync(int id);

    void Update(T entity);

    Task<bool> SaveChangesAsync();
}