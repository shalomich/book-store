
using BookStore.Application.Commands.Account;
using BookStore.Application.Commands.Subscriptions;
using BookStore.Application.Commands.Subscriptions.UpdateSubscription;
using BookStore.Application.Dto;
using BookStore.Application.Queries;
using BookStore.Application.ViewModels.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.WebApi.Areas.Store.Controllers;

[ApiController]
[Area("store")]
[Route("[area]/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class SubscriptionController : ControllerBase
{
    private IMediator Mediator { get; }

    public SubscriptionController(IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpPost]
    public async Task Activate([Required][Phone] string phoneNumber, CancellationToken cancellationToken)
    {
        await Mediator.Send(new ActivateSubscriptionCommand(phoneNumber), cancellationToken);
    }

    [HttpPut]
    public async Task Update(UpdateSubscriptionDto subscriptionDto, CancellationToken cancellationToken)
    {
        await Mediator.Send(new UpdateSubscriptionCommand(subscriptionDto), cancellationToken);
    }

    [HttpDelete]
    public async Task Cancel(CancellationToken cancellationToken)
    {
        await Mediator.Send(new CancelSubscriptionCommand(), cancellationToken);
    }
}
