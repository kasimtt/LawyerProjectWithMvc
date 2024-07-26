using LawyerProject.Application.DTOs.UserDtos;
using LawyerProject.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto );
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate,int addOnAccessTokenDate);
        Task<GetUserDto> GetUserByUserNameAsync(string userNameOrEmail);
        Task<GetUserDetailsDto> GetUserDetailsAsync(string userNameOrEmail);
        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        Task<List<ListUser>> GetAllUsersAsync(int page, int size);
        int TotalUsersCount { get; }
        Task AssignRoleToUserAsync(string userId, string[] roles);
        Task<string[]> GetRolesToUserAsync(string userId);
        Task<string[]> GetRolesToUserNameAsync(string userName);
        Task<bool> HasRolePermissionToEndpointAsync(string name, string code);
    }
}
