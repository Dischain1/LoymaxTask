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
    //public static class SeederExtension
    //{
    //    public static async void Seed(this SqlRepository rep)
    //    {
    //        if (rep.context == null)
    //            rep.context = new UserContext();

    //        List<User> users = new List<User>
    //        {
    //            new User { Name = "Имя1", Surname = "Фамилия1", Patronymic = "Отчество2", DateOfBirth = DateTime.Now - TimeSpan.FromDays(365* 20)},
    //            new User { Name = "Имя2", Surname = "Фамилия2", Patronymic = "Отчество2", DateOfBirth = DateTime.Now - TimeSpan.FromDays(365* 30)},
    //        };

    //        using (var transaction = rep.context.Database.BeginTransaction())
    //        {
    //            try
    //            {
    //                if (!rep.context.Users.Any())
    //                    rep.context.Users.AddRange(users);
    //                await rep.context.SaveChangesAsync();
    //            }
    //            catch (Exception)
    //            {
    //                transaction.Rollback();
    //            }
    //        }
    //    }
    //}

    public class SqlRepositoryDbInitializer : DropCreateDatabaseIfModelChanges<UserContext>
    {
        protected override void Seed(UserContext context)
        {
            List<User> users = new List<User>
            {
                new User { Name = "Имя1", Surname = "Фамилия1", Patronymic = "Отчество2", DateOfBirth = DateTime.Now - TimeSpan.FromDays(365* 20)},
                new User { Name = "Имя2", Surname = "Фамилия2", Patronymic = "Отчество2", DateOfBirth = DateTime.Now - TimeSpan.FromDays(365* 30)},
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
