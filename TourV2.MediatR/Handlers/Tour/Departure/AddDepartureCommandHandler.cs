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
    public class AddDepartureCommandHandler : IRequestHandler<AddDepartureCommand, ServiceResponse<DepartureDto>>
    {
        private readonly IDepartureRepository _departureRepository;
        private readonly IDepartureRecordRepository _departureRecordRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        public AddDepartureCommandHandler(IDepartureRepository departureRepository, IDepartureRecordRepository departureRecordRepository, IMapper mapper, IUnitOfWork<TourContext> uow)
        {
            _departureRepository = departureRepository;
            _departureRecordRepository = departureRecordRepository;
            _uow = uow;
            _mapper = mapper;

        }

        public async Task<ServiceResponse<DepartureDto>> Handle(AddDepartureCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _departureRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                AddDepartureRecord(request.DepartureRecords, entityExist.Id);
                return ServiceResponse<DepartureDto>.Return409("Departure already exist.");
            }

            var entity = new Departure();
            entity.IsActive = true;

            _departureRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DepartureDto>.Return500();
            }
            AddDepartureRecord(request.DepartureRecords, entity.Id);

            var entityDto = _mapper.Map<DepartureDto>(entity);
            return ServiceResponse<DepartureDto>.ReturnResultWith200(entityDto);



        } 


        public async void AddDepartureRecord(List<DepartureRecordDto> recordEntities, int DepartureId)
        {
            foreach (var item in recordEntities)
            {
                var check = _departureRecordRepository.FindBy(x => x.DepartureId ==DepartureId && x.LanguageCode == item.LanguageCode).FirstOrDefault();
                if(check == null)
                {
                    var crEntity = new DepartureRecord
                    {
                        Code = Guid.NewGuid(),
                        Title = item.Title,
                        Slug = SeoSlug.SeoSlugCreator(item.Title),
                        LanguageCode = item.LanguageCode,
                        DepartureId = DepartureId,
                        IsActive = item.IsActive
                    };
                    _departureRecordRepository.Add(crEntity);

                    if (await _uow.SaveAsync() <= 0)
                    {
                        //return false;
                    }
                }

            }

        }
    }
}
