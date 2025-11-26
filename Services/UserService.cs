// Services/UserService.cs
using UserManagementApi.Data;
using UserManagementApi.DTOs;
using UserManagementApi.Models;

namespace UserManagementApi.Services
{
    public class UserService : IUserService
    {
        // Método de mapeo auxiliar
        private UserReadDto MapToReadDto(User user)
        {
            return new UserReadDto { Id = user.Id, Name = user.Name, Email = user.Email };
        }

        public IEnumerable<UserReadDto> GetAllUsers()
        {
            return InMemoryContext.Users.Select(MapToReadDto);
        }

        public UserReadDto GetUserById(int id)
        {
            var user = InMemoryContext.Users.FirstOrDefault(u => u.Id == id);
            return user != null ? MapToReadDto(user) : null;
        }

        public UserReadDto CreateUser(UserCreateDto dto)
        {
            // Validación de unicidad de Email
            if (InMemoryContext.Users.Any(u => u.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("El correo electrónico ya está registrado.");
            }

            var newUser = new User
            {
                Id = InMemoryContext.GetNextId(),
                Name = dto.Name,
                Email = dto.Email,
                Age = dto.Age
            };

            InMemoryContext.Users.Add(newUser);
            return MapToReadDto(newUser);
        }

        public UserReadDto UpdateUser(int id, UserCreateDto dto)
        {
            var user = InMemoryContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return null;

            // Validación de unicidad de Email (excluyendo el usuario actual)
            if (InMemoryContext.Users.Any(u => u.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase) && u.Id != id))
            {
                throw new InvalidOperationException("El correo electrónico ya está registrado por otro usuario.");
            }

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Age = dto.Age;

            return MapToReadDto(user);
        }

        public bool DeleteUser(int id)
        {
            var user = InMemoryContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return false;

            return InMemoryContext.Users.Remove(user);
        }
    }
}