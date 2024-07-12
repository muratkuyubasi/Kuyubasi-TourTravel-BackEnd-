using System;
using System.Collections.Generic;
using TourV2.Data;

namespace TourV2.Data.Dto
{
    public class TourReservationDto
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public int ActiveTourId { get; set; }
        public string ReservationCode { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Note { get; set; }
        public string ContactStreet { get; set; }
        public string ContactPostalCode { get; set; }
        public string ContactDoorNumber { get; set; }
        public string ContactAddress { get; set; }
        public string ContactCity { get; set; }
        public string ContactState { get; set; }
        public string ContactCountry { get; set; }
        public string InvoiceTitle { get; set; }
        public string TaxNumber { get; set; }
        public string TaxAdministration { get; set; }
        public string InvoiceStreet { get; set; }
        public string InvoicePostalCode { get; set; }
        public string InvoiceDoorNumber { get; set; }
        public string InvoiceAddress { get; set; }
        public string InvoiceCity { get; set; }
        public string InvoiceState { get; set; }
        public string InvoiceCountry { get; set; }
        public string Uyruk { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AdvancedPayment { get; set; } = 0;
        public decimal AvaliableBalance { get; set; }
        public bool IsCompleted { get; set; } = false;
        public bool IsPayment { get; set; } = false; 
        public bool IsDifferentInvoice { get; set; }
        public List<TourReservationPerson> ReservationPersons { get; set; }
        public ActiveTour ActiveTour { get; set; }
    }
}
