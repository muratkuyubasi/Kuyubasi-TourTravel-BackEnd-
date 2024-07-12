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
    public class UpdateAllCostCalculationCommandHandler : IRequestHandler<UpdateAllCostCalculationCommand, ServiceResponse<CostCalculationDto>>
    {
        private readonly ITourReservationPersonRepository _tourReservationPerson;
        private readonly ITourReservationRepository _tourReservationRepository;
        private readonly ICostCalculationRepository _costCalculationRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAllCostCalculationCommand> _logger;
        public UpdateAllCostCalculationCommandHandler(
           ITourReservationPersonRepository tourReservationPerson,
            IMapper mapper,
            IUnitOfWork<TourContext> uow,
            ILogger<UpdateAllCostCalculationCommand> logger
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
        public async Task<ServiceResponse<CostCalculationDto>> Handle(UpdateAllCostCalculationCommand request, CancellationToken cancellationToken)
        {
            //foreach (var item in request)
            //{
            //    var entity = _costCalculationRepository.FindBy(x => x.Id == item.Id).FirstOrDefault();
            //    entity.No = item.No;
            //    entity.RoomNumber = item.RoomNumber;
            //    entity.RoomType = item.RoomType;
            //    entity.GelenHavale = item.GelenHavale;
            //    entity.DitibDestek = item.DitibDestek;
            //    entity.SatisFiyati = item.SatisFiyati;
            //    entity.Maliyet = item.Maliyet;
            //    entity.AlisFiyat = item.AlisFiyat;
            //    entity.MaliyetToplam = item.MaliyetToplam;
            //    entity.TurKar = item.TurKar;
            //    entity.Description1 = item.Description1;
            //    entity.Description2 = item.Description2;

            //    _costCalculationRepository.Update(entity);
            //}

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<CostCalculationDto>.Return500();
            }

            return ServiceResponse<CostCalculationDto>.ReturnResultWith204();
        }
    }
}
