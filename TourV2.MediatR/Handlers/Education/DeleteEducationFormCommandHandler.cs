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
using TourV2.Domain;
using TourV2.Helper;
using TourV2.MediatR.Commands;
using TourV2.MediatR.Commands.EducaitonForm;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers.Education
{
    public class DeleteEducationFormCommandHandler : IRequestHandler<DeleteEducationFormCommand, ServiceResponse<EducationFormDTO>>
    {
        private readonly IEducationFormRepository _educationFormRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteEducationFormCommandHandler> _logger;
        private readonly IUnitOfWork<TourContext> _uow;

        public DeleteEducationFormCommandHandler(IEducationFormRepository EducationFormRepository, IMapper mapper, ILogger<DeleteEducationFormCommandHandler> logger, IUnitOfWork<TourContext> uow)
        {
            _educationFormRepository = EducationFormRepository;
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task<ServiceResponse<EducationFormDTO>> Handle(DeleteEducationFormCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _educationFormRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<EducationFormDTO>.Return409("Kayıt Bulunamadı");
            }

            _educationFormRepository.Remove(entityExist);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Kayıt Gerçekleşmedi");
                return ServiceResponse<EducationFormDTO>.Return500();
            }

            var entityDto = _mapper.Map<EducationFormDTO>(entityExist);
            return ServiceResponse<EducationFormDTO>.ReturnResultWith200(entityDto);


        }
    }
}