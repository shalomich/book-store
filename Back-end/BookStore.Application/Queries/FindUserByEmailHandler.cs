using BookStore.Application.Exceptions;
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

public record FindUserByEmailQuery(string Email) : IRequest<User>;
internal class FindUserByEmailHandler : IRequestHandler<FindUserByEmailQuery, User>
{
    private const string NotExistEmailMessage = "This email does not exist";

    private readonly UserManager<User> _userManager;

    public FindUserByEmailHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User> Handle(FindUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            throw new NotFoundException(NotExistEmailMessage);

        return user;
    }
}

