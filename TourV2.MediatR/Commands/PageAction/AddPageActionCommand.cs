using TourV2.Data.Dto;
using MediatR;
using System;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class AddPageActionCommand: IRequest<ServiceResponse<PageActionDto>>
    {
        public Guid PageId { get; set; }
        public Guid ActionId { get; set; }
        public bool Flag { get; set; }
    }
}
