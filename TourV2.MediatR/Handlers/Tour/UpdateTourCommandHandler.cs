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
    public class UpdateTourCommandHandler : IRequestHandler<UpdateTourCommand, ServiceResponse<TourRecordDto>>
    {
        private readonly ITourRecordRepository _tourRecordRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;

        public UpdateTourCommandHandler(ITourRecordRepository tourRecordRepository, IUnitOfWork<TourContext> unitOfWork, IMapper mapper)
        {
            _tourRecordRepository = tourRecordRepository;
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TourRecordDto>> Handle(UpdateTourCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _tourRecordRepository.FindBy(x => x.Code == request.Code).FirstOrDefaultAsync();
            if(entityExist == null)
            {
                return ServiceResponse<TourRecordDto>.Return409("Content not found.");
            }
            entityExist.Title = request.Title;
            entityExist.Slug = SeoSlug.SeoSlugCreator(request.Title);
            entityExist.LanguageCode = request.LanguageCode;

            _tourRecordRepository.Update(entityExist);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<TourRecordDto>.Return500();
            }
            return ServiceResponse<TourRecordDto>.ReturnResultWith200(_mapper.Map<TourRecordDto>(entityExist));
        }

    }
}
