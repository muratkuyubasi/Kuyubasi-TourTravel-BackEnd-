using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Common.UnitOfWork;
using TourV2.Data.Dto;
using TourV2.Data.Dto.ValuesEducation;
using TourV2.Data.Entities;
using TourV2.Domain;
using TourV2.Helper;
using TourV2.MediatR.Commands.State;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class AddStateCommandHandler : IRequestHandler<AddStateCommand, ServiceResponse<StateDto>>
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddStateCommandHandler> _logger;
        private readonly UserInfoToken _userInfoToken;
        private readonly IUnitOfWork<TourContext> _uow;

        public AddStateCommandHandler(IStateRepository StateRepository, IMapper mapper, ILogger<AddStateCommandHandler> logger,
            UserInfoToken userInfoToken, IUnitOfWork<TourContext> uow)
        {
            _stateRepository = StateRepository;
            _mapper = mapper;
            _logger = logger;
            _userInfoToken = userInfoToken;
            _uow = uow;
        }

        public async Task<ServiceResponse<StateDto>> Handle(AddStateCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Data.Entities.State>(request);

            _stateRepository.Add(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Kayıt Gerçekleşmedi");
                return ServiceResponse<StateDto>.Return500();
            }

            var entityDto = _mapper.Map<StateDto>(entity);
            return ServiceResponse<StateDto>.ReturnResultWith200(entityDto);


        }
    }
}