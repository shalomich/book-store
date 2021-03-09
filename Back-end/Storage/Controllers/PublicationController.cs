using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Controllers
{
    public class PublicationController : EntityController<Publication>
    {
        public PublicationController(Database database) : base(database)
        {
        }
    }
}
