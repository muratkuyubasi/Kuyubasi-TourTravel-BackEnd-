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
    public class GetCategoryRecordQueryHandler: IRequestHandler<GetCategoryRecordQuery, ServiceResponse<CategoryRecordDto>>
    {
        private readonly ICategoryRecordRepository _categoryRecordRepository;
        private readonly IMapper _mapper;

        public GetCategoryRecordQueryHandler(ICategoryRecordRepository categoryRecordRepository, IMapper mapper)
        {
            _categoryRecordRepository = categoryRecordRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<CategoryRecordDto>> Handle(GetCategoryRecordQuery request ,CancellationToken cancellationToken)
        {
            var entity = await _categoryRecordRepository.FindBy(x => x.Code == request.Id).FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<CategoryRecordDto>.ReturnResultWith200(_mapper.Map<CategoryRecordDto>(entity));
            else
            {
                return ServiceResponse<CategoryRecordDto>.Return404();
            }
        }
    }
}
