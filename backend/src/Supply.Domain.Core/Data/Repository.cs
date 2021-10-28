using Microsoft.EntityFrameworkCore;
using Supply.Domain.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Supply.Domain.Core.Data
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly BaseDbContext _baseDbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(BaseDbContext baseDbContext)
        {
            _baseDbContext = baseDbContext;
            _dbSet = baseDbContext.Set<TEntity>();
        }

        public IUnitOfWork UnitOfWork => _baseDbContext;

        protected IQueryable<TEntity> Query()
        {
            return _dbSet.AsNoTracking().Where(x => !x.Removed);
        }

        protected virtual IQueryable<TEntity> IncludeQuery()
        {
            return Query();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Query().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await Query().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await Query().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TEntity> GetByIdWithIncludes(Guid id)
        {
            return await IncludeQuery().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            entity.Remove();
            _dbSet.Update(entity);
        }
    }
}
