using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specfcation;

namespace Talabat.Core.Interfaces
{
    public interface IGenaricRepository<T> where T :BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllSpec(ISpecification<T> spec);
        Task<T?> GetByIdSpec(ISpecification<T> spec);
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync();

        public Task<int> GetCountWithSpecAsync(ISpecification<T> spec);


    }
}
