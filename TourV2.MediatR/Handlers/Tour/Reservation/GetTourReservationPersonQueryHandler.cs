using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Helper;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetTourReservationPersonQueryHandler : IRequestHandler<GetTourReservationPersonQuery, ServiceResponse<TourReservationPersonDto>>
    {
        private readonly ITourReservationPersonRepository _tourReservationPersonRepository;
        private readonly IMapper _mapper;

        public GetTourReservationPersonQueryHandler(
            ITourReservationPersonRepository tourReservationPersonRepository,
            IMapper mapper)
        {
            _tourReservationPersonRepository = tourReservationPersonRepository;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<TourReservationPersonDto>> Handle(GetTourReservationPersonQuery request, CancellationToken cancellationToken)
        {
            var entity = await _tourReservationPersonRepository.FindByInclude(x => x.Id == request.Id, i => i.TourDeparture).FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<TourReservationPersonDto>.ReturnResultWith200(_mapper.Map<TourReservationPersonDto>(entity));
            else
            {
                return ServiceResponse<TourReservationPersonDto>.Return404();
            }
        }
    }
}
