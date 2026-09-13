using AutoMapper;
using Talabat.Core.Entites;
using TALABAT.API.DTO_s;

namespace TALABAT.API.Helpers
{
    public class ProductPictureResolve : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureResolve(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";


            else return string.Empty;

        }
    }
}
