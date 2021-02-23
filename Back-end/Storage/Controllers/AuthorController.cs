using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Controllers
{
    public class AuthorController : EntityController<Author>
    {
        public AuthorController(Database database) : base(database)
        {
        }
    }
}
