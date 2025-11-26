// Services/IUserService.cs
using UserManagementApi.Models;
using UserManagementApi.DTOs;

namespace UserManagementApi.Services
{
    public interface IUserService
    {
        IEnumerable<UserReadDto> GetAllUsers();
        UserReadDto GetUserById(int id);
        UserReadDto CreateUser(UserCreateDto dto);
        UserReadDto UpdateUser(int id, UserCreateDto dto);
        bool DeleteUser(int id);
    }
}