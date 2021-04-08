using Microsoft.EntityFrameworkCore;
using Storage.Models;
using Storage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Controllers
{
    public class PublisherController : EntityController<Publisher>
    {
        public PublisherController(IRepository<Publisher> repository) : base(repository)
        {
        }
    }
}
