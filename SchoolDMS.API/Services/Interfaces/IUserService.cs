using SchoolDMS.API.Models.DTOs.Users;
using SchoolDMS.API.Models.Responses;

namespace SchoolDMS.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<IEnumerable<UserDTO>>> GetAllUsersAsync();
        Task<ApiResponse<UserDTO>> GetUserByIdAsync(int id);
        Task<ApiResponse<int>> CreateUserAsync(CreateUserDTO request);
        Task<ApiResponse<bool>> UpdateUserAsync(int id, UpdateUserDTO request);
        Task<ApiResponse<bool>> DeleteUserAsync(int id);
        Task<ApiResponse<IEnumerable<UserDTO>>> GetUsersByRoleAsync(int roleId);
    }
}
