using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Common.UnitOfWork;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Domain;
using TourV2.Helper;
using TourV2.MediatR.Commands;

namespace TourV2.MediatR.Handlers
{
    public class UpdateUserProfilePhotoCommandHandler : IRequestHandler<UpdateUserProfilePhotoCommand, ServiceResponse<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        IUnitOfWork<TourContext> _uow;
        private UserInfoToken _userInfoToken;
        private readonly ILogger<UpdateUserProfileCommandHandler> _logger;
        public readonly PathHelper _pathHelper;
        public UpdateUserProfilePhotoCommandHandler(
            IMapper mapper,
            IUnitOfWork<TourContext> uow,
            UserInfoToken userInfoToken,
            UserManager<User> userManager,
            ILogger<UpdateUserProfileCommandHandler> logger,
            PathHelper pathHelper
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _uow = uow;
            _userInfoToken = userInfoToken;
            _logger = logger;
            _pathHelper = pathHelper;
        }

        public async Task<ServiceResponse<UserDto>> Handle(UpdateUserProfilePhotoCommand request, CancellationToken cancellationToken)
        {
            var filePath = $"{request.RootPath}/{_pathHelper.UserProfilePath}";
            var appUser = await _userManager.FindByIdAsync(_userInfoToken.Id);
            if (appUser == null)
            {
                _logger.LogError("User does not exist.");
                return ServiceResponse<UserDto>.Return409("User does not exist.");
            }
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            // delete existing file
            if (!string.IsNullOrWhiteSpace(appUser.ProfilePhoto))
            {
                if (File.Exists($"{filePath}/{appUser.ProfilePhoto}"))
                {
                    File.Delete($"{filePath}/{appUser.ProfilePhoto}");
                }
            }

            // save new file
            if (request.FormFile.Any())
            {
                var profileFile = request.FormFile[0];
                var newProfilePhoto = $"{Guid.NewGuid()}{Path.GetExtension(profileFile.Name)}";
                string fullPath = Path.Combine(filePath, newProfilePhoto);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    profileFile.CopyTo(stream);
                }
                appUser.ProfilePhoto = newProfilePhoto;
            }
            else
            {
                appUser.ProfilePhoto = "";
            }

            // update user
            IdentityResult result = await _userManager.UpdateAsync(appUser);
            if (await _uow.SaveAsync() <= 0 && !result.Succeeded)
            {
                return ServiceResponse<UserDto>.Return500();
            }

            if (!string.IsNullOrWhiteSpace(appUser.ProfilePhoto))
                appUser.ProfilePhoto = $"{_pathHelper.UserProfilePath}/{appUser.ProfilePhoto}";
            return ServiceResponse<UserDto>.ReturnResultWith200(_mapper.Map<UserDto>(appUser));
        }
    }
}
