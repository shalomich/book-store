using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Account.Common;
public class ApplicationPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole<int>>
{
    /// <inheritdoc />
    public ApplicationPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, IOptions<IdentityOptions> options)
        : base(userManager, roleManager, options)
    {
    }

    /// <inheritdoc />
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        var userId = user.Id;

        identity.AddClaim(new Claim(nameof(userId), userId.ToString()));

        return identity;
    }
}

