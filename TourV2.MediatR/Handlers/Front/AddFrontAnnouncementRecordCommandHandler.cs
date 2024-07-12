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
using System.Threading;
using System.Threading.Tasks;
using TourV2.Helper;
using Microsoft.Extensions.Logging;
using System.IO;

namespace TourV2.MediatR.Handlers
{
    public class AddFrontAnnouncementRecordCommandHandler : IRequestHandler<AddFrontAnnouncementRecordCommand, ServiceResponse<FrontAnnouncementRecordDto>>
    {
        private readonly IFrontAnnouncementRecordRepository _FrontAnnouncementRecordRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        private readonly UserInfoToken _userInfoToken;
        private readonly ILogger<AddFrontAnnouncementRecordCommandHandler> _logger;
        public AddFrontAnnouncementRecordCommandHandler(
           IFrontAnnouncementRecordRepository FrontAnnouncementRecordRepository,
            IMapper mapper,
            IUnitOfWork<TourContext> uow,
            UserInfoToken userInfoToken,
            ILogger<AddFrontAnnouncementRecordCommandHandler> logger
            )
        {
            _FrontAnnouncementRecordRepository = FrontAnnouncementRecordRepository;
            _mapper = mapper;
            _uow = uow;
            _userInfoToken = userInfoToken;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontAnnouncementRecordDto>> Handle(AddFrontAnnouncementRecordCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<FrontAnnouncementRecord>(request);

            var filePath = $"{request.RootPath}/Announcement";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
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
                entity.FileUrl = newMedia;
            }

            _FrontAnnouncementRecordRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontAnnouncementRecordDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontAnnouncementRecordDto>(entity);
            return ServiceResponse<FrontAnnouncementRecordDto>.ReturnResultWith200(entityDto); 
        }
    }
}
