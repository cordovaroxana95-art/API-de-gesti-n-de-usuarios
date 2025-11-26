
using UserManagementApi.Models;

namespace UserManagementApi.Data
{
    public static class InMemoryContext
    {
        private static List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "Alice Johnson", Email = "alice@example.com", Age = 30 },
            new User { Id = 2, Name = "Bob Williams", Email = "bob@example.com", Age = 24 }
        };
        private static int _nextId = 3;

        public static List<User> Users => _users;

        public static int GetNextId() => _nextId++;
    }
}