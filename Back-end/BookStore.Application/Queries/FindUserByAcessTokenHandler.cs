using BookStore.Application.Exceptions;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Queries;

public record FindUserByAcessTokenQuery(string AccessToken) : IRequest<User>;
internal class FindUserByAcessTokenHandler : IRequestHandler<FindUserByAcessTokenQuery, User>
{
    private const string WrongAccessTokenMessage = "Wrong acess token";
    private JwtParser JwtParser { get; }
    private UserManager<User> UserManager { get; }

    public FindUserByAcessTokenHandler(JwtParser jwtParser, UserManager<User> userManager)
    {
        JwtParser = jwtParser ?? throw new ArgumentNullException(nameof(jwtParser));
        UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<User> Handle(FindUserByAcessTokenQuery request, CancellationToken cancellationToken)
    {
        int userId;
        
        try
        {
            userId = JwtParser.FromToken(request.AccessToken);
        }
        catch (Exception exception)
        {
            throw new BadRequestException(WrongAccessTokenMessage, exception);
        }

        var user = await UserManager.FindByIdAsync(userId.ToString());

        if (user == null)
            throw new NotFoundException(WrongAccessTokenMessage);

        return user;
    }
}

