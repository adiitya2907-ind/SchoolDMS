using SchoolDMS.API.Models.DTOs.Auth;
using SchoolDMS.API.Models.Responses;

namespace SchoolDMS.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginResponseDTO>> LoginAsync(LoginRequestDTO request);
        Task<ApiResponse<int>> RegisterAsync(RegisterDTO request);
    }
}
