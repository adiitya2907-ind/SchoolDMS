using AutoMapper;
using SchoolDMS.API.Helpers;
using SchoolDMS.API.Models.DTOs.Users;
using SchoolDMS.API.Models.Entities;
using SchoolDMS.API.Models.Responses;
using SchoolDMS.API.Repositories.Interfaces;
using SchoolDMS.API.Services.Interfaces;

namespace SchoolDMS.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<UserDTO>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDTO>>(users);
            return ApiResponse<IEnumerable<UserDTO>>.SuccessResponse(userDtos);
        }

        public async Task<ApiResponse<UserDTO>> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserWithRoleAsync(id);
            if (user == null)
            {
                return ApiResponse<UserDTO>.FailureResponse("User not found", 404);
            }

            var userDto = _mapper.Map<UserDTO>(user);
            return ApiResponse<UserDTO>.SuccessResponse(userDto);
        }

        public async Task<ApiResponse<int>> CreateUserAsync(CreateUserDTO request)
        {
            if (await _userRepository.ExistsAsync(u => u.Email.ToLower() == request.Email.ToLower()))
            {
                return ApiResponse<int>.FailureResponse("Email already exists", 409);
            }

            var user = _mapper.Map<User>(request);
            user.PasswordHash = PasswordHelper.HashPassword(request.Password);
            
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return ApiResponse<int>.SuccessResponse(user.UserId, "User created successfully", 201);
        }

        public async Task<ApiResponse<bool>> UpdateUserAsync(int id, UpdateUserDTO request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return ApiResponse<bool>.FailureResponse("User not found", 404);
            }

            _mapper.Map(request, user);
            user.UpdatedAt = DateTime.UtcNow;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "User updated successfully");
        }

        public async Task<ApiResponse<bool>> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return ApiResponse<bool>.FailureResponse("User not found", 404);
            }

            user.IsActive = false; // Soft delete
            user.UpdatedAt = DateTime.UtcNow;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "User deleted successfully");
        }

        public async Task<ApiResponse<IEnumerable<UserDTO>>> GetUsersByRoleAsync(int roleId)
        {
            var users = await _userRepository.GetUsersByRoleAsync(roleId);
            var userDtos = _mapper.Map<IEnumerable<UserDTO>>(users);
            return ApiResponse<IEnumerable<UserDTO>>.SuccessResponse(userDtos);
        }
    }
}
