using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Controllers
{
    public class PublisherController : EntityController<Publisher>
    {
        public PublisherController(Database database) : base(database)
        {
        }
    }
}
