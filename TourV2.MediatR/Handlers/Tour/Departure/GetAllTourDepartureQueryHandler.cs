using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetAllTourDepartureQueryHandler : IRequestHandler<GetAllTourDepartureQuery, List<DepartureListDto>>
    {
        private readonly ITourDepartureRepository _tourDepartureRepository;
        public GetAllTourDepartureQueryHandler(ITourDepartureRepository tourDepartureRepository)
        {
            _tourDepartureRepository = tourDepartureRepository;
        }

        public async Task<List<DepartureListDto>> Handle(GetAllTourDepartureQuery request, CancellationToken cancellationToken)
        {

            var entities = await _tourDepartureRepository.AllIncluding(i => i.DepartureRecord)
                .Where(x => x.ActiveTourId == request.ActiveTourId)
                .Select(s => new DepartureListDto
                {
                    Id = s.Id,
                    DepartureRecordId = s.DepartureRecordId,
                    Title = s.DepartureRecord.Title,
                    IsMain = s.IsMain,
                    DepartureTime = s.DepartureTime,

                }).ToListAsync();

            return entities;

        }
    }
}
