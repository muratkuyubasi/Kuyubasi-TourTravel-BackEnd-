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
    public class AddActiveTourPriceCommandHandler : IRequestHandler<AddActiveTourPriceCommand, ServiceResponse<TourPrice>>
    {
        private readonly ITourPriceRepository _tourPriceRepository;
      
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;

        public AddActiveTourPriceCommandHandler(ITourPriceRepository tourPriceRepository, IUnitOfWork<TourContext> uow, IMapper mapper)
        {

            _tourPriceRepository = tourPriceRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TourPrice>> Handle(AddActiveTourPriceCommand request, CancellationToken cancellationToken)
        {
            
            var entity = new TourPrice
            {
                ActiveTourId = request.ActiveTourId,
                Title = request.Title,
                Currency = request.Currency,
                Price = request.Price,
                ExtraPrice = request.ExtraPrice,
                Discount = request.Discount,
                IsChildPrice = request.IsChildPrice

            };
            _tourPriceRepository.Add(entity);


           

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<TourPrice>.Return500();
            }

            var entityDto = _mapper.Map<TourPrice>(entity);
            return ServiceResponse<TourPrice>.ReturnResultWith200(entityDto);
        }


    }
}
