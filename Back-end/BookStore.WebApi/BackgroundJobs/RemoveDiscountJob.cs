﻿using BookStore.Application.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.WebApi.BackgroundJobs;

internal class RemoveDiscountJob
{
    private IMediator Mediator { get; }

    public RemoveDiscountJob(IMediator mediator)
    {
        Mediator = mediator;
    }

    public async Task RemoveDiscount(CancellationToken cancellationToken)
    {
        await Mediator.Send(new RemoveDiscountCommand(), cancellationToken);
    }
}

