using TourV2.Data.Dto;
using MediatR;
using System;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class DeletePageActionCommand : IRequest<ServiceResponse<PageActionDto>>
    {
        public Guid Id { get; set; }
        public Guid PageId { get; set; }
        public Guid ActionId { get; set; }
    }
}
