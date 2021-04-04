using Microsoft.EntityFrameworkCore;
using Sunioj.Core.Contracts.Repositories;
using Sunioj.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sunioj.Data.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : Entity
    {
        protected readonly DbContext Context;
        private readonly DbSet<TEntity> _entities;

        public Repository(DbContext context)
        {
            Context = context;
            _entities = context.Set<TEntity>();
        }

        public AppDbContext AppDbContext
        {
            get { return Context as AppDbContext; }
        }

        public virtual ValueTask<TEntity> GetByIdAsync(TKey id)
        {
            return _entities.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public async Task<int> CountAsync()
        {
            return await _entities.CountAsync();
        }

        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleOrDefaultAsync(predicate);
        }

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }
    }
}
