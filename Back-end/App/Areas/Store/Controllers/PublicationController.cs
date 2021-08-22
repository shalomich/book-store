using App.Areas.Store.ViewModels;
using App.Entities;
using App.Entities.Publications;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueryWorker.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.GetEntitiesHandler;

namespace App.Areas.Store.Controllers
{
    public class PublicationController : ProductController<Publication>
    {
        public PublicationController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet("genre")]
        public async Task<IEnumerable<Option>> GetGenreOptions()
        {
            return await GetRelatedEntityOptions(typeof(Genre), new PaggingArgs());
        }

        [HttpGet("type")]
        public async Task<IEnumerable<Option>> GetTypeOptions()
        {
            return await GetRelatedEntityOptions(typeof(PublicationType), new PaggingArgs());
        }

        [HttpGet("age-limit")]
        public async Task<IEnumerable<Option>> GetAgeLimitOptions()
        {
            return await GetRelatedEntityOptions(typeof(AgeLimit), new PaggingArgs());
        }

        [HttpGet("cover-art")]
        public async Task<IEnumerable<Option>> GetCoverArtOptions()
        {
            return await GetRelatedEntityOptions(typeof(CoverArt), new PaggingArgs());
        }

        [HttpGet("author")]
        public async Task<IEnumerable<Option>> GetAuthorOptions()
        {
            return await GetRelatedEntityOptions(typeof(Author), new PaggingArgs());
        }

        [HttpGet("publisher")]
        public async Task<IEnumerable<Option>> GetPublisherOptions()
        {
            return await GetRelatedEntityOptions(typeof(Publisher), new PaggingArgs());
        }

    }
}
