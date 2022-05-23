using AutoMapper;
using BookStore.Application.Commands.RelatedEntityEditing.Common;
using BookStore.Application.Exceptions;
using BookStore.Application.Extensions;
using BookStore.Domain.Entities;
using BookStore.Persistance;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.RelatedEntityEditing.GetEditRelatedEntity;

public record GetEditRelatedEntityQuery(int Id, Type RelatedEntityType) : IRequest<RelatedEntityForm>;
internal class GetEditRelatedEntityQueryHandler : IRequestHandler<GetEditRelatedEntityQuery, RelatedEntityForm>
{
    public IMapper Mapper { get; }
    public ApplicationContext Context { get; }

    public GetEditRelatedEntityQueryHandler(
        IMapper mapper,
        ApplicationContext context)
    {
        Mapper = mapper;
        Context = context;
    }

    public async Task<RelatedEntityForm> Handle(GetEditRelatedEntityQuery request, CancellationToken cancellationToken)
    {
        var (id, relatedEntityType) = request;

        var relatedEntity = await Context.FindAsync(relatedEntityType, new object[] { id }, cancellationToken);

        if (relatedEntity == null)
        {
            throw new NotFoundException(relatedEntityType.Name);
        }

        var relatedEntityFormType = Mapper.GetDestinationType(relatedEntityType, typeof(RelatedEntityForm));

        return (RelatedEntityForm) Mapper.Map((RelatedEntity) relatedEntity, relatedEntityType, relatedEntityFormType);
    }
}

