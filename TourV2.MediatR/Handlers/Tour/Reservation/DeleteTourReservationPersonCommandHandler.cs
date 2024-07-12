using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class DeleteTourReservationPersonCommandHandler : IRequestHandler<DeleteTourReservationPersonCommand, ServiceResponse<TourReservationPersonDto>>
    {
        private readonly ITourReservationPersonRepository _tourReservationPersonRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddTourReservationPersonCommandHandler> _logger;
        private readonly UserInfoToken _userInfoToken;
        private readonly IUnitOfWork<TourContext> _uow;

        public DeleteTourReservationPersonCommandHandler(ITourReservationPersonRepository tourReservationPersonRepository, IMapper mapper, ILogger<AddTourReservationPersonCommandHandler> logger, UserInfoToken userInfoToken, IUnitOfWork<TourContext> uow)
        {
            _tourReservationPersonRepository = tourReservationPersonRepository;
            _mapper = mapper;
            _logger = logger;
            _userInfoToken = userInfoToken;
            _uow = uow;
        }

        public async Task<ServiceResponse<TourReservationPersonDto>> Handle(DeleteTourReservationPersonCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _tourReservationPersonRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<TourReservationPersonDto>.Return409("Rezervasyon Bulunamadı");
            }

            //Silinecek mi?
            _tourReservationPersonRepository.Remove(entityExist);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Silme İşlemi Gerçekleşmedi");
                return ServiceResponse<TourReservationPersonDto>.Return500();
            }

            return ServiceResponse<TourReservationPersonDto>.ReturnResultWith204();
        }
    }
}