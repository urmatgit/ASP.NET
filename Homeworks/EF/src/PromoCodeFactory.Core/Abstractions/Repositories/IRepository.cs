using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PromoCodeFactory.Core.Domain;

namespace PromoCodeFactory.Core.Abstractions.Repositories
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> AnyAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> CreateAsync(T t);
        Task<T> UpdateAsync(T t);
        Task<bool> DeleteAsync(Guid t);
        
        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}