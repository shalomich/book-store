using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistance
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=app_db;Trusted_Connection=True;";
        public ApplicationContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
           
            builder.UseSqlServer(ConnectionString);
           
            return new ApplicationContext(builder.Options);
        }
    }
}
