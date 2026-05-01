using SchoolDMS.API.Models.Entities;

namespace SchoolDMS.API.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserWithRoleAsync(int userId);
        Task<IEnumerable<User>> GetUsersByRoleAsync(int roleId);
    }
}
