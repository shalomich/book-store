using App.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.Controllers
{
    public class PublisherController : EntityController<Publisher>
    {
        public PublisherController(IMediator mediator) : base(mediator)
        {
        }
    }
}
