using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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
    public class AddEmailTemplateCommandHandler : IRequestHandler<AddEmailTemplateCommand, ServiceResponse<EmailTemplateDto>>
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        private readonly UserInfoToken _userInfoToken;
        private readonly ILogger<AddEmailTemplateCommandHandler> _logger;
        public AddEmailTemplateCommandHandler(
           IEmailTemplateRepository emailTemplateRepository,
            IMapper mapper,
            IUnitOfWork<TourContext> uow,
            UserInfoToken userInfoToken,
            ILogger<AddEmailTemplateCommandHandler> logger
            )
        {
            _emailTemplateRepository = emailTemplateRepository;
            _mapper = mapper;
            _uow = uow;
            _userInfoToken = userInfoToken;
            _logger = logger;
        }
        public async Task<ServiceResponse<EmailTemplateDto>> Handle(AddEmailTemplateCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _emailTemplateRepository.FindBy(c => c.Name == request.Name).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Email Template already exist.");
                return ServiceResponse<EmailTemplateDto>.Return409("Email Template already exist.");
            }
            var entity = _mapper.Map<EmailTemplate>(request);
            entity.Id = Guid.NewGuid();
            entity.CreatedBy = Guid.Parse(_userInfoToken.Id);
            entity.CreatedDate = DateTime.Now.ToLocalTime();
            entity.ModifiedBy = Guid.Parse(_userInfoToken.Id);
            _emailTemplateRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<EmailTemplateDto>.Return500();
            }
            var entityDto = _mapper.Map<EmailTemplateDto>(entity);
            return ServiceResponse<EmailTemplateDto>.ReturnResultWith200(entityDto);
        }
    }
}
