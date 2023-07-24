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
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet(Routes.FetchProducts)]
        public async Task<ApiResponse<IReadOnlyList<ProductDto>, object, object>> FetchProductsByRestaurant([FromQuery] string restaurantId)
        {
            var spec = new ProductByRestaurantIdSpecification(restaurantId);

            var products = await _unitOfWork.Repository<Products>().GetAllWIthSpecAsync(spec);

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

            await _unitOfWork.Repository<Products>().AddListAsync(products);

            return new ApiResponse
            {
                Result = "Created"
            };
        }
    }
}
