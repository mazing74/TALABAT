using System.Text.Json;
using Talabat.Core.Entites;

namespace Talabat.Repository.Data.DataSeed
{
    public class TalabatSeed
    {
        public static async Task seedAsync(TalabatContext context)
        {



            if (context.ProductBrand.Any() == false)
            {
                var brandssdata = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandssdata);
                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                        await context.Set<ProductBrand>().AddAsync(brand);

                    await context.SaveChangesAsync();

                }

            }
            if (context.ProductType.Any() == false)
            {
                var typessdata = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typessdata);
                if (types?.Count > 0)
                {
                    foreach (var type in types)
                        await context.Set<ProductType>().AddAsync(type);

                    await context.SaveChangesAsync();

                }

            }

            if (context.Products.Any() == false)
            {
                var productsdata = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsdata);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                        await context.Set<Product>().AddAsync(product);

                    await context.SaveChangesAsync();

                }

            }

        }
    }
}
