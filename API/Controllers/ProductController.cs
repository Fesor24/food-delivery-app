using API.Helpers;
using Application.Dtos;
using Application.Features.Products.Requests.Command;
using Application.Features.Products.Requests.Queries;
using Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Routes.FetchProducts)]
        public async Task<ApiResponse<IReadOnlyList<ProductDto>, object, object>> FetchProductsByRestaurant([FromQuery] string restaurantId)
        {
            var products = await _mediator.Send(new GetProductsForRestaurantRequest { RestaurantId = restaurantId });

            return new ApiResponse<IReadOnlyList<ProductDto>, object, object>
            {
                Result = products
            };
        }

        [HttpPost(Routes.AddProducts)]
        public async Task<ApiResponse> AddProductsToRestaurant([FromBody] List<CreateProductDto> model)
        {
            var response = await _mediator.Send(new CreateProductsCommand { ProductsDto = model });

            if (response.Successful)
            {
                Response.StatusCode = 201;
            }

            return response;
        }
    }
}
