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
        private readonly string connStr;

        public SqlRepository(string connStr)
        {
            this.connStr = connStr;
        }

        // CRUD
        public async Task AddUser(User user)
        {
            using (var context = new UserContext(connStr))
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task<User> GetUser(int id)
        {
            using (var context = new UserContext(connStr))
            {
                await context.SaveChangesAsync();
                return await context.Users.Where(x => x.TelegramUserId == id).FirstOrDefaultAsync();
            }
        }

        public async Task<List<User>> GetUsers()
        {
            using (var context = new UserContext(connStr))
            {
                return await context.Users.ToListAsync();
            }
        }

        public async Task RemoveUser(int id)
        {
            using (var context = new UserContext(connStr))
            {
                //var user = await context.Users.FindAsync(id);
                var user = await context.Users.Where(x => x.TelegramUserId == id).FirstOrDefaultAsync();

                if (user == null)
                    return;

                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
