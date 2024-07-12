﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.Helper;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetOnlineUsersQueryHandler : IRequestHandler<GetOnlineUsersQuery, List<UserDto>>
    {
        private readonly IConnectionMappingRepository _connectionMappingRepository;
        private readonly IUserRepository _userRepository;
        private readonly PathHelper _pathHelper;
        private readonly UserInfoToken _userInfoToken;
        public GetOnlineUsersQueryHandler(IUserRepository userRepository,
            IConnectionMappingRepository connectionMappingRepository,
            PathHelper pathHelper,
            UserInfoToken userInfoToken)
        {
            _userRepository = userRepository;
            _connectionMappingRepository = connectionMappingRepository;
            _pathHelper = pathHelper;
            _userInfoToken = userInfoToken;
        }
        public async Task<List<UserDto>> Handle(GetOnlineUsersQuery request, CancellationToken cancellationToken)
        {
            var allUserIds = _connectionMappingRepository.GetAllUsersExceptThis(_userInfoToken).Select(c => Guid.Parse(c.Id)).ToList();
            var users = await _userRepository.All.Where(c => allUserIds.Contains(c.Id))
                .Select(cs => new UserDto
                {
                    Id = cs.Id,
                    FirstName = cs.FirstName,
                    LastName = cs.LastName,
                    Email = cs.Email,
                    ProfilePhoto = !string.IsNullOrWhiteSpace(cs.ProfilePhoto) ? $"{_pathHelper.UserProfilePath}{cs.ProfilePhoto}" : string.Empty
                }).ToListAsync();
            return users;
        }
    }
}
