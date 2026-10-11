using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entites;
using Talabat.Core.Interfaces;
using Talabat.Core.Specfcation;
using TALABAT.API.DTO_s;
using TALABAT.API.Errors;

namespace TALABAT.API.Controllers
{
    public class ProductController : ApiBaseController
    {
        private readonly IGenaricRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly IGenaricRepository<ProductBrand> _productbrand;
        private readonly IGenaricRepository<ProductType> _producttype;

        public ProductController(IGenaricRepository<Product> ProductRepository, IMapper mapper, IGenaricRepository<ProductBrand> productbrand, IGenaricRepository<ProductType> producttype)
        {
            _productRepository = ProductRepository;
            _mapper = mapper;
            _productbrand = productbrand;
            _producttype = producttype;
        }
        //get all 
        [HttpGet]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetAllWithSpec()
        {
            var spec = new ProductWithCategory();
            var products = await _productRepository.GetAllSpec(spec);
            var MappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(MappedProducts);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetByIdWithSpec(int id)
        {
            var spec = new ProductWithCategory(id);
            var product = await _productRepository.GetByIdSpec(spec);
            if (product == null)
                return NotFound(new ApiResponse(404));

            var MappedProducts = _mapper.Map<Product, ProductToReturnDto>(product);
            return Ok(MappedProducts);

        }

        [HttpGet("Brands")]
        [ProducesResponseType(typeof(ProductBrand), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var ProductBrand = await _productbrand.GetAllAsync();

            return Ok(ProductBrand);

        }

        [HttpGet("Types")]
        [ProducesResponseType(typeof(ProductType), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var ProductTypes = await _producttype.GetAllAsync();

            return Ok(ProductTypes);

        }







    }
}
