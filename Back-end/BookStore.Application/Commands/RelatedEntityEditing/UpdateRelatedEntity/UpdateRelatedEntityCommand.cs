using BookStore.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Persistance;
using BookStore.Application.Exceptions;
using AutoMapper;
using BookStore.Application.Commands.RelatedEntityEditing.Common;
using BookStore.Application.Extensions;

namespace BookStore.Application.Commands.RelatedEntityEditing.UpdateRelatedEntity;

public record UpdateRelatedEntityCommand(int Id, Type RelatedEntityType, RelatedEntityForm RelatedEntityForm) : IRequest;
public class UpdateRelatedEntityCommandHandler : AsyncRequestHandler<UpdateRelatedEntityCommand>
{
    private ApplicationContext Context { get; }
    public IMapper Mapper { get; }

    public UpdateRelatedEntityCommandHandler(
        ApplicationContext context,
        IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    protected override async Task Handle(UpdateRelatedEntityCommand request, CancellationToken cancellationToken)
    {
        var (id, type, form) = request;

        if (form.Id != id)
        {
            throw new BadRequestException("Id and form id are different");
        }

        var relatedEntity = await Context.FindAsync(type, new object[] { id }, cancellationToken);

        if (relatedEntity == null)
        {
            throw new NotFoundException(type.Name);
        }

        relatedEntity = Mapper.Map(form, (RelatedEntity) relatedEntity);

        try
        {
            await Context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            throw new BadRequestException(exception.GetFullMessage());
        }
    }
}