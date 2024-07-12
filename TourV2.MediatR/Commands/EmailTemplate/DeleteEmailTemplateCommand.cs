using MediatR;
using System;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class DeleteEmailTemplateCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
