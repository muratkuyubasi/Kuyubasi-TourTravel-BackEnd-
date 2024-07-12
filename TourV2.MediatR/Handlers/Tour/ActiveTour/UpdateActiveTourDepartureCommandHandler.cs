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
    public class UpdateActiveTourDepartureCommandHandler : IRequestHandler<UpdateActiveTourDepartureCommand, ServiceResponse<TourDeparture>>
    {
        private readonly ITourDepartureRepository _departureRepository;

        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        public UpdateActiveTourDepartureCommandHandler(ITourDepartureRepository departureRepository, IUnitOfWork<TourContext> uow, IMapper mapper)
        {
            _departureRepository = departureRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TourDeparture>> Handle(UpdateActiveTourDepartureCommand request, CancellationToken cancellationToken)
        {
            var entity = await _departureRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (entity == null)
            {
                return ServiceResponse<TourDeparture>.Return409("Content not found.");
            }
            entity.DepartureTime = request.DepartureTime;
            entity.IsMain = request.IsMain;

            _departureRepository.Update(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<TourDeparture>.Return500();
            }

            var entityDto = _mapper.Map<TourDeparture>(entity);
            return ServiceResponse<TourDeparture>.ReturnResultWith200(entityDto);
        }
    }
}
