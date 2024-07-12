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
    public class AddTourCommandHandler : IRequestHandler<AddTourCommand, ServiceResponse<TourDto>>
    {
        private readonly ITourRepository _tourRepository;
        private readonly ITourRecordRepository _tourRecordRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        public AddTourCommandHandler(ITourRepository tourRepository, ITourRecordRepository tourRecordRepository, IMapper mapper, IUnitOfWork<TourContext> uow)
        {
            _tourRepository = tourRepository;
            _tourRecordRepository = tourRecordRepository;
            _uow = uow;
            _mapper = mapper;

        }

        public async Task<ServiceResponse<TourDto>> Handle(AddTourCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _tourRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                AddTourRecord(request.TourRecords, entityExist.Id);
                return ServiceResponse<TourDto>.Return409("Tour already exist.");
            }

            var entity = new TourV2.Data.Tour();
            entity.IsActive = true;
            entity.Code = Guid.NewGuid();

            _tourRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<TourDto>.Return500();
            }
            AddTourRecord(request.TourRecords, entity.Id);

            var entityDto = _mapper.Map<TourDto>(entity);
            return ServiceResponse<TourDto>.ReturnResultWith200(entityDto);



        } 


        public async void AddTourRecord(List<TourRecordDto> recordEntities, int TourId)
        {
            foreach (var item in recordEntities)
            {
                var check = _tourRecordRepository.FindBy(x => x.TourId ==TourId && x.LanguageCode == item.LanguageCode).FirstOrDefault();
                if(check == null)
                {
                    var crEntity = new TourRecord
                    {
                        Code = Guid.NewGuid(),
                        Title = item.Title,
                        Slug = SeoSlug.SeoSlugCreator(item.Title),
                        LanguageCode = item.LanguageCode,
                        TourId = TourId,
                        IsActive = true,
                    };
                    _tourRecordRepository.Add(crEntity);

                    if (await _uow.SaveAsync() <= 0)
                    {
                        //return false;
                    }
                }

            }

        }
    }
}
