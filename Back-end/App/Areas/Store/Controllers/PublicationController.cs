using App.Areas.Store.ViewModels;
using App.Entities;
using App.Entities.Publications;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static App.Areas.Common.RequestHandlers.GetHandler;

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
            return await GetRelatedEntityOptions(typeof(Genre));
        }

        [HttpGet("type")]
        public async Task<IEnumerable<Option>> GetTypeOptions()
        {
            return await GetRelatedEntityOptions(typeof(PublicationType));
        }

        [HttpGet("age-limit")]
        public async Task<IEnumerable<Option>> GetAgeLimitOptions()
        {
            return await GetRelatedEntityOptions(typeof(AgeLimit));
        }

        [HttpGet("cover-art")]
        public async Task<IEnumerable<Option>> GetCoverArtOptions()
        {
            return await GetRelatedEntityOptions(typeof(CoverArt));
        }

        [HttpGet("author")]
        public async Task<IEnumerable<Option>> GetAuthorOptions()
        {
            return await GetRelatedEntityOptions(typeof(Author));
        }

        [HttpGet("publisher")]
        public async Task<IEnumerable<Option>> GetPublisherOptions()
        {
            return await GetRelatedEntityOptions(typeof(Publisher));
        }

    }
}
