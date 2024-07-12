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
    public class AddRegionCommandHandler : IRequestHandler<AddRegionCommand, ServiceResponse<RegionDto>>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IRegionRecordRepository _regionRecordRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        public AddRegionCommandHandler(IRegionRepository regionRepository, IRegionRecordRepository regionRecordRepository, IMapper mapper, IUnitOfWork<TourContext> uow)
        {
            _regionRepository = regionRepository;
            _regionRecordRepository = regionRecordRepository;
            _uow = uow;
            _mapper = mapper;

        }

        public async Task<ServiceResponse<RegionDto>> Handle(AddRegionCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _regionRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                AddRegionRecord(request.RegionRecords, entityExist.Id);
                return ServiceResponse<RegionDto>.Return409("Region already exist.");
            }

            var entity = new Region();
            entity.IsActive = true;

            _regionRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<RegionDto>.Return500();
            }
            AddRegionRecord(request.RegionRecords, entity.Id);

            var entityDto = _mapper.Map<RegionDto>(entity);
            return ServiceResponse<RegionDto>.ReturnResultWith200(entityDto);



        } 


        public async void AddRegionRecord(List<RegionRecordDto> recordEntities, int RegionId)
        {
            foreach (var item in recordEntities)
            {
                var check = _regionRecordRepository.FindBy(x => x.RegionId ==RegionId && x.LanguageCode == item.LanguageCode).FirstOrDefault();
                if(check == null)
                {
                    var crEntity = new RegionRecord
                    {
                        Code = Guid.NewGuid(),
                        Title = item.Title,
                        Slug = SeoSlug.SeoSlugCreator(item.Title),
                        LanguageCode = item.LanguageCode,
                        RegionId = RegionId,
                        IsActive = true,
                    };
                    _regionRecordRepository.Add(crEntity);

                    if (await _uow.SaveAsync() <= 0)
                    {
                        //return false;
                    }
                }

            }

        }
    }
}
