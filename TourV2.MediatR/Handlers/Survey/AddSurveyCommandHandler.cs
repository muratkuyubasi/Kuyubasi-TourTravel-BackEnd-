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
using TourV2.Data.Dto.ValuesEducation;
using TourV2.Data.Dto;
using TourV2.Domain;
using TourV2.Helper;
using TourV2.MediatR.Commands.State;
using TourV2.Repository;
using TourV2.Repository.Survey;
using TourV2.MediatR.Commands.Survery;
using TourV2.Data.Dto.Tour;

namespace TourV2.MediatR.Handlers.Survey
{
    public class AddSurveyCommandHandler : IRequestHandler<AddSurveryCommand, ServiceResponse<SurveyDto>>
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddSurveyCommandHandler> _logger;
        private readonly UserInfoToken _userInfoToken;
        private readonly IUnitOfWork<TourContext> _uow;

        public AddSurveyCommandHandler(ISurveyRepository SurveyRepository, IMapper mapper, ILogger<AddSurveyCommandHandler> logger,
            UserInfoToken userInfoToken, IUnitOfWork<TourContext> uow)
        {
            _surveyRepository = SurveyRepository;
            _mapper = mapper;
            _logger = logger;
            _userInfoToken = userInfoToken;
            _uow = uow;
        }

        public async Task<ServiceResponse<SurveyDto>> Handle(AddSurveryCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Data.Entities.TourTravel.Survey>(request);

            _surveyRepository.Add(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Kayıt Gerçekleşmedi");
                return ServiceResponse<SurveyDto>.Return500();
            }

            var surveyDto = _mapper.Map<SurveyDto>(entity);
            return ServiceResponse<SurveyDto>.ReturnResultWith200(surveyDto);


        }
    }
}