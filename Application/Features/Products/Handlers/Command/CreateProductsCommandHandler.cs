using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Features.Products.Requests.Command;
using Application.Response;
using AutoMapper;
using ProductEntity = Core.Entities.Products;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Products.Handlers.Command
{
    public class CreateProductsCommandHandler : IRequestHandler<CreateProductsCommand, ApiResponse>
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public CreateProductsCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse> Handle(CreateProductsCommand request, CancellationToken cancellationToken)
        {
            var products = _mapper.Map<List<CreateProductDto>, List<ProductEntity>>(request.ProductsDto);

            await _unitOfWork.Repository<ProductEntity>().AddListAsync(products);

            await _unitOfWork.CompleteAsync();

            return new ApiResponse
            {
                Result = "Created at action"
            };
        }
    }
}
