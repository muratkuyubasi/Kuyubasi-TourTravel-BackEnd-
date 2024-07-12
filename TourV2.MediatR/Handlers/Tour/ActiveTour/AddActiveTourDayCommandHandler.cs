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
    public class AddActiveTourDayCommandHandler : IRequestHandler<AddActiveTourDayCommand, ServiceResponse<TourDay>>
    {
        private readonly ITourDayRepository _tourDayRepository;
      
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;

        public AddActiveTourDayCommandHandler(ITourDayRepository tourDayRepository, IUnitOfWork<TourContext> uow, IMapper mapper)
        {
           
            _tourDayRepository = tourDayRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TourDay>> Handle(AddActiveTourDayCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _tourDayRepository.FindBy(c => c.Title == request.Title).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                //AddTourRecord(request.TourRecords, entityExist.Id);
                return ServiceResponse<TourDay>.Return409("active Tour already exist.");
            }
            var entity = new TourDay
            {
                ActiveTourId = request.ActiveTourId,
                Title = request.Title,
                Description = request.Description

            };
            _tourDayRepository.Add(entity);


           

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<TourDay>.Return500();
            }

            var entityDto = _mapper.Map<TourDay>(entity);
            return ServiceResponse<TourDay>.ReturnResultWith200(entityDto);
        }


    }
}
