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
using TourV2.Data.Dto.ValuesEducation;
using TourV2.Domain;
using TourV2.Helper;
using TourV2.MediatR.Commands.Mosque;
using TourV2.MediatR.Commands.State;
using TourV2.MediatR.Handlers.State;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers.Mosque
{
    public class DeleteMosqueCommandHandler : IRequestHandler<DeleteMosqueCommand, ServiceResponse<MosqueDto>>
    {
        private readonly IMosqueRepository _mosqueRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteMosqueCommandHandler> _logger;
        private readonly IUnitOfWork<TourContext> _uow;

        public DeleteMosqueCommandHandler(IMosqueRepository MosqueRepository, IMapper mapper, ILogger<DeleteMosqueCommandHandler> logger, IUnitOfWork<TourContext> uow)
        {
            _mosqueRepository = MosqueRepository;
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task<ServiceResponse<MosqueDto>> Handle(DeleteMosqueCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _mosqueRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<MosqueDto>.Return409("Kayıt Bulunamadı");
            }

            _mosqueRepository.Remove(entityExist);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Kayıt Gerçekleşmedi");
                return ServiceResponse<MosqueDto>.Return500();
            }

            var entityDto = _mapper.Map<MosqueDto>(entityExist);
            return ServiceResponse<MosqueDto>.ReturnResultWith200(entityDto);


        }
    }
}