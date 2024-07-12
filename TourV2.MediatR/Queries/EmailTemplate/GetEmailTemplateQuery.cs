using MediatR;
using System;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Queries
{
    public class GetEmailTemplateQuery : IRequest<ServiceResponse<EmailTemplateDto>>
    {
        public Guid Id { get; set; }
    }
}
