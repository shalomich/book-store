﻿namespace BookStore.TelegramBot.UseCases.Battle.CastVote;
internal record UserBattleInfoViewModel
{
    public int VotingPointCount { init; get; }
    public int? CurrentVotedBattleBookId { get; init; }
}

