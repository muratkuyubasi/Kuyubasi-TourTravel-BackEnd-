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
    public class GetInfoTourReservationForPDFQueryHandler : IRequestHandler<GetInfoTourReservartionForPDFQuery, ServiceResponse<TourReservationPdfInfo>>
    {
        private readonly ITourReservationRepository _tourReservationRepository;
        private readonly IEmailSMTPSettingRepository _emailSMTPSettingRepository;
        private readonly IMapper _mapper;

        public GetInfoTourReservationForPDFQueryHandler(
            ITourReservationRepository tourReservationRepository,
            IMapper mapper,
            IEmailSMTPSettingRepository emailSMTPSettingRepository)
        {
            _tourReservationRepository = tourReservationRepository;
            _mapper = mapper;
            _emailSMTPSettingRepository = emailSMTPSettingRepository;
        }
        public async Task<ServiceResponse<TourReservationPdfInfo>> Handle(GetInfoTourReservartionForPDFQuery request, CancellationToken cancellationToken)
        {
            var entity = await _tourReservationRepository.FindByInclude(x => x.Id == request.Id, i => i.ReservationPersons
            , t => t.ActiveTour.TourRecord, tp => tp.ActiveTour.PeriodRecord, tpp => tpp.ActiveTour.TourPrices).Include(x => x.ReservationPersons).ThenInclude(y => y.TourPrice).FirstOrDefaultAsync();

            var defaultSmtp = await _emailSMTPSettingRepository.FindBy(c => c.IsDefault).FirstOrDefaultAsync();

            string persons = "";
            foreach (var item in entity.ReservationPersons)
            {
                persons += item.FirstName + " " + item.LastName + " " + item.BirthDay.ToString("dd.MM.yyyy") + "\r\n";
            }
            int startBillNumber = 5000 + (_tourReservationRepository.AllIncluding().Count() + 1);
            var result = new TourReservationPdfInfo()
            {
                TourInfo = entity.ActiveTour.TourRecord.Title + " " + entity.ActiveTour.PeriodRecord.Title,
                TourName = entity.ActiveTour.TourRecord.Title,
                Fullname = entity.ContactFirstName + " " + entity.ContactLastName,
                ReservationPersons = persons,
                Address = entity.ContactAddress + " " + entity.ContactDoorNumber + "\r\n" + entity.ContactPostalCode + " " + entity.ContactCity + "\r\n" + entity.ContactCountry,
                BillNo = "RE-0" + startBillNumber,
                ProductCode = entity.ReservationCode,
                TicketDate = entity.CreatedDate.ToString("dd.MM.yyyy"),
                PaymentDate = entity.CreatedDate.AddDays(30).ToString("dd.MM.yyyy"),
                Quantity = entity.ReservationPersons.Count().ToString(),
                QuantityPrice = entity.ActiveTour.TourPrices.FirstOrDefault(x => x.IsChildPrice == false).Price.ToString(),
                QuantityTotalPrice = (entity.ReservationPersons.Sum(x => x.TourPrice.Price)).ToString(),
                TotalPrice = entity.TotalAmount.ToString(),
                ExtraPrice = entity.ReservationPersons.Count() < 2 ? entity.ActiveTour.TourPrices.FirstOrDefault(x => x.IsChildPrice == false).ExtraPrice.ToString() : "0",
                DefaultSmtp = defaultSmtp
            };

            if (entity != null)
                return ServiceResponse<TourReservationPdfInfo>.ReturnResultWith200(result);
            else
            {
                return ServiceResponse<TourReservationPdfInfo>.Return404();
            }
        }
    }
}
