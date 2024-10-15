using Environment.Api.V1.Common.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Environment.Api.V1.Common.Repository;

public class Repository<T> : IRepository<T> where T : BaseEntity.BaseEntity
{

    protected readonly AppDbContext dbContext;
    protected readonly DbSet<T> dbSet;

    public Repository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.dbSet = dbContext.Set<T>();
    }

    public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> expression, string[] includes = null, bool isTracking = true)
    {
        var query = expression is null ? dbSet : dbSet.Where(expression);

        if (includes != null)
            foreach (var include in includes)
                if (!string.IsNullOrEmpty(include))
                    query = query.Include(include);

        if (!isTracking)
            query = query.AsNoTracking().AsSplitQuery();

        return query;
    }

    public virtual async ValueTask<T> GetAsync(Expression<Func<T, bool>> expression, bool isTracking = true, string[] includes = null) =>
        await GetAll(expression, includes, isTracking).FirstOrDefaultAsync();

    public async ValueTask<T> CreateAsync(T entity) =>
        (await dbContext.AddAsync(entity)).Entity;

    public async ValueTask<bool> DeleteAsync(int id)
    {
        var entity = await GetAsync(e => e.Id == id);

        if (entity == null)
            return false;

        dbSet.Remove(entity);

        return true;
    }

    public T Update(T entity) =>
        dbSet.Update(entity).Entity;

    public async ValueTask SaveChangesAsync() =>
        await dbContext.SaveChangesAsync();

}
