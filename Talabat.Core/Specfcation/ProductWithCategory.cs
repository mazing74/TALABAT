using Talabat.Core.Entites;

namespace Talabat.Core.Specfcation
{
    public class ProductWithCategory : BaseSpecifcation<Product>
    {
        public ProductWithCategory(ProductSpecParams Parameters) : base
            (p => (!Parameters.BrandId.HasValue || p.ProductBrandId == Parameters.BrandId)
                &&
            (!Parameters.TypeId.HasValue || p.ProductTypeId == Parameters.TypeId))
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

            if (!string.IsNullOrEmpty(Parameters.Sort))
            {
                switch (Parameters.Sort)
                {
                    case "PriceAsc":
                        SetOrderBy(p => p.Price);
                        break;

                    case "PriceDesc":
                        SetOrderByDesc(p => p.Price);
                        break;

                    default:
                        SetOrderBy(p => p.Name);
                        break;
                }
            }

            ApplyPagination((Parameters.PageIndex - 1) * Parameters.PageSize, Parameters.PageSize);

        }
        public ProductWithCategory(int id) : base(p => p.Id == id)
        {

            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }

    }
}
