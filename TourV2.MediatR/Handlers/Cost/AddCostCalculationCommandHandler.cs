using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Common.UnitOfWork;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Data.Entities;
using TourV2.Domain;
using TourV2.Helper;
using TourV2.MediatR.Commands;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class AddCostCalculationCommandHandler : IRequestHandler<AddCostCalculationCommand, ServiceResponse<CostCalculationDto>>
    {
        private readonly ITourReservationPersonRepository _tourReservationPerson;
        private readonly ITourReservationRepository _tourReservationRepository;
        private readonly ICostCalculationRepository _costCalculationRepository;
        private readonly IActiveTourRepository _activeTourRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCostCalculationCommand> _logger;
        public AddCostCalculationCommandHandler(
           ITourReservationPersonRepository tourReservationPerson,
            IMapper mapper,
            IUnitOfWork<TourContext> uow,
            ILogger<CreateCostCalculationCommand> logger
,
            ITourReservationRepository tourReservationRepository,
            ICostCalculationRepository costCalculationRepository,
            IActiveTourRepository activeTourRepository)
        {
            _tourReservationPerson = tourReservationPerson;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
            _tourReservationRepository = tourReservationRepository;
            _costCalculationRepository = costCalculationRepository;
            _activeTourRepository = activeTourRepository;
        }
        public async Task<ServiceResponse<CostCalculationDto>> Handle(AddCostCalculationCommand request, CancellationToken cancellationToken)
        {
            var activeTour = _activeTourRepository.FindBy(x => x.Id == request.ActiveTourId).FirstOrDefault();

            if (activeTour == null)
            {
                return ServiceResponse<CostCalculationDto>.Return404("Aradığınız aktif tur bilgisine ulaşılamıyor.");
            }

            var entity = _mapper.Map<CostCalculation>(request);
            entity.TourStartDate = activeTour.StartDate ?? DateTime.Now;
            entity.TourEndDate = activeTour.EndDate ?? DateTime.Now;

            _costCalculationRepository.Add(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<CostCalculationDto>.Return500();
            }

            var entityDto = _mapper.Map<CostCalculationDto>(entity);
            return ServiceResponse<CostCalculationDto>.ReturnResultWith200(entityDto);
        }
    }
}
