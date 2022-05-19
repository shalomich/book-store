using BookStore.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookStore.Domain.Enums;
using BookStore.Application.Dto;
using BookStore.WebApi.Attributes;
using BookStore.Application.Services.CatalogSelections;
using Microsoft.AspNetCore.Authorization;

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
            [FromQuery] OptionParameters optionParameters, [FromServices] CategorySelection categorySelection)
        {
            categorySelection.ChoosenCategory = category;

            return await Mediator.Send(new GetCatalogSelectionQuery(categorySelection, optionParameters));
        }

        [HttpGet("specialForYou")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<PreviewSetDto> FindSpecialForYou([FromQuery] int tagCount,
            [FromQuery] OptionParameters optionParameters, [FromServices] SpecialForYouCategorySelection specialForYouSelection)
        {
            specialForYouSelection.TagCount = tagCount;

            return await Mediator.Send(new GetCatalogSelectionQuery(specialForYouSelection, optionParameters));
        }

        [HttpGet("lastViewed")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<PreviewSetDto> FindLastViewed([FromQuery] OptionParameters optionParameters, [FromServices] LastViewedSelection lastViewedSelection)
        {
            return await Mediator.Send(new GetCatalogSelectionQuery(lastViewedSelection, optionParameters));
        }

        [HttpGet("battle")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<PreviewSetDto> FindBattleBooks([FromQuery] OptionParameters optionParameters, [FromServices] BooksForBattleSelection battleBookSelection)
        {
            return await Mediator.Send(new GetCatalogSelectionQuery(battleBookSelection, optionParameters));
        }
    }
}
