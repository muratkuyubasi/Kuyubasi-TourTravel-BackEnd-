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
    public class UpdateActiveTourDayCommandHandler : IRequestHandler<UpdateActiveTourDayCommand, ServiceResponse<TourDay>>
    {
        private readonly ITourDayRepository _tourDayRepository;

        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        public UpdateActiveTourDayCommandHandler(ITourDayRepository tourDayRepository, IUnitOfWork<TourContext> uow, IMapper mapper)
        {
            _tourDayRepository = tourDayRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TourDay>> Handle(UpdateActiveTourDayCommand request, CancellationToken cancellationToken)
        {
            var entity = await _tourDayRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (entity == null)
            {
                return ServiceResponse<TourDay>.Return409("Content not found.");
            }
            entity.Title = request.Title;
           entity.Description = request.Description;

            _tourDayRepository.Update(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<TourDay>.Return500();
            }

            var entityDto = _mapper.Map<TourDay>(entity);
            return ServiceResponse<TourDay>.ReturnResultWith200(entityDto);
        }
    }
}
