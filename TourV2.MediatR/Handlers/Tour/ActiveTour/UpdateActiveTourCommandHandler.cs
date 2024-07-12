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
    public class UpdateActiveTourCommandHandler : IRequestHandler<UpdateActiveTourCommand, ServiceResponse<ActiveTourDto>>
    {
        private readonly IActiveTourRepository _activeTourRepository;
        private readonly UserInfoToken _userInfoToken;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;

        public UpdateActiveTourCommandHandler(IActiveTourRepository activeTourRepository, UserInfoToken userInfoToken, IUnitOfWork<TourContext> uow, IMapper mapper)
        {
            _activeTourRepository = activeTourRepository;
            _userInfoToken = userInfoToken;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ActiveTourDto>> Handle(UpdateActiveTourCommand request, CancellationToken cancellationToken)
        {
            var entity = await _activeTourRepository.FindBy(x => x.Code == request.Code).FirstOrDefaultAsync();
            if (entity == null)
            {
                return ServiceResponse<ActiveTourDto>.Return409("Content not found.");
            }

            entity.TourRecordId = request.TourRecordId;
            entity.PeriodRecordId = request.PeriodRecordId;
            entity.RegionRecordId = request.RegionRecordId;
            entity.ShortDescription = request.ShortDescription;
            entity.Description = request.Description;
            entity.IsChild = request.IsChild;
            entity.ChildQuota = request.ChildQuota;
            entity.Quota = request.Quota;
            entity.DayCount = request.DayCount;
            entity.StartDate = request.StartDate;
            entity.EndDate = request.EndDate;
            entity.FinishDate = request.FinishDate;
            entity.IsActive = request.IsActive;
            entity.YoutubeLink= request.YoutubeLink;
            entity.ModifiedBy = Guid.Parse(_userInfoToken.Id);
            entity.ModifiedDate = DateTime.Now.ToLocalTime();
            entity.ShowCase = request.ShowCase;

            _activeTourRepository.Update(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<ActiveTourDto>.Return500();
            }

            var entityDto = _mapper.Map<ActiveTourDto>(entity);
            return ServiceResponse<ActiveTourDto>.ReturnResultWith200(entityDto);

        }
    }
}
