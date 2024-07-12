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
    public class AddActiveTourDepartureCommandHandler : IRequestHandler<AddActiveTourDepartureCommand, ServiceResponse<TourDeparture>>
    {
        private readonly ITourDepartureRepository _tourDepartureRepository;
      
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;

        public AddActiveTourDepartureCommandHandler(ITourDepartureRepository tourDepartureRepository, IUnitOfWork<TourContext> uow, IMapper mapper)
        {

            _tourDepartureRepository = tourDepartureRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TourDeparture>> Handle(AddActiveTourDepartureCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _tourDepartureRepository
                .FindBy(c => c.DepartureRecordId == request.DepartureRecordId && c.ActiveTourId == request.ActiveTourId && c.DepartureTime.Day == request.DepartureTime.Day)
                .FirstOrDefaultAsync();
            if (entityExist != null)
            {
                //AddTourRecord(request.TourRecords, entityExist.Id);
                return ServiceResponse<TourDeparture>.Return409("active Tour already exist.");
            }
            var entity = new TourDeparture
            {
                ActiveTourId = request.ActiveTourId,
                DepartureRecordId = request.DepartureRecordId,
                DepartureTime = request.DepartureTime,
                IsMain = request.IsMain

            };
            _tourDepartureRepository.Add(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<TourDeparture>.Return500();
            }

            var entityDto = _mapper.Map<TourDeparture>(entity);
            return ServiceResponse<TourDeparture>.ReturnResultWith200(entityDto);
        }


    }
}
