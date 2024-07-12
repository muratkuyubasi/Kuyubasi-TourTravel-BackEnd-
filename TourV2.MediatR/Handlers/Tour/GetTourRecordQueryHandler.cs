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
    public class GetTourRecordQueryHandler: IRequestHandler<GetTourRecordQuery, ServiceResponse<TourRecordDto>>
    {
        private readonly ITourRecordRepository _tourRecordRepository;
        private readonly IMapper _mapper;

        public GetTourRecordQueryHandler(ITourRecordRepository tourRecordRepository, IMapper mapper)
        {
            _tourRecordRepository = tourRecordRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TourRecordDto>> Handle(GetTourRecordQuery request ,CancellationToken cancellationToken)
        {
            var entity = await _tourRecordRepository.FindBy(x => x.Code == request.Id).FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<TourRecordDto>.ReturnResultWith200(_mapper.Map<TourRecordDto>(entity));
            else
            {
                return ServiceResponse<TourRecordDto>.Return404();
            }
        }
    }
}
