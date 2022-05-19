using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Entities.Products;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.UserProfile.CreateMark
{
    public record CreateMarkCommand(int BookId) : IRequest;
    internal class CreateMarkHandler : AsyncRequestHandler<CreateMarkCommand>
    {
        private ApplicationContext Context { get; }
        private LoggedUserAccessor LoggedUserAccessor { get; }

        public CreateMarkHandler(ApplicationContext context, LoggedUserAccessor loggedUserAccessor)
        {
            Context = context;
            LoggedUserAccessor = loggedUserAccessor;
        }

        protected override async Task Handle(CreateMarkCommand request, CancellationToken cancellationToken)
        {
            bool isBookExist = await Context.Books
                .AnyAsync(book => book.Id == request.BookId);

            if (!isBookExist)
                throw new NotFoundException("Book does not exist by this id.");

            var userId = LoggedUserAccessor.GetCurrentUserId();

            bool isMarkExist = await Context.Marks
                .AnyAsync(mark => mark.ProductId == request.BookId
                && mark.UserId == userId);

            if (isMarkExist)
                throw new BadRequestException("Mark is already created.");

            var mark = new Mark()
            {
                ProductId = request.BookId,
                UserId = userId
            };

            await Context.AddAsync(mark);
            await Context.SaveChangesAsync();
        }
    }
}
