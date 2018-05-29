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
        private UserContext context;

        public SqlRepository()
        {
            context = new UserContext();
        }

        public SqlRepository(string connStr)
        {
            context = new UserContext(connStr);
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
