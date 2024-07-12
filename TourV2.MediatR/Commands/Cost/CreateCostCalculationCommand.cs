using MediatR;
using System.Collections.Generic;
using TourV2.Data.Dto;
using TourV2.Helper;

namespace TourV2.MediatR.Commands
{
    public class CreateCostCalculationCommand : IRequest<ServiceResponse<List<CostCalculationDto>>>
    {
        public int ActiveTourId { get; set; }
    }
}