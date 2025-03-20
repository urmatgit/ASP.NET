using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
using PromoCodeFactory.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly EfDataContext _dbContext;
        private readonly DbSet<T> _entitySet;
        public EfRepository(EfDataContext dbContext)
        {
            _dbContext = dbContext;
            _entitySet = _dbContext.Set<T>();   
        }

        public async Task<T> CreateAsync(T t)
        {
            var entity= await _entitySet.AddAsync(t);
            return entity.Entity;
        }

        public async Task<bool> DeleteAsync(Guid t)
        {
            var entity = await _entitySet.FindAsync(t);
            if (entity != null) {
                _entitySet.Remove(entity);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entitySet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _entitySet.FindAsync(id);
        }

        public Task<T> UpdateAsync(T t)
        {
            _dbContext.Entry(t).State = EntityState.Modified;
            return Task.FromResult(t);
        }
        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
         //   _dbContext.ChangeTracker.Clear();
        }

        public async Task<bool> AnyAsync()
        {
            return await _entitySet.AnyAsync();
        }

        public object GetDbSet()
        {
            return _entitySet;
        }
    }
}
