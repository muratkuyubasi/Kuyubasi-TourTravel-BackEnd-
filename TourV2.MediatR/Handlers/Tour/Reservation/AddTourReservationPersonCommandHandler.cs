using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Common.UnitOfWork;
using TourV2.Data.Dto;
using TourV2.Data;
using TourV2.Domain;
using TourV2.Helper;
using TourV2.MediatR.Commands;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers.Tour.Reservation
{
    public class AddTourReservationPersonCommandHandler : IRequestHandler<AddTourReservationPersonCommand, ServiceResponse<TourReservationPersonDto>>
    {
        private readonly ITourReservationPersonRepository _tourReservationPersonRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddTourReservationPersonCommandHandler> _logger;
        private readonly UserInfoToken _userInfoToken;
        private readonly IUnitOfWork<TourContext> _uow;

        public AddTourReservationPersonCommandHandler(ITourReservationPersonRepository tourReservationPersonRepository, IUnitOfWork<TourContext> uow, IMapper mapper, ILogger<AddTourReservationPersonCommandHandler> logger, UserInfoToken userInfoToken)
        {
            _tourReservationPersonRepository = tourReservationPersonRepository;
            _mapper = mapper;
            _logger = logger;
            _userInfoToken = userInfoToken;
            _uow = uow;
        }

        public async Task<ServiceResponse<TourReservationPersonDto>> Handle(AddTourReservationPersonCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TourReservationPerson>(request);

            _tourReservationPersonRepository.Add(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Kayıt Gerçekleşmedi");
                return ServiceResponse<TourReservationPersonDto>.Return500();
            }

            var entityDto = _mapper.Map<TourReservationPersonDto>(entity);
            return ServiceResponse<TourReservationPersonDto>.ReturnResultWith200(entityDto);



        }
    }
}