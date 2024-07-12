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
    public class UpdateActiveTourCategoryCommandHandler : IRequestHandler<UpdateActiveTourCategoryCommand, ServiceResponse<TourCategory>>
    {
        private readonly ITourCategoryRepository _tourCategoryRepository;

        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        public UpdateActiveTourCategoryCommandHandler(ITourCategoryRepository tourCategoryRepository, IUnitOfWork<TourContext> uow, IMapper mapper)
        {
            _tourCategoryRepository = tourCategoryRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TourCategory>> Handle(UpdateActiveTourCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _tourCategoryRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (entity == null)
            {
                return ServiceResponse<TourCategory>.Return409("Content not found.");
            }
            entity.CategoryRecordId = request.CategoryRecordId;

            _tourCategoryRepository.Update(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<TourCategory>.Return500();
            }

            var entityDto = _mapper.Map<TourCategory>(entity);
            return ServiceResponse<TourCategory>.ReturnResultWith200(entityDto);
        }
    }
}
