using MediatR;
using System;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class AddTourCommentCommand : IRequest<ServiceResponse<TourCommentDto>>
    {
        //public Guid Id { get; set; }
        public int ActiveTourId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public short? Point { get; set; }
        //public DateTime CreationDate { get; set; }
        //public DateTime? ModifiedDate { get; set; }
        //public Guid? ModifiedBy { get; set; }
        //public bool IsActive { get; set; }
        //public virtual TourMain TourMain { get; set; }
    }
}