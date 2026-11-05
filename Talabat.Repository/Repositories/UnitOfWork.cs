using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Interfaces;

namespace Talabat.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TalabatContext _dbcontext;
        private Hashtable _repositories;

        public UnitOfWork(TalabatContext dbcontext)
        {
            _dbcontext = dbcontext;
            _repositories = new Hashtable();
        }
        public async Task<int> CompeleteAsync() => await _dbcontext.SaveChangesAsync();

        public async ValueTask DisposeAsync() => await _dbcontext.DisposeAsync();

        public IGenaricRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var Type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(Type))
            {
                var Repository = new GenaricRepository<TEntity>(_dbcontext);
                _repositories.Add(Type, Repository);
            }
            return _repositories[Type] as IGenaricRepository<TEntity>;
        }
    }
}
