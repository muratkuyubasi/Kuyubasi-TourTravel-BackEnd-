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
    public class CreateCostCalculationCommandHandler : IRequestHandler<CreateCostCalculationCommand, ServiceResponse<List<CostCalculationDto>>>
    {
        private readonly ITourReservationPersonRepository _tourReservationPerson;
        private readonly ITourReservationRepository _tourReservationRepository;
        private readonly ICostCalculationRepository _costCalculationRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCostCalculationCommand> _logger;
        public CreateCostCalculationCommandHandler(
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
        public async Task<ServiceResponse<List<CostCalculationDto>>> Handle(CreateCostCalculationCommand request, CancellationToken cancellationToken)
        {
            if (!_costCalculationRepository.FindBy(x => x.ActiveTourId == request.ActiveTourId).Any())
            {
                var reservationPersons = _tourReservationRepository.AllIncluding(x => x.ActiveTour).Include(x => x.ReservationPersons).ThenInclude(x => x.TourPrice).Where(x => x.ActiveTourId == request.ActiveTourId);

                if (!reservationPersons.Any())
                {
                    return ServiceResponse<List<CostCalculationDto>>.Return404("Rezervasyon listesi boş");
                }

                var costList = new List<CostCalculation>(); int sayac = 0;
                foreach (var item in reservationPersons)
                {
                    foreach (var person in item.ReservationPersons)
                    {
                        sayac++;

                        costList.Add(new CostCalculation
                        {
                            No = sayac,
                            TourStartDate = item.ActiveTour.StartDate ?? DateTime.Now,
                            TourEndDate = item.ActiveTour.EndDate ?? DateTime.Now,
                            RoomNumber = "0",
                            RoomType = 0,
                            Name = person.FirstName,
                            Surname = person.LastName,
                            BirthDate = person.BirthDay,
                            Gender = person.Gender,
                            GelenHavale = 0,
                            DitibDestek = 0,
                            SatisFiyati = person.TourPrice.Price,
                            Maliyet = 0,
                            AlisFiyat = 0,
                            MaliyetToplam = 0,
                            TurKar = 0,
                            ActiveTourId = request.ActiveTourId,
                            CreatedBy = Guid.Parse("a2d47445-b8e6-4e5a-ab35-15a5368fee87"),
                            ModifiedBy = Guid.Parse("a2d47445-b8e6-4e5a-ab35-15a5368fee87")
                        });
                    }
                }

                _costCalculationRepository.AddRange(costList);

                if (await _uow.SaveAsync() <= 0)
                {
                    return ServiceResponse<List<CostCalculationDto>>.Return500();
                }

                var entityDto = _mapper.Map<List<CostCalculationDto>>(_costCalculationRepository.FindBy(x => x.ActiveTourId == request.ActiveTourId));
                return ServiceResponse<List<CostCalculationDto>>.ReturnResultWith200(entityDto);
            }
            else
            {
                var entityDto = _mapper.Map<List<CostCalculationDto>>(_costCalculationRepository.FindBy(x => x.ActiveTourId == request.ActiveTourId));
                return ServiceResponse<List<CostCalculationDto>>.ReturnResultWith200(entityDto);
            }
        }
    }
}
