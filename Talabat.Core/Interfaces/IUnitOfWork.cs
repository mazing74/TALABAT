using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Interfaces
{
    public interface IUnitOfWork :IAsyncDisposable
    {

        IGenaricRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> CompeleteAsync();
    }
}
