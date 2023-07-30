using Microsoft.EntityFrameworkCore.ChangeTracking;
using NewProject.Shared;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace NewProject.Repositories;

public interface IGeneralRepository<T, TKey> where T : class
{
    EntityEntry<T> Add(T entry);
    Task<EntityEntry<T>> AddAsync(T entry);
    public IQueryable<T> Get(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderby = null,
        string? orderDirection = Constanties.ORDERASC , int? take = null, int? skip = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

    IQueryable<T> Get(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
    T? GetBy(Expression<Func<T,bool>> expression);
    T? GetBy(Expression<Func<T,bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
    EntityEntry<T> Remove(T entry);
    EntityEntry<T> Update(T entry);
    Task<T?> GetById(TKey Id);
    int SaveChanges();
    Task<int> SaveChangesAsync();
    Task<bool> IsExistedAsync(Expression<Func<T, bool>> expression);
    bool IsExisted(Expression<Func<T, bool>> expression);
    int Count(Expression<Func<T, bool>>? expression = null);
}
