using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Controllers
{
    public class PublicationController : EntityController<Publication>
    {
        protected override IQueryable<Publication> Data => _database
                                                                .Publications
                                                                .Include(publication => publication.Images)
                                                                .Include(publication => publication.Author)
                                                                .Include(publication => publication.Publisher);

        public PublicationController(Database database) : base(database)
        {
        }
    }
}
