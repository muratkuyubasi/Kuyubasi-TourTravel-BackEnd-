using System;
using System.ComponentModel;

namespace TourV2.Data
{
    public class ContactMessage
    {
        public Guid Id { get; set; }
        [DefaultValue(1)]
        public short Type { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
