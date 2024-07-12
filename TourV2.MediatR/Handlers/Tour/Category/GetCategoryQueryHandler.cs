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
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, ServiceResponse<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var entity = await _categoryRepository.FindByInclude(x => x.Id == request.Id, i=>i.CategoryRecords).FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<CategoryDto>.ReturnResultWith200(_mapper.Map<CategoryDto>(entity));
            else
            {
                return ServiceResponse<CategoryDto>.Return404();
            }
        }
    }
}
