using AutoMapper;
using TourV2.Common.UnitOfWork;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Domain;
using TourV2.MediatR.Commands;
using TourV2.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Helper;
using Microsoft.Extensions.Logging;
using System.IO;

namespace TourV2.MediatR.Handlers
{
    public class UpdateFrontAnnouncementRecordCommandHandler : IRequestHandler<UpdateFrontAnnouncementRecordCommand, ServiceResponse<FrontAnnouncementRecordDto>>
    {
        private readonly IFrontAnnouncementRecordRepository _FrontAnnouncementRecordRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        private readonly UserInfoToken _userInfoToken;
        private readonly ILogger<UpdateFrontAnnouncementRecordCommandHandler> _logger;
        public UpdateFrontAnnouncementRecordCommandHandler(
           IFrontAnnouncementRecordRepository FrontAnnouncementRecordRepository,
            IMapper mapper,
            IUnitOfWork<TourContext> uow,
            UserInfoToken userInfoToken,
            ILogger<UpdateFrontAnnouncementRecordCommandHandler> logger
            )
        {
            _FrontAnnouncementRecordRepository = FrontAnnouncementRecordRepository;
            _mapper = mapper;
            _uow = uow;
            _userInfoToken = userInfoToken;
            _logger = logger;
        }

        public async Task<ServiceResponse<FrontAnnouncementRecordDto>> Handle(UpdateFrontAnnouncementRecordCommand request, CancellationToken cancellationToken)
        {
            var filePath = $"{request.RootPath}/Announcement";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var entityExist = await _FrontAnnouncementRecordRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();

            if (entityExist != null)
            {
                _logger.LogError("FrontAnnouncementRecord Already Exist.");
                return ServiceResponse<FrontAnnouncementRecordDto>.Return409("FrontAnnouncementRecord Already Exist.");
            }

            if (request.FormFile != null)
            {
                var mediaFile = request.FormFile;
                var newMedia = $"{Guid.NewGuid()}{Path.GetExtension(mediaFile.Name)}";
                string fullPath = Path.Combine(filePath, newMedia);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    mediaFile.CopyTo(stream);
                }
                entityExist.FileUrl = newMedia;
            }

            entityExist.FrontAnnouncementId = request.FrontAnnouncementId;
            entityExist.Title = request.Title;
            entityExist.ShortDescription = request.ShortDescription;
            entityExist.LongDescription = request.LongDescription;
            entityExist.LanguageCode = request.LanguageCode;
            entityExist.Link = request.Link;


            _FrontAnnouncementRecordRepository.Update(entityExist);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontAnnouncementRecordDto>.Return500();
            }
            return ServiceResponse<FrontAnnouncementRecordDto>.ReturnResultWith200(_mapper.Map<FrontAnnouncementRecordDto>(entityExist));
        }
    }
}
