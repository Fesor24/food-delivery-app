using Application.Dtos;
using Application.Features.Restaurants.Requests.Queries;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using MediatR;

namespace Application.Features.Restaurants.Handlers.Queries
{
    public class GetRestaurantByIdHandler : IRequestHandler<GetRestaurantByIdRequest, RestaurantDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public GetRestaurantByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RestaurantDto> Handle(GetRestaurantByIdRequest request, CancellationToken cancellationToken)
        {
            var spec = new RestaurantByIdSpecification(request.Id);

            var restaurant = await _unitOfWork.Repository<Restaurant>().GetWithSpecAsync(spec);

            if(restaurant is null)
            {
                return null;
            }

            var restaurantToReturn = _mapper.Map<Restaurant, RestaurantDto>(restaurant);

            return restaurantToReturn;
        }
    }
}
