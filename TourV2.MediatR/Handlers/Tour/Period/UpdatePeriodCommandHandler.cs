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
    public class UpdatePeriodCommandHandler : IRequestHandler<UpdatePeriodCommand, ServiceResponse<PeriodRecordDto>>
    {
        private readonly IPeriodRecordRepository _periodRecordRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;

        public UpdatePeriodCommandHandler(IPeriodRecordRepository periodRecordRepository, IUnitOfWork<TourContext> unitOfWork, IMapper mapper)
        {
            _periodRecordRepository = periodRecordRepository;
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<PeriodRecordDto>> Handle(UpdatePeriodCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _periodRecordRepository.FindBy(x => x.Code == request.Code).FirstOrDefaultAsync();
            if(entityExist == null)
            {
                return ServiceResponse<PeriodRecordDto>.Return409("Content not found.");
            }
            entityExist.Title = request.Title;
            entityExist.Slug = SeoSlug.SeoSlugCreator(request.Title);
            entityExist.LanguageCode = request.LanguageCode;
            entityExist.IsActive = request.IsActive;

            _periodRecordRepository.Update(entityExist);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<PeriodRecordDto>.Return500();
            }
            return ServiceResponse<PeriodRecordDto>.ReturnResultWith200(_mapper.Map<PeriodRecordDto>(entityExist));
        }

    }
}
