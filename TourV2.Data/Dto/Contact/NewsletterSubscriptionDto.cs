using System;

namespace TourV2.Data.Dto
{
    public class NewsletterSubscriptionDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; }
        public bool? isVerified { get; set; }
        public int? Counter { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
