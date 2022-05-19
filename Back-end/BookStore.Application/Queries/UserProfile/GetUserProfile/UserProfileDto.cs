using System.Collections.Generic;
using System.Linq;

namespace BookStore.Application.Queries.UserProfile.GetUserProfile;

public record UserProfileDto
{
    public int Id { init; get; }
    public string FirstName { init; get; }
    public string LastName { init; get; }
    public string Email { init; get; }
    public string PhoneNumber { init; get; }
    public string Address { init; get; }
    public int VotingPointCount { init; get; }
    public bool IsTelegramBotLinked { init; get; }
    public int? CurrentVotedBattleBookId { get; init; }
    public int? SpentCurrentVotingPointCount { get; init; }
    public IEnumerable<int> BasketBookIds { init; get; } = Enumerable.Empty<int>();
    public IEnumerable<int> MarkBookIds { init; get; } = Enumerable.Empty<int>();
}

