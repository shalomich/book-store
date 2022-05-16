using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Dto;
using BookStore.Application.Services;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries
{
    public record GetBasketProductsQuery : IRequest<IEnumerable<BasketProductDto>>;

    internal class GetBasketProductsHandler : IRequestHandler<GetBasketProductsQuery, IEnumerable<BasketProductDto>>
    {
        private LoggedUserAccessor LoggedUserAccessor { get;}
        private ApplicationContext Context { get; }
        private IMapper Mapper { get; }
        private ImageFileRepository ImageFileRepository { get; }

        public GetBasketProductsHandler(LoggedUserAccessor loggedUserAccessor, 
            ApplicationContext context, IMapper mapper, ImageFileRepository imageFileRepository)
        {
            LoggedUserAccessor = loggedUserAccessor;
            Context = context;
            Mapper = mapper;
            ImageFileRepository = imageFileRepository;
        }

        public async Task<IEnumerable<BasketProductDto>> Handle(GetBasketProductsQuery request, CancellationToken cancellationToken)
        {
            int currentUserId = LoggedUserAccessor.GetCurrentUserId();

            IEnumerable<BasketProductDto> basketProducts = await Context.BasketProducts
                .Where(basketProduct => basketProduct.UserId == currentUserId)
                .ProjectTo<BasketProductDto>(Mapper.ConfigurationProvider)
                .ToListAsync();

            basketProducts = await SetFileUrls(basketProducts, cancellationToken);

            return basketProducts;
        }

        private async Task<IEnumerable<BasketProductDto>> SetFileUrls(IEnumerable<BasketProductDto> basketProducts, CancellationToken cancellationToken)
        {
            var basketProductDtoWithUrl = new List<BasketProductDto>();

            foreach (var basketProduct in basketProducts)
            {
                var titleImage = basketProduct.TitleImage with
                {
                    FileUrl = await ImageFileRepository.GetPresignedUrlForViewing(basketProduct.TitleImage.Id, cancellationToken)
                };

                basketProductDtoWithUrl.Add(basketProduct with { TitleImage = titleImage });
            }

            return basketProductDtoWithUrl;
        }
    }
}
