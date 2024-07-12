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
    public class DeletePeriodCommandHandler : IRequestHandler<DeletePeriodCommand, ServiceResponse<PeriodDto>>
    {
        private readonly IPeriodRepository _PeriodRepository;
        private readonly IUnitOfWork<TourContext> _uow;

        public DeletePeriodCommandHandler(IPeriodRepository PeriodRepository, IUnitOfWork<TourContext> uow)
        {
            _PeriodRepository = PeriodRepository;
            _uow = uow;
        }

        public async Task<ServiceResponse<PeriodDto>> Handle(DeletePeriodCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _PeriodRepository.FindBy(x=>x.Id ==request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<PeriodDto>.Return404();
            }
            _PeriodRepository.Remove(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<PeriodDto>.Return500();
            }
            return ServiceResponse<PeriodDto>.ReturnResultWith204();
        }
    }
}
