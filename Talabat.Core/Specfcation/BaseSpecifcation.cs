using System.Linq.Expressions;
using Talabat.Core.Entites;

namespace Talabat.Core.Specfcation
{
    public class BaseSpecifcation<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; }
            = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Take { get ; set ; }
        public int Skip { get ; set; }
        public bool IsPaginationEnabled { get ; set; }

        public BaseSpecifcation()
        {

        }
        public BaseSpecifcation(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public void SetOrderBy(Expression<Func<T, object>> OrderByexpression) 
        {
            OrderBy = OrderByexpression;
        }
        public void SetOrderByDesc(Expression<Func<T, object>> OrderByDescexpression)
        {
            OrderByDesc = OrderByDescexpression;
        }
        public void ApplyPagination(int skip, int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}
