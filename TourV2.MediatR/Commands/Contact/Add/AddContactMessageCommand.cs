using MediatR;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class AddContactMessageCommand : IRequest<ServiceResponse<ContactMessageDto>>
    {
        //public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
    }
    public class AddTourRequestCommand : IRequest<ServiceResponse<ContactMessageDto>>
    {
        //public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        //public string Subject { get; set; }
        public string Message { get; set; }
        //public bool IsActive { get; set; }
    }
}