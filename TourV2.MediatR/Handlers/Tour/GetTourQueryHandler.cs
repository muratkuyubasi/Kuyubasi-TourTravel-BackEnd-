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
    public class GetTourQueryHandler : IRequestHandler<GetTourQuery, ServiceResponse<TourDto>>
    {
        private readonly ITourRepository _tourRepository;
        private readonly IMapper _mapper;

        public GetTourQueryHandler(ITourRepository tourRepository, IMapper mapper)
        {
            _tourRepository = tourRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TourDto>> Handle(GetTourQuery request, CancellationToken cancellationToken)
        {
            var entity = await _tourRepository.FindByInclude(x => x.Id == request.Id, i=>i.TourRecords).FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<TourDto>.ReturnResultWith200(_mapper.Map<TourDto>(entity));
            else
            {
                return ServiceResponse<TourDto>.Return404();
            }
        }
    }
}
