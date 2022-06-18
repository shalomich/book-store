using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Account.CheckEmailExistence;

public record CheckEmailExistenceQuery(string Email) : IRequest<bool>;
internal class CheckEmailExistenceQueryHandler : IRequestHandler<CheckEmailExistenceQuery, bool>
{
    private UserManager<User> UserManager { get; }

    public CheckEmailExistenceQueryHandler(
        UserManager<User> userManager)
    {
        UserManager = userManager;
    }
    
    public async Task<bool> Handle(CheckEmailExistenceQuery request, CancellationToken cancellationToken)
    {
        var user = await UserManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return false;
        }

        return true;
    }
}

