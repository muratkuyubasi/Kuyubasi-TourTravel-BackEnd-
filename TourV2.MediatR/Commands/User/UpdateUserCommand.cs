using TourV2.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class UpdateUserCommand : IRequest<ServiceResponse<UserDto>>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Keywords { get; set; }
        public string Biblio { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Linkedin { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        public List<UserAllowedIPDto> UserAllowedIPs { get; set; }
        public List<UserRoleDto> UserRoles { get; set; } = new List<UserRoleDto>();
    }
}
