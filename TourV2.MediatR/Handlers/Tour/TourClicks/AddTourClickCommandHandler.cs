using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
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
    public class AddTourClickCommandHandler : IRequestHandler<AddTourClickCommand, ServiceResponse<TourClickDto>>
    {
        private readonly ITourClickRepository _tourClickRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddTourClickCommandHandler> _logger;
        private readonly UserInfoToken _userInfoToken;
        private readonly IUnitOfWork<TourContext> _uow;

        public AddTourClickCommandHandler(ITourClickRepository tourClickRepository, IMapper mapper, ILogger<AddTourClickCommandHandler> logger,
            UserInfoToken userInfoToken, IUnitOfWork<TourContext> uow)
        {
            _tourClickRepository = tourClickRepository;
            _mapper = mapper;
            _logger = logger;
            _userInfoToken = userInfoToken;
            _uow = uow;
        }

        public async Task<ServiceResponse<TourClickDto>> Handle(AddTourClickCommand request, CancellationToken cancellationToken)
        {
            if (_tourClickRepository.All.Any(x => x.IpAddress.Contains(request.IpAddress) && x.ActiveTourId == request.ActiveTourId))
            {
                _logger.LogError("Kayıt Gerçekleşmedi. Zaten daha önceden kayıtlı");
                return ServiceResponse<TourClickDto>.Return409("Kayıt Gerçekleşmedi. Zaten daha önceden kayıtlı");
            }

            var entity = _mapper.Map<TourClick>(request);
            entity.Id = Guid.NewGuid();

            _tourClickRepository.Add(entity);

            if (await _uow.SaveAsync() <= 0)
            {
            }

            var entityDto = _mapper.Map<TourClickDto>(entity);
            return ServiceResponse<TourClickDto>.ReturnResultWith200(entityDto);


        }
    }
}