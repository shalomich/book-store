using Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth
{
    public class Database : IdentityDbContext<User>
    {
        public Database(DbContextOptions options) : base(options)
        {

        }
    }
}
