using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetAllTourReservationPersonQueryHandler : IRequestHandler<GetAllTourReservationPersonQuery, List<TourReservationPersonDto>>
    {
        private readonly ITourReservationPersonRepository _tourReservationPersonRepository;
        private readonly IMapper _mapper;

        public GetAllTourReservationPersonQueryHandler(
            ITourReservationPersonRepository tourReservationPersonRepository,
            IMapper mapper)
        {
            _tourReservationPersonRepository = tourReservationPersonRepository;
            _mapper = mapper;

        }
        public async Task<List<TourReservationPersonDto>> Handle(GetAllTourReservationPersonQuery request, CancellationToken cancellationToken)
        {
            var entities = await _tourReservationPersonRepository.AllIncluding(i => i.TourDeparture).ToListAsync();
            return _mapper.Map<List<TourReservationPersonDto>>(entities);
        }
    }
}
