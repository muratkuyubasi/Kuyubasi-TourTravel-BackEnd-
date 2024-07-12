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

namespace TourV2.MediatR.Handlers
{
    public class UpdateTourReservationCommandHandler : IRequestHandler<UpdateTourReservationCommand, ServiceResponse<TourReservationDto>>
    {
        private readonly ITourReservationRepository _tourReservationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddTourReservationCommandHandler> _logger;
        private readonly UserInfoToken _userInfoToken;
        private readonly IUnitOfWork<TourContext> _uow;

        public UpdateTourReservationCommandHandler(ITourReservationRepository tourReservationRepository, IMapper mapper, ILogger<AddTourReservationCommandHandler> logger, UserInfoToken userInfoToken, IUnitOfWork<TourContext> uow)
        {
            _tourReservationRepository = tourReservationRepository;
            _mapper = mapper;
            _logger = logger;
            _userInfoToken = userInfoToken;
            _uow = uow;
        }

        public async Task<ServiceResponse<TourReservationDto>> Handle(UpdateTourReservationCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _tourReservationRepository.FindBy(x=>x.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<TourReservationDto>.Return409("Rezervasyon Bulunamadı");
            }
            entityExist.ActiveTourId = request.ActiveTourId;
            entityExist.ContactFirstName = request.ContactFirstName;
            entityExist.ContactLastName = request.ContactLastName;
            entityExist.ContactEmail = request.ContactEmail;
            entityExist.ContactPhone = request.ContactPhone;
            entityExist.Note = request.Note;
            entityExist.ContactStreet = request.ContactStreet;
            entityExist.ContactPostalCode = request.ContactPostalCode;
            entityExist.ContactDoorNumber = request.ContactDoorNumber;
            entityExist.ContactAddress = request.ContactAddress;
            entityExist.ContactCity = request.ContactCity;
            entityExist.ContactState = request.ContactState;
            entityExist.ContactCountry = request.ContactCountry;
            entityExist.InvoiceTitle = request.InvoiceTitle;
            entityExist.TaxNumber = request.TaxNumber;
            entityExist.TaxAdministration = request.TaxAdministration;
            entityExist.InvoiceStreet = request.InvoiceStreet;
            entityExist.InvoicePostalCode = request.InvoicePostalCode;
            entityExist.InvoiceDoorNumber = request.InvoiceDoorNumber;
            entityExist.InvoiceAddress = request.InvoiceAddress;
            entityExist.InvoiceCity = request.InvoiceCity;
            entityExist.InvoiceState = request.InvoiceState;
            entityExist.InvoiceCountry = request.InvoiceCountry;
            entityExist.ModifiedBy = Guid.Parse(_userInfoToken.Id);
            entityExist.ModifiedDate = DateTime.Now.ToLocalTime();

            _tourReservationRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Güncelleme Gerçekleşmedi");
                return ServiceResponse<TourReservationDto>.Return500();
            }

            var entityDto = _mapper.Map<TourReservationDto>(entityExist);
            return ServiceResponse<TourReservationDto>.ReturnResultWith200(entityDto);


        }
    }
}