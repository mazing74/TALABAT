using Talabat.Core.Entites;

namespace Talabat.Core.Specfcation
{
    public class ProductWithCategory : BaseSpecifcation<Product>
    {
        public ProductWithCategory()
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

        }
        public ProductWithCategory(int id) : base(p => p.Id == id)
        {

            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }

    }
}
