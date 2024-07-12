using TourV2.Common.GenericRespository;
using TourV2.Data;
using TourV2.Data.Dto;
using System.Threading.Tasks;
using TourV2.Data.Resources;

namespace TourV2.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<UserList> GetUsers(UserResource userResource);
        Task<UserAuthDto> BuildUserAuthObject(User appUser);
    }
}
