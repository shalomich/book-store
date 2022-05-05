using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private S3Storage S3Storage { get; }

        public GetBasketProductsHandler(LoggedUserAccessor loggedUserAccessor, 
            ApplicationContext context, IMapper mapper, S3Storage s3Storage)
        {
            LoggedUserAccessor = loggedUserAccessor;
            Context = context;
            Mapper = mapper;
            S3Storage = s3Storage;
        }

        public async Task<IEnumerable<BasketProductDto>> Handle(GetBasketProductsQuery request, CancellationToken cancellationToken)
        {
            int currentUserId = LoggedUserAccessor.GetCurrentUserId();

            IEnumerable<BasketProductDto> basketProducts = await Context.BasketProducts
                .Where(basketProduct => basketProduct.UserId == currentUserId)
                .ProjectTo<BasketProductDto>(Mapper.ConfigurationProvider)
                .ToListAsync();

            basketProducts = SetFileUrls(basketProducts);

            return basketProducts;
        }

        private IEnumerable<BasketProductDto> SetFileUrls(IEnumerable<BasketProductDto> basketProducts)
        {
            foreach (var basketProduct in basketProducts)
            {

                var titleImage = basketProduct.TitleImage with
                {
                    FileUrl = S3Storage.GetPresignedUrlForViewing(basketProduct.ProductId, basketProduct.TitleImage.Id)
                };

                yield return basketProduct with { TitleImage = titleImage };
            }
        }
    }
}
