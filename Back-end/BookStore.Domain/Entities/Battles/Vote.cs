
namespace BookStore.Domain.Entities.Battles;

public class Vote : IEntity
{
    public int Id { get; set; }
    
    public int VotingPointCount { get; set; }

    public BattleBook BattleBook { get; set; }
    public int BattleBookId { get; set; }

    public User User { get; set; }
    public int? UserId { get; set; }
}

