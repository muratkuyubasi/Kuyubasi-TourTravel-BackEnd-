using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Helper;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetTourReservationQueryHandler : IRequestHandler<GetTourReservationQuery, ServiceResponse<List<TourReservationDto>>>
    {
        private readonly ITourReservationRepository _tourReservationRepository;
        private readonly IMapper _mapper;

        public GetTourReservationQueryHandler(
            ITourReservationRepository tourReservationRepository,
            IMapper mapper)
        {
            _tourReservationRepository = tourReservationRepository;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<TourReservationDto>>> Handle(GetTourReservationQuery request, CancellationToken cancellationToken)
        {
            var entity = await _tourReservationRepository.FindByInclude(x => x.ActiveTourId== request.Id, i => i.ReservationPersons).ToListAsync();
            if (entity != null)
                return ServiceResponse<List<TourReservationDto>>.ReturnResultWith200(_mapper.Map<List<TourReservationDto>>(entity));
            else
            {
                return ServiceResponse<List<TourReservationDto>>.Return404();
            }
        }
    }
}
