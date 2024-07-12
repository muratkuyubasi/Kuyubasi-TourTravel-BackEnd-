using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.MediatR.Queries;
using TourV2.MediatR.Queries.Tour.Reservation;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers.Tour.Reservation
{
    public class GetAllParticipantByTourIdQueryHandler : IRequestHandler<GetAllParticipantByTourIdQuery, List<TourReservationDto>>
    {
        private readonly ITourReservationRepository _tourReservationRepository;
        private readonly IMapper _mapper;

        public GetAllParticipantByTourIdQueryHandler(
            ITourReservationRepository tourReservation,
            IMapper mapper)
        {
            _tourReservationRepository = tourReservation;
            _mapper = mapper;

        }
        public async Task<List<TourReservationDto>> Handle(GetAllParticipantByTourIdQuery request, CancellationToken cancellationToken)
        {
            var entities = await _tourReservationRepository.AllIncluding(x => x.ReservationPersons).Where(x => x.ActiveTourId == request.TourId).ToListAsync();
            return _mapper.Map<List<TourReservationDto>>(entities);
        }
    }
}
