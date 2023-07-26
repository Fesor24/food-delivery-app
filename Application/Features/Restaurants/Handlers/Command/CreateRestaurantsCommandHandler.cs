using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Features.Restaurants.Requests.Command;
using Application.Response;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using MediatR;

namespace Application.Features.Restaurants.Handlers.Command
{
    public class CreateRestaurantsCommandHandler : IRequestHandler<CreateRestaurantsCommand, ApiResponse>
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public CreateRestaurantsCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse> Handle(CreateRestaurantsCommand request, CancellationToken cancellationToken)
        {
            var restaurants = _mapper.Map<List<RestaurantDto>, List<Restaurant>>(request.RestaurantsDto);

            await _unitOfWork.Repository<Restaurant>().AddListAsync(restaurants);

            await _unitOfWork.CompleteAsync();

            return new ApiResponse
            {
                Result = "Created at Action"
            };
        }
    }
}
