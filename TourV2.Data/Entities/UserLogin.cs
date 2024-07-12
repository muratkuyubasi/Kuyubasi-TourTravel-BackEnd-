using Microsoft.AspNetCore.Identity;
using System;

namespace TourV2.Data
{
    public class UserLogin : IdentityUserLogin<Guid>
    {
        public virtual User User { get; set; }
    }
}
