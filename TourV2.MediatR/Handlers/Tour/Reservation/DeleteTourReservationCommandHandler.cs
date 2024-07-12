using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Common.UnitOfWork;
using TourV2.Data.Dto;
using TourV2.Domain;
using TourV2.Helper;
using TourV2.MediatR.Commands;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers.Tour.Reservation
{
    public class DeleteTourReservationCommandHandler : IRequestHandler<DeleteTourReservationCommand, ServiceResponse<TourReservationDto>>
    {
        private readonly ITourReservationRepository _tourReservationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddTourReservationCommandHandler> _logger;
        private readonly UserInfoToken _userInfoToken;
        private readonly IUnitOfWork<TourContext> _uow;

        public DeleteTourReservationCommandHandler(ITourReservationRepository tourReservationRepository, IMapper mapper, ILogger<AddTourReservationCommandHandler> logger, UserInfoToken userInfoToken, IUnitOfWork<TourContext> uow)
        {
            _tourReservationRepository = tourReservationRepository;
            _mapper = mapper;
            _logger = logger;
            _userInfoToken = userInfoToken;
            _uow = uow;
        }

        public async Task<ServiceResponse<TourReservationDto>> Handle(DeleteTourReservationCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _tourReservationRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<TourReservationDto>.Return409("Rezervasyon Bulunamadı");
            }

            entityExist.DeletedBy = Guid.Parse(_userInfoToken.Id);
            entityExist.DeletedDate = DateTime.Now.ToLocalTime();
            entityExist.IsDeleted = true;

            _tourReservationRepository.Update(entityExist);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Silme İşlemi Gerçekleşmedi");
                return ServiceResponse<TourReservationDto>.Return500();
            }

            return ServiceResponse<TourReservationDto>.ReturnResultWith204();
        }
    }
}