using BookStore.Application.Services;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Catalog.ViewBook;

public record ViewBookCommand(int BookId) : IRequest;
internal class ViewBookCommandHandler : AsyncRequestHandler<ViewBookCommand>
{
    private int ViewIntervalInMinutes { get; }
    private ApplicationContext Context { get; }
    private LoggedUserAccessor LoggedUserAccessor { get; }

    public ViewBookCommandHandler(ApplicationContext context, LoggedUserAccessor loggedUserAccessor,
        IConfiguration configuration)
    {
        Context = context;
        LoggedUserAccessor = loggedUserAccessor;

        ViewIntervalInMinutes = configuration.GetSection(nameof(ViewIntervalInMinutes)).Get<int>();
    }

    protected async override Task Handle(ViewBookCommand request, CancellationToken cancellationToken)
    {
        int currentUserId = LoggedUserAccessor.GetCurrentUserId();

        var view = await Context.Views
            .SingleOrDefaultAsync(view => view.UserId == currentUserId
                && view.BookId == request.BookId);

        if (view == null)
        {
            view = new View
            {
                UserId = currentUserId,
                BookId = request.BookId
            };

            Context.Update(view);
            await Context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            var timeWithOutView = DateTimeOffset.Now - view.LastTime;

            if (timeWithOutView.Minutes > ViewIntervalInMinutes)
            {
                view.LastTime = DateTimeOffset.Now;
                view.Count++;

                await Context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}

