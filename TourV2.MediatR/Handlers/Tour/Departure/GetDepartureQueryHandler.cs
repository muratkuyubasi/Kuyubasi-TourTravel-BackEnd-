using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Helper;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetDepartureQueryHandler : IRequestHandler<GetDepartureQuery, ServiceResponse<DepartureDto>>
    {
        private readonly IDepartureRepository _departureRepository;
        private readonly IMapper _mapper;

        public GetDepartureQueryHandler(IDepartureRepository departureRepository, IMapper mapper)
        {
            _departureRepository = departureRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<DepartureDto>> Handle(GetDepartureQuery request, CancellationToken cancellationToken)
        {
            var entity = await _departureRepository.FindByInclude(x => x.Id == request.Id, i=>i.DepartureRecords).FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<DepartureDto>.ReturnResultWith200(_mapper.Map<DepartureDto>(entity));
            else
            {
                return ServiceResponse<DepartureDto>.Return404();
            }
        }
    }
}
