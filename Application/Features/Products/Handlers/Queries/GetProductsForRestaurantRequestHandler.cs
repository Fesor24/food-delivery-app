using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Features.Products.Requests.Queries;
using AutoMapper;
using ProductEntity = Core.Entities.Products;
using Core.Interfaces;
using Core.Specifications;
using MediatR;

namespace Application.Features.Products.Handlers.Queries
{
    public class GetProductsForRestaurantRequestHandler : IRequestHandler<GetProductsForRestaurantRequest, IReadOnlyList<ProductDto>>
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public GetProductsForRestaurantRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<ProductDto>> Handle(GetProductsForRestaurantRequest request, CancellationToken cancellationToken)
        {
            var spec = new ProductByRestaurantIdSpecification(request.RestaurantId);

            var products = await _unitOfWork.Repository<ProductEntity>().GetAllWIthSpecAsync(spec);

            var productsToReturn = _mapper.Map<IReadOnlyList<ProductEntity>, IReadOnlyList<ProductDto>>(products);

            return productsToReturn;
        }
    }
}
