using AutoMapper;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Entites.OrderAggregation;
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

            CreateMap<Talabat.Core.Entites.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<Talabat.Core.Entites.OrderAggregation.Address, AddressDto>().ReverseMap();
            
        }
    }
}
