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

namespace TourV2.MediatR.Handlers
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, ServiceResponse<UserRoleDto>>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUnitOfWork<TourContext> _uow;
        private readonly IMapper _mapper;
        public UpdateUserRoleCommandHandler(IUserRoleRepository userRoleRepository,
            IUnitOfWork<TourContext> uow,
            IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<UserRoleDto>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var userRoles = await _userRoleRepository.All.Where(c => c.RoleId == request.Id).ToListAsync();
            var userRolesToAdd = request.UserRoles.Where(c => !userRoles.Select(c => c.UserId).Contains(c.UserId.Value)).ToList();
            _userRoleRepository.AddRange(_mapper.Map<List<UserRole>>(userRolesToAdd));
            var userRolesToDelete = userRoles.Where(c => !request.UserRoles.Select(cs => cs.UserId).Contains(c.UserId)).ToList();
            _userRoleRepository.RemoveRange(userRolesToDelete);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<UserRoleDto>.Return500();
            }
            return ServiceResponse<UserRoleDto>.ReturnSuccess();
        }
    }

}
