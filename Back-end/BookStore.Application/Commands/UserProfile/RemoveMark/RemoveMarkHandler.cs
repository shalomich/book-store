using BookStore.Application.Exceptions;
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

namespace BookStore.Application.Commands.UserProfile.RemoveMark
{
    public record RemoveMarkCommand(int BookId) : IRequest;
    internal class RemoveMarkHandler : AsyncRequestHandler<RemoveMarkCommand>
    {
        private ApplicationContext Context { get; }
        private LoggedUserAccessor LoggedUserAccessor { get; }

        public RemoveMarkHandler(ApplicationContext context, LoggedUserAccessor loggedUserAccessor)
        {
            Context = context;
            LoggedUserAccessor = loggedUserAccessor;
        }

        protected override async Task Handle(RemoveMarkCommand request, CancellationToken cancellationToken)
        {
            var removedMark = await Context.Marks
                .SingleOrDefaultAsync(mark => mark.ProductId == request.BookId
                && mark.UserId == LoggedUserAccessor.GetCurrentUserId());

            if (removedMark == null)
                throw new NotFoundException("Mark does not exist by this book id.");

            Context.Remove(removedMark);
            await Context.SaveChangesAsync();
        }
    }
}
