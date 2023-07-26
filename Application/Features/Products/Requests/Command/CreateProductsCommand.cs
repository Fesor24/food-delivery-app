using Application.Dtos;
using Application.Response;
using MediatR;

namespace Application.Features.Products.Requests.Command
{
    public class CreateProductsCommand : IRequest<ApiResponse>
    {
        public List<CreateProductDto> ProductsDto { get; set; }
    }
}
