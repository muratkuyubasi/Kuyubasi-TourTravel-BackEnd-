﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.MediatR.Queries;
using TourV2.Repository;

namespace TourV2.MediatR.Handlers
{
    public class GetRecentlyRegisteredUserQueryHandler : IRequestHandler<GetRecentlyRegisteredUserQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetRecentlyRegisteredUserQueryHandler(
           IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetRecentlyRegisteredUserQuery request, CancellationToken cancellationToken)
        {
            var entities = await _userRepository.All.OrderByDescending(c => c.CreatedDate).Take(10).ToListAsync();
            return _mapper.Map<List<UserDto>>(entities);
        }
    }
}
