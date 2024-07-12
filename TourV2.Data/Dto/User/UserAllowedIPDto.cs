using System;

namespace TourV2.Data.Dto
{
    public class UserAllowedIPDto
    {
        public Guid? UserId { get; set; }
        public string IPAddress { get; set; }
    }
}
