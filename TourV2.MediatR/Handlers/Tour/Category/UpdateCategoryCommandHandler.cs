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
using TourV2.Data.Dto;
using TourV2.Domain;
using TourV2.Helper;
using TourV2.MediatR.Commands;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ServiceResponse<CategoryRecordDto>>
    {
        private readonly ICategoryRecordRepository _categoryRecordRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRecordRepository categoryRecordRepository, IUnitOfWork<TourContext> unitOfWork, IMapper mapper)
        {
            _categoryRecordRepository = categoryRecordRepository;
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<CategoryRecordDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _categoryRecordRepository.FindBy(x => x.Code == request.Code).FirstOrDefaultAsync();
            if(entityExist == null)
            {
                return ServiceResponse<CategoryRecordDto>.Return409("Content not found.");
            }
            entityExist.Title = request.Title;
            entityExist.Slug = SeoSlug.SeoSlugCreator(request.Title);
            entityExist.LanguageCode = request.LanguageCode;
            entityExist.IsActive = request.IsActive;
            entityExist.ShowCase = request.ShowCase;
            _categoryRecordRepository.Update(entityExist);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<CategoryRecordDto>.Return500();
            }
            return ServiceResponse<CategoryRecordDto>.ReturnResultWith200(_mapper.Map<CategoryRecordDto>(entityExist));
        }

    }
}
