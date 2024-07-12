using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands.EducaitonForm
{
    public class AddEducationFormCommand : IRequest<ServiceResponse<EducationFormDTO>>
    {
        public int PersonType { get; set; }
        public int Gender { get; set; }
        public int Nationality { get; set; }
        public int StateId { get; set; }
        public int MosqueId { get; set; }
        public int Airport { get; set; }
        public bool IsPrice { get; set; }

        public string ThePersonWhoRecorded { get; set; }
        public string RegistrantPhone { get; set; }

        public string StudentNameSurname { get; set; }
        public string Studentbirthdate { get; set; }
        public int StudentGender { get; set; }
        public string StudentMail { get; set; }

        public string StudentPassportNumber { get; set; }
        public string StudentIdentificationNumber { get; set; }

        public string StudentAddress { get; set; }
        public string StudentMobilePhoneNumberGermany { get; set; }
        public string StudentMobilePhoneNumberTurkey { get; set; }
        public string TransferFullName { get; set; }
        public string TranferDate { get; set; }
        public string TransferTransactionNumber { get; set; }
        public string StudentFatherNameSurname { get; set; }
        public string StudentFatherPhone { get; set; }
        public string StudentMotherNameSurname { get; set; }
        public string StudentMotherPhone { get; set; }
        public string MosqueReligiousOfficialFullName { get; set; }
        public string MosqueReligiousOfficialPhone { get; set; }

        public string? PasaportPath { get; set; }
        public string? IdentificationPath { get; set; }
        public string? ReceiptPath { get; set; }


    }
}
