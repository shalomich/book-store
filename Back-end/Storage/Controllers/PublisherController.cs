using Microsoft.EntityFrameworkCore;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Controllers
{
    public class PublisherController : EntityController<Publisher>
    {
        protected override IQueryable<Publisher> Data => _database
                                                                .Publishers
                                                                .Include(publisher => publisher.Images)
                                                                .Include(publisher => publisher.Publications);

        public PublisherController(Database database) : base(database)
        {
        }

    }
}
