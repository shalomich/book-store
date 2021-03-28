using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Controllers
{
    public class AuthorController : EntityController<Author>
    {
        protected override IQueryable<Author> Data => _database.Authors
                                                                .Include(author => author.Images)
                                                                .Include(author => author.Publications);
        public AuthorController(Database database) : base(database)
        {
        }
    }
}
