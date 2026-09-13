using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specfcation;

namespace Talabat.Repository.Repositories
{
    public static class SpecificationEvalutor<T> where T:BaseEntity
    {
      public static IQueryable<T> GetQuery(IQueryable<T> inputquery,ISpecification<T> specification)
        {
            var query = inputquery;

            if (specification.Criteria is not null)
                query = query.Where(specification.Criteria);

            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }

    }
}
