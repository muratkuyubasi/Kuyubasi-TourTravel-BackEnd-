using Microsoft.AspNetCore.Identity;
using System;

namespace TourV2.Data
{
    public class RoleClaim : IdentityRoleClaim<Guid>
    {
        public Guid ActionId { get; set; }
        public Guid PageId { get; set; }
        public virtual Role Role { get; set; }
        public Action Action { get; set; }
        public Page Page { get; set; }
    }
}
