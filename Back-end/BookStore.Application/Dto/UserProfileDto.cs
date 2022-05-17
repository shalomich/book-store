namespace BookStore.Application.Dto;

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
}

