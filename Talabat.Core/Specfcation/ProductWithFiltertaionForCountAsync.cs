using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specfcation
{
    public class ProductWithFiltertaionForCountAsync :BaseSpecifcation<Product>
    {
        public ProductWithFiltertaionForCountAsync(ProductSpecParams Parameters) :base((p => (!Parameters.BrandId.HasValue || p.ProductBrandId == Parameters.BrandId)
                &&
            (!Parameters.TypeId.HasValue || p.ProductTypeId == Parameters.TypeId)))
        {
                
        }
    }
}
