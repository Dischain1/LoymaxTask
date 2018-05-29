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
    public class SqlRepositoryDbInitializer : DropCreateDatabaseIfModelChanges<UserContext>
    {
        protected override void Seed(UserContext context)
        {
            List<User> users = new List<User>
            {
                new User { Name = "Имя1", Surname = "Фамилия1", Patronymic = "Отчество2", DateOfBirth = DateTime.Now - TimeSpan.FromDays(365* 20), TelegramUserId = 10},
                new User { Name = "Имя2", Surname = "Фамилия2", Patronymic = "Отчество2", DateOfBirth = DateTime.Now - TimeSpan.FromDays(365* 30), TelegramUserId = 11},
            };

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Users.AddRange(users);
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            base.Seed(context);
        }
    }
}
