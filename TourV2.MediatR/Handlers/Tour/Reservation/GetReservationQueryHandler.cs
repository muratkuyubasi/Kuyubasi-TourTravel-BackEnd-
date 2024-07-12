using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Helper;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetReservationQueryHandler : IRequestHandler<GetReservationQuery, ServiceResponse<TourReservationDto>>
    {
        private readonly ITourReservationRepository _tourReservationRepository;
        private readonly IMapper _mapper;

        public GetReservationQueryHandler(
            ITourReservationRepository tourReservationRepository,
            IMapper mapper)
        {
            _tourReservationRepository = tourReservationRepository;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<TourReservationDto>> Handle(GetReservationQuery request, CancellationToken cancellationToken)
        {
            var entity = await _tourReservationRepository.FindByInclude(x => x.Code == request.Id, i => i.ReservationPersons,
                t => t.ActiveTour.TourRecord).FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<TourReservationDto>.ReturnResultWith200(_mapper.Map<TourReservationDto>(entity));
            else
            {
                return ServiceResponse<TourReservationDto>.Return404();
            }
        }
    }
}
