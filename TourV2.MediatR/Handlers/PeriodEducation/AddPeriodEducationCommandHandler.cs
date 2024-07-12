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
using TourV2.MediatR.Commands.PeriodEducation;
using TourV2.Repository;
using TourV2.Repository.PeriodEducation;

namespace TourV2.MediatR.Handlers.PeriodEducation
{
    public class AddPeriodEducationCommandHandler : IRequestHandler<AddPeriodEducationCommand, ServiceResponse<PeriodEducationDto>>
    {
        private readonly IPeriodEducationRepository _periodEducationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddPeriodEducationCommandHandler> _logger;
        private readonly UserInfoToken _userInfoToken;
        private readonly IUnitOfWork<TourContext> _uow;

        public AddPeriodEducationCommandHandler(IPeriodEducationRepository PeriodEducationRepository, IMapper mapper, ILogger<AddPeriodEducationCommandHandler> logger,
            UserInfoToken userInfoToken, IUnitOfWork<TourContext> uow)
        {
            _periodEducationRepository = PeriodEducationRepository;
            _mapper = mapper;
            _logger = logger;
            _userInfoToken = userInfoToken;
            _uow = uow;
        }

        public async Task<ServiceResponse<PeriodEducationDto>> Handle(AddPeriodEducationCommand request, CancellationToken cancellationToken)
        {
            var data = _periodEducationRepository.All.Where(x => x.IsActive == true).FirstOrDefault();
            if (data != null)
            {
                data.IsActive = false;
                _periodEducationRepository.Update(data);
            }
            var entity = _mapper.Map<Data.Entities.PeriodEducation>(request);

            entity.CreationDate= DateTime.Now;
            _periodEducationRepository.Add(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Kayıt Gerçekleşmedi");
                return ServiceResponse<PeriodEducationDto>.Return500();
            }

            var entityDto = _mapper.Map<PeriodEducationDto>(entity);
            return ServiceResponse<PeriodEducationDto>.ReturnResultWith200(entityDto);


        }
    }
}