using App.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.Controllers
{
    public class PublicationController : EntityController<Publication>
    {
        public PublicationController(IMediator mediator) : base(mediator)
        {
        }
    }
}
