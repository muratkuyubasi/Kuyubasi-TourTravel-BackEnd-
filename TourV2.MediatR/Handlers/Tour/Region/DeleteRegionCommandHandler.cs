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
    public class DeleteRegionCommandHandler : IRequestHandler<DeleteRegionCommand, ServiceResponse<RegionDto>>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IUnitOfWork<TourContext> _uow;

        public DeleteRegionCommandHandler(IRegionRepository regionRepository, IUnitOfWork<TourContext> uow)
        {
            _regionRepository = regionRepository;
            _uow = uow;
        }

        public async Task<ServiceResponse<RegionDto>> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _regionRepository.FindBy(x=>x.Id ==request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<RegionDto>.Return404();
            }
            _regionRepository.Remove(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<RegionDto>.Return500();
            }
            return ServiceResponse<RegionDto>.ReturnResultWith204();
        }
    }
}
