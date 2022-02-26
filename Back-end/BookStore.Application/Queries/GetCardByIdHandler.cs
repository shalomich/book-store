using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Dto;
using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Entities.Books;
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
    public record GetCardByIdQuery(int BookId) : IRequest<CardDto>;
    internal class GetCardByIdHandler : IRequestHandler<GetCardByIdQuery, CardDto>
    {
        private class GetCardByIdProfile : Profile
        {
            public GetCardByIdProfile()
            {
                CreateMap<Book, CardDto>()
                    .ForMember(card => card.TitleImage, mapper =>
                        mapper.MapFrom(product => product.Album.TitleImage))
                    .ForMember(card => card.NotTitleImages, mapper =>
                        mapper.MapFrom(product => product.Album.NotTitleImages))
                    .ForMember(card => card.PublisherName, mapper =>
                        mapper.MapFrom(book => book.Publisher.Name))
                    .ForMember(card => card.AuthorName, mapper =>
                        mapper.MapFrom(book => book.Author.Name))
                    .ForMember(card => card.Type, mapper =>
                        mapper.MapFrom(book => book.Type.Name))
                    .ForMember(card => card.AgeLimit, mapper =>
                        mapper.MapFrom(book => book.AgeLimit.Name))
                    .ForMember(card => card.CoverArt, mapper =>
                        mapper.MapFrom(book => book.CoverArt.Name))
                    .ForMember(card => card.Genres, mapper =>
                        mapper.MapFrom(book => book.Genres.ToArray()));
            }
        }

        private LoggedUserAccessor LoggedUserAccessor { get; }
        private ApplicationContext Context { get; }
        private IMapper Mapper { get; }

        public GetCardByIdHandler(LoggedUserAccessor loggedUserAccessor, ApplicationContext context,
            IMapper mapper)
        {
            LoggedUserAccessor = loggedUserAccessor;
            Context = context;
            Mapper = mapper;
        }
        public async Task<CardDto> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
        {
            var bookById = await Context.Books
              .Include(book => book.Author)
              .Include(book => book.Publisher)
              .Include(book => book.Type)
              .Include(book => book.CoverArt)
              .Include(book => book.AgeLimit)
              .Include(book => book.Album)
                .ThenInclude(album => album.Images)
              .SingleOrDefaultAsync(book => book.Id == request.BookId);

            if (bookById == null)
              throw new NotFoundException("Book does not exist by this id.");

            var card = Mapper.Map<CardDto>(bookById);

            if (LoggedUserAccessor.IsAuthenticated())
            {
                int currentUserId = LoggedUserAccessor.GetCurrentUserId();

                card.IsInBasket = Context.BasketProducts
                  .Any(basketProduct => basketProduct.UserId == currentUserId
                    && basketProduct.ProductId == bookById.Id);
            }

            return card;
        }
    }
}
