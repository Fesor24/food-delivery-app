using Application.Dtos;
using Application.Features.Restaurants.Requests.Queries;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using MediatR;

namespace Application.Features.Restaurants.Handlers.Queries
{
    public class GetRestaurantsRequestHandler : IRequestHandler<GetRestaurantsRequest, IReadOnlyList<RestaurantDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public GetRestaurantsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<RestaurantDto>> Handle(GetRestaurantsRequest request, CancellationToken cancellationToken)
        {
            var spec = new RestaurantSpecification(request.Sort, request.Location);

            var restaurants = await _unitOfWork.Repository<Restaurant>().GetAllWIthSpecAsync(spec);

            var restaurantsToReturn = _mapper.Map<IReadOnlyList<Restaurant>, IReadOnlyList<RestaurantDto>>(restaurants);

            return restaurantsToReturn;
        }
    }
}
