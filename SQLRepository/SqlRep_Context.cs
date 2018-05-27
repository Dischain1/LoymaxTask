using EFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLRepository
{
    public class UserContext: DbContext
    {
        public UserContext():base()
        {
            Database.SetInitializer(new SqlRepositoryDbInitializer());
        }

        public virtual DbSet<User> Users { get; set; }
    }
}
