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
    public class UpdateCostCalculationCommandHandler : IRequestHandler<UpdateCostCalculationCommand, ServiceResponse<CostCalculationDto>>
    {
        private readonly ITourReservationPersonRepository _tourReservationPerson;
        private readonly ITourReservationRepository _tourReservationRepository;
        private readonly ICostCalculationRepository _costCalculationRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCostCalculationCommand> _logger;
        public UpdateCostCalculationCommandHandler(
           ITourReservationPersonRepository tourReservationPerson,
            IMapper mapper,
            IUnitOfWork<TourContext> uow,
            ILogger<CreateCostCalculationCommand> logger
,
            ITourReservationRepository tourReservationRepository,
            ICostCalculationRepository costCalculationRepository)
        {
            _tourReservationPerson = tourReservationPerson;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
            _tourReservationRepository = tourReservationRepository;
            _costCalculationRepository = costCalculationRepository;
        }
        public async Task<ServiceResponse<CostCalculationDto>> Handle(UpdateCostCalculationCommand request, CancellationToken cancellationToken)
        {
            var entity = _costCalculationRepository.FindBy(x => x.Id == request.Id).FirstOrDefault();

            if (entity == null)
            {
                return ServiceResponse<CostCalculationDto>.Return404("Böyle bir veri yok.");
            }

            entity.No = request.No;
            entity.RoomNumber = request.RoomNumber;
            entity.RoomType = request.RoomType;
            entity.GelenHavale = request.GelenHavale;
            entity.DitibDestek = request.DitibDestek;
            entity.SatisFiyati = request.SatisFiyati;
            entity.Maliyet = request.Maliyet;
            entity.AlisFiyat = request.AlisFiyat;
            entity.MaliyetToplam = request.MaliyetToplam;
            entity.TurKar = request.TurKar;
            entity.Description1 = request.Description1;
            entity.Description2 = request.Description2;

            _costCalculationRepository.Update(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<CostCalculationDto>.Return500();
            }

            var entityDto = _mapper.Map<CostCalculationDto>(entity);
            return ServiceResponse<CostCalculationDto>.ReturnResultWith200(entityDto);
        }
    }
}
