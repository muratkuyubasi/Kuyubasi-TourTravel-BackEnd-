using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Helper;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetActiveTourQueryHandler : IRequestHandler<GetActiveTourQuery, ServiceResponse<ActiveTourDto>>
    {
        private readonly IActiveTourRepository _activeTourRepository;
        private readonly IMapper _mapper;
        public GetActiveTourQueryHandler(IActiveTourRepository activeTourRepository, IMapper mapper)
        {
            _activeTourRepository = activeTourRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ActiveTourDto>> Handle(GetActiveTourQuery request, CancellationToken cancellationToken)
        {
            var exitEntitiy = _activeTourRepository.FindByInclude(x => x.Id == request.Id,
                 i => i.TourRecord,
                    i => i.TourCategories,
                    i => i.TourDays,
                    i => i.TourTransportation,
                    i => i.TourMedias,
                    i => i.TourPrices,
                    i => i.PeriodRecord,
                    i => i.RegionRecord,
                    i => i.TourSpecifications,
                    i => i.TourCategories,
                    i => i.TourComments,
                    i => i.TourClicks
                )
                .Include(x => x.TourReservations)
                .ThenInclude(x => x.ReservationPersons)
                .Include(x => x.TourDepartures)

                .ThenInclude(x => x.DepartureRecord)
                .Include(x => x.PeriodRecord)

                .AsEnumerable()
                .Select(s=> new ActiveTourDto
                {
                    Id = s.Id,
                    Code = s.Code,
                    TourRecordId = s.TourRecordId,
                    PeriodRecordId  = s.PeriodRecordId,
                    RegionRecordId = s.RegionRecordId,
                    TourCategories  =s.TourCategories.ToList(),
                    ShortDescription = s.ShortDescription,
                    Description = s.Description,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    FinishDate = s.FinishDate,
                    TourTransportation = s.TourTransportation,
                    IsChild = s.IsChild,
                    ChildQuota = s.ChildQuota,
                    Quota = s.Quota,
                    DayCount = s.DayCount,
                    IsActive = s.IsActive,
                    ShowCase = s.ShowCase,
                    
                    TourRecord = s.TourRecord,
                    TourPrices = s.TourPrices.ToList(),
                    TourMedias = s.TourMedias.ToList(),
                    TourDays = s.TourDays.ToList(),
                    TourSpecifications = s.TourSpecifications.ToList(),
                    TourComments = s.TourComments.ToList(),
                    ReservationTotalPerson = s.TourReservations.Sum(x => x.ReservationPersons.Count()),
                    ClickCount = s.TourClicks.Count(),
                    TourClicks = s.TourClicks.ToList(),
                    YoutubeLink = s.YoutubeLink,
                    TourDepartures = s.TourDepartures.Select(d=>new TourDeparture
                    {
                        Id = d.Id,
                        DepartureTime = d.DepartureTime,
                        IsMain = d.IsMain,
                        DepartureRecord = d.DepartureRecord,
                        DepartureRecordId = d.DepartureRecordId,
                        ActiveTourId = d.ActiveTourId
                    }).ToList(),
                    PeriodRecord= s.PeriodRecord,
                    
                })
                .FirstOrDefault();
            if (exitEntitiy != null)
                return ServiceResponse<ActiveTourDto>.ReturnResultWith200(_mapper.Map<ActiveTourDto>(exitEntitiy));
            else
            {
                return ServiceResponse<ActiveTourDto>.Return404();
            }

        }
    }
}
