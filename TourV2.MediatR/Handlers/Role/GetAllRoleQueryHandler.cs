using AutoMapper;
using TourV2.Data.Dto;
using TourV2.MediatR.Queries;
using TourV2.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TourV2.MediatR.Handlers
{
    public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, List<RoleDto>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllRoleQueryHandler> _logger;

        public GetAllRoleQueryHandler(
           IRoleRepository roleRepository,
            IMapper mapper,
            ILogger<GetAllRoleQueryHandler> logger)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<List<RoleDto>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            var entities = await _roleRepository.All.ToListAsync();
            return _mapper.Map<List<RoleDto>>(entities);
        }
    }
}
