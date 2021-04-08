using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage.Models;
using Storage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Controllers
{
    public class AuthorController : EntityController<Author>
    {
        public AuthorController(IRepository<Author> repository) : base(repository)
        {
        }
    }
}
