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
    public class PublicationController : EntityController<Publication>
    {
        public PublicationController(IRepository<Publication> repository) : base(repository)
        {
        }
    }
}
