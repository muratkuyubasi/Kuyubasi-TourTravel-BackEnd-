using System;

namespace TourV2.Data
{
    public class NewsletterSubscription
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public bool? isVerified { get; set; }
        public int? Counter { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
