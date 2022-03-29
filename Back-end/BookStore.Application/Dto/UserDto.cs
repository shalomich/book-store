namespace BookStore.Application.Dto;

public record UserDto
{
    public string FirstName { init; get; }
    public string LastName { init; get; }
    public string Email { init; get; }
    public string PhoneNumber { init; get; }
    public string Address { init; get; }
}

