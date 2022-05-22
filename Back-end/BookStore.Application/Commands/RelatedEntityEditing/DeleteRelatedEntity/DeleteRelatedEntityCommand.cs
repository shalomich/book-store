using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Persistance;
using BookStore.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Entities.Books;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Extensions;
using BookStore.Domain.Entities;

namespace BookStore.Application.Commands.RelatedEntityEditing.DeleteRelatedEntity;
public record DeleteRelatedEntityCommand(int Id, Type RelatedEntityType) : IRequest;

internal class DeleteRelatedEntityCommandHandler : AsyncRequestHandler<DeleteRelatedEntityCommand>
{
    private ApplicationContext Context { get; }

    public DeleteRelatedEntityCommandHandler(
        ApplicationContext context)
    {
        Context = context;
    }

    protected override async Task Handle(DeleteRelatedEntityCommand request, CancellationToken cancellationToken)
    {
        var (id, type) = request;

        var relatedEntity = await Context.FindAsync(type, new object[] { id } , cancellationToken);

        if (relatedEntity == null)
        {
            throw new NotFoundException(type.Name);
        }

        try
        {
            Context.Remove((RelatedEntity) relatedEntity);
            await Context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            throw new BadRequestException(exception.GetFullMessage());
        }
    }
}
