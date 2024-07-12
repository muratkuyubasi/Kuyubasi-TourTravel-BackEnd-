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
    public class AddPeriodCommandHandler : IRequestHandler<AddPeriodCommand, ServiceResponse<PeriodDto>>
    {
        private readonly IPeriodRepository _PeriodRepository;
        private readonly IPeriodRecordRepository _PeriodRecordRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        public AddPeriodCommandHandler(IPeriodRepository PeriodRepository, IPeriodRecordRepository PeriodRecordRepository, IMapper mapper, IUnitOfWork<TourContext> uow)
        {
            _PeriodRepository = PeriodRepository;
            _PeriodRecordRepository = PeriodRecordRepository;
            _uow = uow;
            _mapper = mapper;

        }

        public async Task<ServiceResponse<PeriodDto>> Handle(AddPeriodCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _PeriodRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                AddPeriodRecord(request.PeriodRecords, entityExist.Id);
                return ServiceResponse<PeriodDto>.Return409("Period already exist.");
            }

            var entity = new Period();
            if (request.PeriodRecords != null)
            {
                entity.IsActive = true;

                _PeriodRepository.Add(entity);
                if (await _uow.SaveAsync() <= 0)
                {
                    return ServiceResponse<PeriodDto>.Return500();
                }
                AddPeriodRecord(request.PeriodRecords, entity.Id);
            }
            else
            {
                return ServiceResponse<PeriodDto>.Return409("Error.");
            }
            

            var entityDto = _mapper.Map<PeriodDto>(entity);
            return ServiceResponse<PeriodDto>.ReturnResultWith200(entityDto);



        } 


        public async void AddPeriodRecord(List<PeriodRecordDto> recordEntities, int PeriodId)
        {
            foreach (var item in recordEntities)
            {
                var check = _PeriodRecordRepository.FindBy(x => x.PeriodId ==PeriodId && x.LanguageCode == item.LanguageCode).FirstOrDefault();
                if(check == null)
                {
                    var crEntity = new PeriodRecord
                    {
                        Code = Guid.NewGuid(),
                        Title = item.Title,
                        Slug = SeoSlug.SeoSlugCreator(item.Title),
                        LanguageCode = item.LanguageCode,
                        PeriodId = PeriodId,
                        IsActive = true,
                    };
                    _PeriodRecordRepository.Add(crEntity);

                    if (await _uow.SaveAsync() <= 0)
                    {
                        //return false;
                    }
                }

            }

        }
    }
}
