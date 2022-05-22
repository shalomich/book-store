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

namespace BookStore.Application.Commands.RelatedEntityEditing.CreateRelatedEntity;
public record CreateRelatedEntityCommand(Type RelatedEntityType, RelatedEntityForm RelatedEntityForm) : IRequest<int>;
public class CreateRelatedEntityCommandHandler : IRequestHandler<CreateRelatedEntityCommand, int>
{
    private ApplicationContext Context { get; }
    public IMapper Mapper { get; }

    public CreateRelatedEntityCommandHandler(
        ApplicationContext context,
        IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    public async Task<int> Handle(CreateRelatedEntityCommand request, CancellationToken cancellationToken)
    {
        var (type, form) = request;

        var relatedEntity = (RelatedEntity) Mapper.Map(form, form.GetType(), type);

        try
        {
            await Context.AddAsync(relatedEntity, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            throw new BadRequestException(exception.GetFullMessage());
        }

        return relatedEntity.Id;
    }
}
