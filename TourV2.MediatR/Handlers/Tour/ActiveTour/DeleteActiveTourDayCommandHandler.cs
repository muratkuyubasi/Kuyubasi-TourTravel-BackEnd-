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
    public class DeleteActiveTourDayCommandHandler : IRequestHandler<DeleteActiveTourDayCommand, ServiceResponse<TourDay>>
    {
        private readonly ITourDayRepository _dayRepository;
        private readonly IUnitOfWork<TourContext> _uow;

        public DeleteActiveTourDayCommandHandler(ITourDayRepository dayRepository, IUnitOfWork<TourContext> uow)
        {
            _dayRepository = dayRepository;
            _uow = uow;
        }

        public async Task<ServiceResponse<TourDay>> Handle(DeleteActiveTourDayCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _dayRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<TourDay>.Return404();
            }
            _dayRepository.Remove(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<TourDay>.Return500();
            }
            return ServiceResponse<TourDay>.ReturnResultWith204();
        }
    }
}
