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
    public class UpdateActiveTourTransportationCommandHandler : IRequestHandler<UpdateActiveTourTransportationCommand, ServiceResponse<TourTransportation>>
    {
        private readonly ITourTransportationRepository _tourTransportationRepository;

        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        public UpdateActiveTourTransportationCommandHandler(ITourTransportationRepository tourTransportationRepository, IUnitOfWork<TourContext> uow, IMapper mapper)
        {
            _tourTransportationRepository = tourTransportationRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TourTransportation>> Handle(UpdateActiveTourTransportationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _tourTransportationRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (entity == null)
            {
                return ServiceResponse<TourTransportation>.Return409("Content not found.");
            }
            entity.Title = request.Title;

            _tourTransportationRepository.Update(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<TourTransportation>.Return500();
            }

            var entityDto = _mapper.Map<TourTransportation>(entity);
            return ServiceResponse<TourTransportation>.ReturnResultWith200(entityDto);
        }
    }
}
