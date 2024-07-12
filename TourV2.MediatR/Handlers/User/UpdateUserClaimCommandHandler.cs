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
    public class UpdateUserClaimCommandHandler : IRequestHandler<UpdateUserClaimCommand, ServiceResponse<UserClaimDto>>
    {
        IUnitOfWork<TourContext> _uow;
        IUserClaimRepository _userClaimRepository;
        private readonly IMapper _mapper;
        public UpdateUserClaimCommandHandler(
            IMapper mapper,
            IUserClaimRepository userClaimRepository,
            IUnitOfWork<TourContext> uow
            )
        {
            _mapper = mapper;
            _uow = uow;
            _userClaimRepository = userClaimRepository;
        }

        public async Task<ServiceResponse<UserClaimDto>> Handle(UpdateUserClaimCommand request, CancellationToken cancellationToken)
        {
            var appUserClaims = await _userClaimRepository.All.Where(c => c.UserId == request.Id).ToListAsync();
            var claimsToAdd = request.UserClaims.Where(c => !appUserClaims.Select(c => c.ClaimType).Contains(c.ClaimType)).ToList();
            _userClaimRepository.AddRange(_mapper.Map<List<UserClaim>>(claimsToAdd));
            var claimsToDelete = appUserClaims.Where(c => !request.UserClaims.Select(cs => cs.ClaimType).Contains(c.ClaimType)).ToList();
            _userClaimRepository.RemoveRange(claimsToDelete);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<UserClaimDto>.Return500();
            }
            return ServiceResponse<UserClaimDto>.ReturnSuccess();
        }
    }
}
