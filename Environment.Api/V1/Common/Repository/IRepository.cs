using System.Linq.Expressions;

namespace Environment.Api.V1.Common.Repository;

public interface IRepository<T> where T : BaseEntity.BaseEntity
{
    IQueryable<T> GetAll(Expression<Func<T, bool>> expression, string[] includes = null, bool isTracking = true);
    ValueTask<T> GetAsync(Expression<Func<T, bool>> expression, bool isTracking = true, string[] includes = null);
    ValueTask<T> CreateAsync(T entity);
    ValueTask<bool> DeleteAsync(int id);
    T Update(T entity);
    ValueTask SaveChangesAsync();
}
