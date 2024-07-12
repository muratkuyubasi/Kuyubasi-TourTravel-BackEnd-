using TourV2.Data.Dto;
using MediatR;
using System;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class DeletePageCommand : IRequest<ServiceResponse<PageDto>>
    {
        public Guid Id { get; set; }
    }
}
