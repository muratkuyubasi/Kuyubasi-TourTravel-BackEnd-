using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Common.UnitOfWork;
using TourV2.Data.Dto;
using TourV2.Data.Dto.ValuesEducation;
using TourV2.Domain;
using TourV2.Helper;
using TourV2.MediatR.Commands.EducaitonForm;
using TourV2.MediatR.Commands.State;
using TourV2.MediatR.Handlers.Education;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers.State
{
    public class DeleteStateCommandHandler : IRequestHandler<DeleteStateCommand, ServiceResponse<StateDto>>
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteStateCommandHandler> _logger;
        private readonly IUnitOfWork<TourContext> _uow;

        public DeleteStateCommandHandler(IStateRepository StateRepository, IMapper mapper, ILogger<DeleteStateCommandHandler> logger, IUnitOfWork<TourContext> uow)
        {
            _stateRepository = StateRepository;
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task<ServiceResponse<StateDto>> Handle(DeleteStateCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _stateRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<StateDto>.Return409("Kayıt Bulunamadı");
            }

            _stateRepository.Remove(entityExist);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Kayıt Gerçekleşmedi");
                return ServiceResponse<StateDto>.Return500();
            }

            var entityDto = _mapper.Map<StateDto>(entityExist);
            return ServiceResponse<StateDto>.ReturnResultWith200(entityDto);


        }
    }
}
