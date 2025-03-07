using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
namespace PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T>: IRepository<T> where T: BaseEntity
    {
        protected List<T> Data { get; set; }

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data =(List<T>) data;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult((IEnumerable<T>)Data);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task<T> CreateAsync(T entity)
        {
              
            Data.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<T> UpdateAsync( T entity)
        {
            Data.Remove(entity);
            Data.Add(entity);
            return Task.FromResult(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var enitity=await  GetByIdAsync(id);
            if (enitity is not null)
            {
                Data.Remove(enitity);
            }
        }
    }
}