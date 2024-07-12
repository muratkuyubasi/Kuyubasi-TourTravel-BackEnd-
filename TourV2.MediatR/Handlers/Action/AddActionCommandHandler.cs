using AutoMapper;
using TourV2.Common.UnitOfWork;
using TourV2.Data.Dto;
using TourV2.Domain;
using TourV2.MediatR.Commands;
using TourV2.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Helper;
using Microsoft.Extensions.Logging;

namespace TourV2.MediatR.Handlers
{
    public class AddActionCommandHandler : IRequestHandler<AddActionCommand, ServiceResponse<ActionDto>>
    {
        private readonly IActionRepository _actionRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddActionCommandHandler> _logger;
        public AddActionCommandHandler(
           IActionRepository actionRepository,
            IMapper mapper,
            IUnitOfWork<TourContext> uow,
            ILogger<AddActionCommandHandler> logger
            )
        {
            _actionRepository = actionRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<ActionDto>> Handle(AddActionCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _actionRepository.FindBy(c => c.Name.Trim().ToLower() == request.Name.Trim().ToLower()).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Action already exist.");
                return ServiceResponse<ActionDto>.Return409("Action already exist.");
            }
            var entity = _mapper.Map<Data.Action>(request);
            entity.Id = Guid.NewGuid();
            _actionRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<ActionDto>.Return500();
            }
            var entityDto = _mapper.Map<ActionDto>(entity);
            return ServiceResponse<ActionDto>.ReturnResultWith200(entityDto);
        }
    }
}
