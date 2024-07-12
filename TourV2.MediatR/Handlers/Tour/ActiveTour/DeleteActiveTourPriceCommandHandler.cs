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
    public class DeleteActiveTourPriceCommandHandler : IRequestHandler<DeleteActiveTourPriceCommand, ServiceResponse<TourPrice>>
    {
        private readonly ITourPriceRepository _priceRepository;
        private readonly IUnitOfWork<TourContext> _uow;

        public DeleteActiveTourPriceCommandHandler(ITourPriceRepository priceRepository, IUnitOfWork<TourContext> uow)
        {
            _priceRepository = priceRepository;
            _uow = uow;
        }

        public async Task<ServiceResponse<TourPrice>> Handle(DeleteActiveTourPriceCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _priceRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<TourPrice>.Return404();
            }
            _priceRepository.Remove(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<TourPrice>.Return500();
            }
            return ServiceResponse<TourPrice>.ReturnResultWith204();
        }
    }
}
