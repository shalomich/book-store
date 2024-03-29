﻿using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Application.Services.Jwt;
using BookStore.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.TelegramBot.CreateTelegramBotToken;

public record CreateTelegramBotTokenCommand() : IRequest<TelegramBotTokenDto>;
internal class CreateTelegramBotTokenHandler : IRequestHandler<CreateTelegramBotTokenCommand, TelegramBotTokenDto>
{
    private LoggedUserAccessor LoggedUserAccessor { get; }
    private TelegramBotJwtParser JwtParser { get; }
    public ApplicationContext Context { get; }

    public CreateTelegramBotTokenHandler(
        LoggedUserAccessor loggedUserAccessor,
        TelegramBotJwtParser jwtParser,
        ApplicationContext context)
    {
        LoggedUserAccessor = loggedUserAccessor;
        JwtParser = jwtParser;
        Context = context;
    }

    public async Task<TelegramBotTokenDto> Handle(CreateTelegramBotTokenCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        var userId = LoggedUserAccessor.GetCurrentUserId();

        var claimIdentities = new List<ClaimsIdentity>()
        {
            new ClaimsIdentity(new List<Claim>()
            {
                new Claim(nameof(userId), userId.ToString())
            })
        };

        var principal = new ClaimsPrincipal(claimIdentities);

        return new TelegramBotTokenDto
        {
            BotToken = JwtParser.ToToken(principal)
        };
    }

    private async Task Validate(CreateTelegramBotTokenCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = LoggedUserAccessor.GetCurrentUserId();

        var currentUser = await Context.Users
            .SingleAsync(user => user.Id == currentUserId, cancellationToken);

        if (string.IsNullOrEmpty(currentUser.PhoneNumber))
        {
            throw new BadRequestException("User has no phone number.");
        }
    }
}

