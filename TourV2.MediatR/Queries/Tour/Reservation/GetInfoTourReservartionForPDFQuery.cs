using MediatR;
using System;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Queries
{
    public class GetInfoTourReservartionForPDFQuery : IRequest<ServiceResponse<TourReservationPdfInfo>>
    {
        public int Id { get; set; }
    }
    public class TourReservationPdfInfo
    {
        public string TourName { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string BillNo { get; set; }
        public string TourInfo { get; set; }
        public string TicketDate { get; set; }
        public string PaymentDate { get; set; }
        public string ProductCode { get; set; }
        public string ReservationPersons { get; set; }
        public string Quantity { get; set; }
        public string QuantityPrice { get; set; }
        public string ExtraPrice { get; set; }
        public string QuantityTotalPrice { get; set; }
        public string TotalPrice { get; set; }
        public EmailSMTPSetting DefaultSmtp { get; set; }
    }
}