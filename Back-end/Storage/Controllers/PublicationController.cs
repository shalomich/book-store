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

        [HttpGet("genres")]
        public ActionResult<IEnumerable<string>> ReadGenres()
        {
            return new JsonResult(Publication.GenreConsts);
        }

        [HttpGet("types")]
        public IEnumerable<string> ReadTypes()
        {
            return Publication.PublicationTypeConsts;
        }

        [HttpGet("cover-arts")]
        public IEnumerable<string> ReadCoverArts()
        {
            return Publication.CoverArtConsts;
        }


        [HttpGet("age-limits")]
        public IEnumerable<string> ReadAgeLimits()
        {
            return Publication.AgeLimitConsts;
        }

    }
}
