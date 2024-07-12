using System;

namespace TourV2.Data
{
    public class UserAllowedIP
    {
        public Guid UserId { get; set; }
        public string IPAddress { get; set; }
        public User User { get; set; }
    }
}
