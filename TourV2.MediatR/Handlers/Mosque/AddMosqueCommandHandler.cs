using AutoMapper;
using MediatR;
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
using TourV2.MediatR.Commands.Mosque;
using TourV2.MediatR.Commands.MosqueRepository;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers.Mosque
{
    public class AddMosqueCommandHandler : IRequestHandler<AddMosqueCommand, ServiceResponse<MosqueDto>>
    {
        private readonly IMosqueRepository _mosqueRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddMosqueCommandHandler> _logger;
        private readonly UserInfoToken _userInfoToken;
        private readonly IUnitOfWork<TourContext> _uow;

        public AddMosqueCommandHandler(IMosqueRepository MosqueRepository, IMapper mapper, ILogger<AddMosqueCommandHandler> logger,
            UserInfoToken userInfoToken, IUnitOfWork<TourContext> uow)
        {
            _mosqueRepository = MosqueRepository;
            _mapper = mapper;
            _logger = logger;
            _userInfoToken = userInfoToken;
            _uow = uow;
        }

        public async Task<ServiceResponse<MosqueDto>> Handle(AddMosqueCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Data.Entities.Mosque>(request);

            _mosqueRepository.Add(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Kayıt Gerçekleşmedi");
                return ServiceResponse<MosqueDto>.Return500();
            }

            var entityDto = _mapper.Map<MosqueDto>(entity);
            return ServiceResponse<MosqueDto>.ReturnResultWith200(entityDto);


        }
    }
}