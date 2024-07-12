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
    public class UpdateRegionCommandHandler : IRequestHandler<UpdateRegionCommand, ServiceResponse<RegionRecordDto>>
    {
        private readonly IRegionRecordRepository _regionRecordRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;

        public UpdateRegionCommandHandler(IRegionRecordRepository regionRecordRepository, IUnitOfWork<TourContext> unitOfWork, IMapper mapper)
        {
            _regionRecordRepository = regionRecordRepository;
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<RegionRecordDto>> Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _regionRecordRepository.FindBy(x => x.Code == request.Code).FirstOrDefaultAsync();
            if(entityExist == null)
            {
                return ServiceResponse<RegionRecordDto>.Return409("Content not found.");
            }
            entityExist.Title = request.Title;
            entityExist.Slug = SeoSlug.SeoSlugCreator(request.Title);
            entityExist.LanguageCode = request.LanguageCode;

            _regionRecordRepository.Update(entityExist);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<RegionRecordDto>.Return500();
            }
            return ServiceResponse<RegionRecordDto>.ReturnResultWith200(_mapper.Map<RegionRecordDto>(entityExist));
        }

    }
}
