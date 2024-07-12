using MediatR;
using System;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class ConfirmTourCommentCommand : IRequest<ServiceResponse<TourCommentDto>>
    {
        public Guid Id { get; set; }
    }
    public class SelectTourCommentCommand : IRequest<ServiceResponse<TourCommentDto>>
    {
        public Guid Id { get; set; }
    }
}