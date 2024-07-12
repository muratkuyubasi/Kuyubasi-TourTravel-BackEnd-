using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Common.UnitOfWork;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Domain;
using TourV2.Helper;
using TourV2.MediatR.Commands;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, ServiceResponse<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryRecordRepository _categoryRecordRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        public AddCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryRecordRepository categoryRecordRepository, IMapper mapper, IUnitOfWork<TourContext> uow)
        {
            _categoryRepository = categoryRepository;
            _categoryRecordRepository = categoryRecordRepository;
            _uow = uow;
            _mapper = mapper;

        }

        public async Task<ServiceResponse<CategoryDto>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _categoryRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                AddCategoryRecord(request.CategoryRecords, entityExist.Id);
                return ServiceResponse<CategoryDto>.Return409("Category already exist.");
            }
            
                var entity = new Category();
            if (request.CategoryRecords != null)
            {
                entity.IsActive = true;

                _categoryRepository.Add(entity);

                if (await _uow.SaveAsync() <= 0)
                {
                    return ServiceResponse<CategoryDto>.Return500();
                }
           
                AddCategoryRecord(request.CategoryRecords, entity.Id);
            }
            else
            {
                return ServiceResponse<CategoryDto>.Return409("Error.");
            }

            var entityDto = _mapper.Map<CategoryDto>(entity);
            return ServiceResponse<CategoryDto>.ReturnResultWith200(entityDto);



        } 


        public async void AddCategoryRecord(List<CategoryRecordDto> recordEntities, int categoryId)
        {
            foreach (var item in recordEntities)
            {
                var check = _categoryRecordRepository.FindBy(x => x.CategoryId ==categoryId && x.LanguageCode == item.LanguageCode).FirstOrDefault();
                if(check == null)
                {
                    var crEntity = new CategoryRecord
                    {
                        Code = Guid.NewGuid(),
                        Title = item.Title,
                        Slug = SeoSlug.SeoSlugCreator(item.Title),
                        LanguageCode = item.LanguageCode,
                        CategoryId = categoryId,
                        IsActive = item.IsActive
                    };
                    _categoryRecordRepository.Add(crEntity);

                    if (await _uow.SaveAsync() <= 0)
                    {
                        //return false;
                    }
                }

            }

        }
    }
}
