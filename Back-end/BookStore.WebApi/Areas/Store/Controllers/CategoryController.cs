using BookStore.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookStore.Domain.Enums;
using BookStore.Application.Dto;
using BookStore.WebApi.Attributes;

namespace BookStore.WebApi.Areas.Store.Controllers
{
    [Route("[area]/selection/")]
    public class CategoryController : StoreController
    {
        private IMediator Mediator { get; }

        public CategoryController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet("{category}")]
        [TypeFilter(typeof(OptionalAuthorizeFilter))]
        public async Task<PreviewSetDto> FindBooksByCategory(Category category, 
            [FromQuery] OptionParameters optionParameters)
        {
            return await Mediator.Send(new FindBooksByCategoryQuery(category, optionParameters)); 
        }
    }
}
