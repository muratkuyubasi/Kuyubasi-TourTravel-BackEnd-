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
    public class DeleteActiveTourSpecificationCommandHandler : IRequestHandler<DeleteActiveTourSpecificationCommand, ServiceResponse<TourSpecification>>
    {
        private readonly ITourSpecificationRepository _specificationRepository;
        private readonly IUnitOfWork<TourContext> _uow;

        public DeleteActiveTourSpecificationCommandHandler(ITourSpecificationRepository specificationRepository, IUnitOfWork<TourContext> uow)
        {
            _specificationRepository = specificationRepository;
            _uow = uow;
        }

        public async Task<ServiceResponse<TourSpecification>> Handle(DeleteActiveTourSpecificationCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _specificationRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<TourSpecification>.Return404();
            }
            _specificationRepository.Remove(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<TourSpecification>.Return500();
            }
            return ServiceResponse<TourSpecification>.ReturnResultWith204();
        }
    }
}
