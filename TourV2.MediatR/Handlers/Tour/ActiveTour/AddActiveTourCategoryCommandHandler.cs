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
    public class AddActiveTourCategoryCommandHandler : IRequestHandler<AddActiveTourCategoryCommand, ServiceResponse<TourCategory>>
    {
        private readonly ITourCategoryRepository _tourCategoryRepository;
      
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;

        public AddActiveTourCategoryCommandHandler(ITourCategoryRepository tourCategoryRepository, IUnitOfWork<TourContext> uow, IMapper mapper)
        {
           
            _tourCategoryRepository = tourCategoryRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TourCategory>> Handle(AddActiveTourCategoryCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _tourCategoryRepository.FindBy(c => c.CategoryRecordId == request.CategoryRecordId).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                //AddTourRecord(request.TourRecords, entityExist.Id);
                return ServiceResponse<TourCategory>.Return409("active Tour already exist.");
            }
            var entity = new TourCategory
            {
                ActiveTourId = request.ActiveTourId,
                CategoryRecordId = request.CategoryRecordId,

            };
            _tourCategoryRepository.Add(entity);


           

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<TourCategory>.Return500();
            }

            var entityDto = _mapper.Map<TourCategory>(entity);
            return ServiceResponse<TourCategory>.ReturnResultWith200(entityDto);
        }


    }
}
