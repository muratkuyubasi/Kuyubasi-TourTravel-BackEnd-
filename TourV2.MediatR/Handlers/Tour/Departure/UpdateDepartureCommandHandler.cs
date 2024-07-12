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
    public class UpdateDepartureCommandHandler : IRequestHandler<UpdateDepartureCommand, ServiceResponse<DepartureRecordDto>>
    {
        private readonly IDepartureRecordRepository _departureRecordRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;

        public UpdateDepartureCommandHandler(IDepartureRecordRepository departureRecordRepository, IUnitOfWork<TourContext> unitOfWork, IMapper mapper)
        {
            _departureRecordRepository = departureRecordRepository;
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<DepartureRecordDto>> Handle(UpdateDepartureCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _departureRecordRepository.FindBy(x => x.Code == request.Code).FirstOrDefaultAsync();
            if(entityExist == null)
            {
                return ServiceResponse<DepartureRecordDto>.Return409("Content not found.");
            }
            entityExist.Title = request.Title;
            entityExist.Slug = SeoSlug.SeoSlugCreator(request.Title);
            entityExist.LanguageCode = request.LanguageCode;
            entityExist.IsActive = request.IsActive;

            _departureRecordRepository.Update(entityExist);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DepartureRecordDto>.Return500();
            }
            return ServiceResponse<DepartureRecordDto>.ReturnResultWith200(_mapper.Map<DepartureRecordDto>(entityExist));
        }

    }
}
