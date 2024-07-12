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
using TourV2.Domain;
using TourV2.Helper;
using TourV2.MediatR.Commands;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class UpdateActiveTourSpecificationCommandHandler : IRequestHandler<UpdateActiveTourSpecificationCommand, ServiceResponse<TourSpecification>>
    {
        private readonly ITourSpecificationRepository _tourSpecificationRepository;

        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        public UpdateActiveTourSpecificationCommandHandler(ITourSpecificationRepository tourSpecificationRepository, IUnitOfWork<TourContext> uow, IMapper mapper)
        {
            _tourSpecificationRepository = tourSpecificationRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TourSpecification>> Handle(UpdateActiveTourSpecificationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _tourSpecificationRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (entity == null)
            {
                return ServiceResponse<TourSpecification>.Return409("Content not found.");
            }
            entity.Specification = request.Specification;
            entity.InPrice = request.InPrice;

            _tourSpecificationRepository.Update(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<TourSpecification>.Return500();
            }

            var entityDto = _mapper.Map<TourSpecification>(entity);
            return ServiceResponse<TourSpecification>.ReturnResultWith200(entityDto);
        }
    }
}
