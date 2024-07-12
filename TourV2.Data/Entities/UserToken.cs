using Microsoft.AspNetCore.Identity;
using System;

namespace TourV2.Data
{
    public class UserToken : IdentityUserToken<Guid>
    {
        public virtual User User { get; set; } = null;
    }
}
