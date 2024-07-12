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
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryListDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryListDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _categoryRepository.AllIncluding(i => i.CategoryRecords)
                .Where(x=>x.CategoryRecords.Any(a=>a.LanguageCode == request.LanguageCode && x.IsActive))
                .Select(s => new CategoryListDto
                {
                    Id = s.Id,
                    IsActive = s.CategoryRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).IsActive,
                    CategoryRecordId = s.CategoryRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Id,
                    Code = s.CategoryRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Code,
                    Title = s.CategoryRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Title,
                    Slug = s.CategoryRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Slug,
                    ShowCase = s.CategoryRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).ShowCase,
                    Image = s.CategoryRecords.FirstOrDefault(x => x.LanguageCode == request.LanguageCode).Image,
                    LanguageCode = request.LanguageCode

                }).ToListAsync();
            return entities;
                
        }
    }
}
