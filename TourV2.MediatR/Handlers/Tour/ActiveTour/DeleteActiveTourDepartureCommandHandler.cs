using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
    public class DeleteActiveTourDepartureCommandHandler : IRequestHandler<DeleteActiveTourDepartureCommand, ServiceResponse<TourDeparture>>
    {
        private readonly ITourDepartureRepository _departureRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        public readonly PathHelper _pathHelper;


        public DeleteActiveTourDepartureCommandHandler(ITourDepartureRepository departureRepository, PathHelper pathHelper, IUnitOfWork<TourContext> uow)
        {
            _departureRepository = departureRepository;
            _uow = uow;
            _pathHelper = pathHelper;
        }

        public async Task<ServiceResponse<TourDeparture>> Handle(DeleteActiveTourDepartureCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _departureRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<TourDeparture>.Return404();
            }
            _departureRepository.Remove(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<TourDeparture>.Return500();
            }

            return ServiceResponse<TourDeparture>.ReturnResultWith204();
        }
    }
}
