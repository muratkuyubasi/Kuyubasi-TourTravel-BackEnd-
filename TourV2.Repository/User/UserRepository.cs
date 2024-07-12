using TourV2.Common.GenericRespository;
using TourV2.Common.UnitOfWork;
using TourV2.Data;
using TourV2.Data.Dto;
using TourV2.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TourV2.Data.Resources;

namespace TourV2.Repository
{
    public class UserRepository : GenericRepository<User, TourContext>,
          IUserRepository
    {
        private JwtSettings _settings = null;
        private readonly IUserClaimRepository _userClaimRepository;
        private readonly IRoleClaimRepository _roleClaimRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IPageActionRepository _pageActionRepository;
        private readonly IPropertyMappingService _propertyMappingService;
        public UserRepository(
            IUnitOfWork<TourContext> uow,
             JwtSettings settings,
             IUserClaimRepository userClaimRepository,
             IRoleClaimRepository roleClaimRepository,
             IUserRoleRepository userRoleRepository,
             IPageActionRepository pageActionRepository,
             IPropertyMappingService propertyMappingService
            ) : base(uow)
        {
            _roleClaimRepository = roleClaimRepository;
            _userClaimRepository = userClaimRepository;
            _userRoleRepository = userRoleRepository;
            _settings = settings;
            _pageActionRepository = pageActionRepository;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<UserList> GetUsers(UserResource userResource)
        {
            var collectionBeforePaging = All;
            collectionBeforePaging =
               collectionBeforePaging.ApplySort(userResource.OrderBy,
               _propertyMappingService.GetPropertyMapping<UserDto, User>());

            if (!string.IsNullOrWhiteSpace(userResource.FirstName))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c =>
                   EF.Functions.Like(c.FirstName, $"%{userResource.FirstName}%") || EF.Functions.Like(c.LastName, $"%{userResource.FirstName}%"));
            }
            if (!string.IsNullOrWhiteSpace(userResource.LastName))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c =>
                   EF.Functions.Like(c.LastName, $"%{userResource.LastName}%"));
            }
            if (!string.IsNullOrWhiteSpace(userResource.Email))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c =>
                   EF.Functions.Like(c.Email, $"%{userResource.Email}%"));
            }
            if (!string.IsNullOrWhiteSpace(userResource.PhoneNumber))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c =>
                   EF.Functions.Like(c.PhoneNumber, $"%{userResource.PhoneNumber}%"));
            }
            var isActive = userResource.IsActive == "0" ? false : true;
            collectionBeforePaging = collectionBeforePaging
                .Where(c => c.IsActive == isActive);

            var loginAudits = new UserList();
            return await loginAudits.Create(
                collectionBeforePaging,
                userResource.Skip,
                userResource.PageSize
                );
        }

        public async Task<UserAuthDto> BuildUserAuthObject(User appUser)
        {
            UserAuthDto ret = new UserAuthDto();
            List<AppClaimDto> appClaims = new List<AppClaimDto>();
            // Set User Properties
            ret.Id = appUser.Id;
            ret.UserName = appUser.UserName;
            ret.FirstName = appUser.FirstName;
            ret.LastName = appUser.LastName;
            ret.Email = appUser.Email;
            ret.PhoneNumber = appUser.PhoneNumber;
            ret.IsAuthenticated = true;
            ret.ProfilePhoto = appUser.ProfilePhoto;
            // Get all claims for this user
            var appClaimDtos = await this.GetUserAndRoleClaims(appUser);
            ret.Claims = appClaimDtos;
            var claims = appClaimDtos.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
            // Set JWT bearer token
            ret.BearerToken = BuildJwtToken(ret, claims, appUser.Id);
            return ret;
        }

        private async Task<List<AppClaimDto>> GetUserAndRoleClaims(User appUser)
        {
            var userClaims = await _userClaimRepository.FindBy(c => c.UserId == appUser.Id).ToListAsync();
            var roleClaims = await GetRoleClaims(appUser);
            var pageActions = await _pageActionRepository.AllIncluding(c => c.Page, c => c.Action).ToListAsync();
            List<AppClaimDto> lstAppClaimDto = new List<AppClaimDto>();
            foreach (var pageAction in pageActions)
            {
                var appClaimDto = new AppClaimDto
                {
                    ClaimType = $"{pageAction.Page.Name.ToLower()}_{pageAction.Action.Name.ToLower()}",
                    ClaimValue = "false"
                };
                if (userClaims.Any(c => c.PageId == pageAction.PageId && c.ActionId == pageAction.ActionId))
                {
                    appClaimDto.ClaimValue = "true";
                }
                if (roleClaims.Any(c => c.PageId == pageAction.PageId && c.ActionId == pageAction.ActionId))
                {
                    appClaimDto.ClaimValue = "true";
                }
                lstAppClaimDto.Add(appClaimDto);
            }
            return lstAppClaimDto;
        }

        private async Task<List<RoleClaim>> GetRoleClaims(User appUser)
        {
            var rolesIds = await _userRoleRepository.All.Where(c => c.UserId == appUser.Id)
                .Select(c => c.RoleId)
                .ToListAsync();
            List<RoleClaim> lstRoleClaim = new List<RoleClaim>();
            foreach (var roleId in rolesIds)
            {
                var roleClaims = await _roleClaimRepository.FindBy(c => c.RoleId == roleId).ToListAsync();
                foreach (var roleClaim in roleClaims)
                {
                    if (!lstRoleClaim.Any(c => c.ActionId == roleClaim.ActionId && c.PageId == roleClaim.PageId))
                    {
                        lstRoleClaim.Add(roleClaim);
                    }
                }
            }
            return lstRoleClaim;
        }

        protected string BuildJwtToken(UserAuthDto authUser, IList<Claim> claims, Guid Id)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(_settings.Key));
            claims.Add(new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub.ToString(), Id.ToString()));
            claims.Add(new Claim("Email", authUser.Email));
            // Create the JwtSecurityToken object
            var token = new JwtSecurityToken(
              issuer: _settings.Issuer,
              audience: _settings.Audience,
              claims: claims,
              notBefore: DateTime.UtcNow,
              expires: DateTime.UtcNow.AddMinutes(
                  _settings.MinutesToExpiration),
              signingCredentials: new SigningCredentials(key,
                          SecurityAlgorithms.HmacSha256)
            );
            // Create a string representation of the Jwt token
            return new JwtSecurityTokenHandler().WriteToken(token); ;
        }
    }
}
