using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Interfaces;
using Talabat.Core.Specfcation;

namespace Talabat.Repository.Repositories
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseEntity
    {
        private readonly TalabatContext _context;

        public GenaricRepository(TalabatContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)=>
            await _context.Set<T>().AddAsync(entity);

        public void Delete(T entity) =>
           _context.Set<T>().Remove(entity);
        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public async Task<IReadOnlyList<T>> GetAllSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
       
        private IQueryable<T> ApplySpecification (ISpecification<T> spec) 
        {
            return SpecificationEvalutor<T>.GetQuery(_context.Set<T>(), spec);
        
        }

     

        
    }
}
