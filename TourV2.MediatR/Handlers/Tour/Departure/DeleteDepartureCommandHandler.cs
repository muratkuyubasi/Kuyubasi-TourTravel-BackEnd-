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
    public class DeleteDepartureCommandHandler : IRequestHandler<DeleteDepartureCommand, ServiceResponse<DepartureDto>>
    {
        private readonly IDepartureRepository _departureRepository;
        private readonly IUnitOfWork<TourContext> _uow;

        public DeleteDepartureCommandHandler(IDepartureRepository departureRepository, IUnitOfWork<TourContext> uow)
        {
            _departureRepository = departureRepository;
            _uow = uow;
        }

        public async Task<ServiceResponse<DepartureDto>> Handle(DeleteDepartureCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _departureRepository.FindBy(x=>x.Id ==request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<DepartureDto>.Return404();
            }
            _departureRepository.Remove(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DepartureDto>.Return500();
            }
            return ServiceResponse<DepartureDto>.ReturnResultWith204();
        }
    }
}
