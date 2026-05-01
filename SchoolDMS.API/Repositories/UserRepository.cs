using Microsoft.EntityFrameworkCore;
using SchoolDMS.API.Data;
using SchoolDMS.API.Models.Entities;
using SchoolDMS.API.Repositories.Interfaces;

namespace SchoolDMS.API.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbSet
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User?> GetUserWithRoleAsync(int userId)
        {
            return await _dbSet
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(int roleId)
        {
            return await _dbSet
                .Include(u => u.Role)
                .Where(u => (int)u.RoleId == roleId)
                .ToListAsync();
        }
    }
}
