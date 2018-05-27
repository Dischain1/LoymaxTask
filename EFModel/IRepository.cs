using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFModel
{
    public interface IRepository : IDisposable
    {
        // ---------------------------------- User CRUD ----------------------------------
        Task<List<User>> GetUsers();
        Task AddUser(User user);
        Task RemoveUser(int userId);
        Task<User> GetUser(int userId);
    }
}
