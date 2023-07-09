using API.Dtos;
using API.Helpers;
using API.Response;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Products> _productRepo;

        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Products> productRepo, IMapper mapper)
        {
            _productRepo= productRepo;
            _mapper = mapper;
        }

        [HttpGet(Routes.FetchProducts)]
        public async Task<ApiResponse<IReadOnlyList<ProductDto>, object, object>> FetchProductsByRestaurant([FromQuery] string restaurantId)
        {
            var spec = new ProductByRestaurantIdSpecification(restaurantId);

            var products = await _productRepo.GetAllWIthSpecAsync(spec);

            var productsToReturn = _mapper.Map<IReadOnlyList<Products>, IReadOnlyList<ProductDto>>(products);

            return new ApiResponse<IReadOnlyList<ProductDto>, object, object>
            {
                Result = productsToReturn
            };
        }

        [HttpPost(Routes.AddProducts)]
        public async Task<ApiResponse> AddProductsToRestaurant([FromBody] List<CreateProductDto> model)
        {
            var products = _mapper.Map<List<CreateProductDto>, List<Products>>(model);

            await _productRepo.AddListAsync(products);

            return new ApiResponse
            {
                Result = "Created"
            };
        }
    }
}
