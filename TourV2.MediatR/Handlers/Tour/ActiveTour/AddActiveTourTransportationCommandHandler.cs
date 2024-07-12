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
    public class AddActiveTourTransportationCommandHandler : IRequestHandler<AddActiveTourTransportationCommand, ServiceResponse<TourTransportation>>
    {
        private readonly ITourTransportationRepository _tourTransportationRepository;
      
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;

        public AddActiveTourTransportationCommandHandler(ITourTransportationRepository tourTransportationRepository, IUnitOfWork<TourContext> uow, IMapper mapper)
        {

            _tourTransportationRepository = tourTransportationRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TourTransportation>> Handle(AddActiveTourTransportationCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _tourTransportationRepository.FindBy(c => c.ActiveTourId == request.ActiveTourId).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                //AddTourRecord(request.TourRecords, entityExist.Id);
                return ServiceResponse<TourTransportation>.Return409("active Tour already exist.");
            }
            var entity = new TourTransportation
            {
                ActiveTourId = request.ActiveTourId,
                Title = request.Title,

            };
            _tourTransportationRepository.Add(entity);


           

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<TourTransportation>.Return500();
            }

            var entityDto = _mapper.Map<TourTransportation>(entity);
            return ServiceResponse<TourTransportation>.ReturnResultWith200(entityDto);
        }


    }
}
