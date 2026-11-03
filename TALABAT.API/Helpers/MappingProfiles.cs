using AutoMapper;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Identity;
using TALABAT.API.DTO_s;

namespace TALABAT.API.Helpers
{
    public class MappingProfiles :Profile
    {

        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d=>d.ProductPrand,o=>o.MapFrom(s=>s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d=>d.PictureUrl,o=>o.MapFrom<ProductPictureResolve>());

            CreateMap<Address, AddressDto>().ReverseMap();
            
        }
    }
}
