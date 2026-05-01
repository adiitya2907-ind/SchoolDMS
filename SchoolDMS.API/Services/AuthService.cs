using AutoMapper;
using Microsoft.Extensions.Configuration;
using SchoolDMS.API.Helpers;
using SchoolDMS.API.Models.DTOs.Auth;
using SchoolDMS.API.Models.Entities;
using SchoolDMS.API.Models.Responses;
using SchoolDMS.API.Repositories.Interfaces;
using SchoolDMS.API.Services.Interfaces;

namespace SchoolDMS.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ApiResponse<LoginResponseDTO>> LoginAsync(LoginRequestDTO request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null || !user.IsActive)
            {
                return ApiResponse<LoginResponseDTO>.FailureResponse("Invalid credentials or inactive account.", 401);
            }

            if (!PasswordHelper.VerifyPassword(request.Password, user.PasswordHash))
            {
                return ApiResponse<LoginResponseDTO>.FailureResponse("Invalid credentials.", 401);
            }

            var token = JwtTokenHelper.GenerateJwtToken(user, _configuration);
            var refreshToken = JwtTokenHelper.GenerateRefreshToken();

            var expirationMinutes = int.Parse(_configuration.GetSection("Jwt")["ExpirationMinutes"] ?? "60");

            var responseDto = new LoginResponseDTO
            {
                Token = token,
                RefreshToken = refreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(expirationMinutes),
                UserId = user.UserId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role?.RoleName ?? user.RoleId.ToString()
            };

            return ApiResponse<LoginResponseDTO>.SuccessResponse(responseDto, "Login successful");
        }

        public async Task<ApiResponse<int>> RegisterAsync(RegisterDTO request)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return ApiResponse<int>.FailureResponse("User with this email already exists.", 409);
            }

            var user = _mapper.Map<User>(request);
            user.PasswordHash = PasswordHelper.HashPassword(request.Password);
            user.IsActive = true;
            user.CreatedAt = DateTime.UtcNow;

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return ApiResponse<int>.SuccessResponse(user.UserId, "Registration successful", 201);
        }
    }
}
