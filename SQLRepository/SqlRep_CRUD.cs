using EFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLRepository
{
    public partial class SqlRepository : IRepository
    {
        // CRUD
        public async Task AddUser(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        public async Task<User> GetUser(int id)
        {
            //return await context.Users.FindAsync(id);
            return await context.Users.Where(x=>x.TelegramUserId == id).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        public async Task RemoveUser(int id)
        {
            //var user = await context.Users.FindAsync(id);
            var user = await context.Users.Where(x => x.TelegramUserId == id).FirstOrDefaultAsync();

            if (user == null)
                return;

            context.Users.Remove(user);
            context.SaveChanges();
        }
    }
}
