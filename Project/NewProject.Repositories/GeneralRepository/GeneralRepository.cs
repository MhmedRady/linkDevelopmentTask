using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NewProject.Domain.BaseEntities;
using NewProject.EntityFrameworkCore;
using NewProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace NewProject.Repositories
{
    public class GeneralRepository<T, TKey> :IGeneralRepository<T, TKey> where T : class
    {
        private readonly MainDbContext _dbContext;
        public GeneralRepository(MainDbContext _DBContext)
        {
            _dbContext = _DBContext;
        }

        private IQueryable<T> SetEntity()
        {
            return _dbContext.Set<T>();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderby = null,
            string? orderDirection = Constanties.ORDERASC , int? take = null, int? skip = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = this.SetEntity();
            
            if (include != null)
                query = include(query);
            if (expression != null)
                query = query.Where(expression);
            
            if (take.HasValue)
                query.Take(take.Value);

            if (skip.HasValue)
                query.Skip(skip.Value);
            
            if (orderby is not null)
            {
                if(orderDirection == Constanties.ORDERASC)
                {
                    query = query.OrderBy(orderby);
                }
                else
                {
                    query = query.OrderByDescending(orderby);
                }
            }
            return query;
        }
        public IQueryable<T> Get(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return this.Get(expression, null, null, include: include);
        }
        
        public T? GetBy(Expression<Func<T, bool>> expression)
        {
            var query = this.SetEntity();
            return query.Where(expression).FirstOrDefault();
        }
        
        public T? GetBy(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var entity = SetEntity();
            if (include != null)
                entity = include(entity);
            if (expression != null)
                entity = entity.Where(expression);
            return entity.FirstOrDefault();
        }

        public async Task<T?> GetById(TKey Id)
        {
            return await _dbContext.Set<T>().FindAsync(Id);
        }
        public async Task<EntityEntry<T>> AddAsync(T entry) => await _dbContext.Set<T>().AddAsync(entry);
        public EntityEntry<T> Add(T entry) => _dbContext.Set<T>().Add(entry);
        public EntityEntry<T> Update(T entry) => _dbContext.Set<T>().Update(entry);
        public EntityEntry<T> Remove(T entry) => _dbContext.Set<T>().Remove(entry);

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public bool IsExisted(Expression<Func<T, bool>> expression)
        {
            var query = this.SetEntity();
            return query.Any(expression);
        }
        public async Task<bool> IsExistedAsync(Expression<Func<T, bool>> expression)
        {
            var query = this.SetEntity();
            return await query.AnyAsync(expression);
        }
        public int Count(Expression<Func<T, bool>>? expression)
        {
            var query = this.SetEntity();
            if(expression == null)
                return query.Count();
            return query.Count(expression);
        }

        public void EntityStateModified(T Entity)
        {
            _dbContext.Entry(Entity).State = EntityState.Modified;
        }
    }
}
