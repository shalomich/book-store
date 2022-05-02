using BookStore.Application.Queries.Battle.GetBattleSettings;
using System;

namespace BookStore.Application.Services;
internal static class BattleCalculator
{
    public static int CalculateDiscount(int totalBattleVotingPointCount, BattleSettings battleSettings)
    {
        int increase = (int) Math.Round(totalBattleVotingPointCount * battleSettings.DiscountPerVotingPoint);

        int currentDiscount = increase + battleSettings.InitialDiscount;

        if (currentDiscount > battleSettings.FinalDiscount)
        {
            currentDiscount = battleSettings.FinalDiscount;
        }

        return currentDiscount;
    }
}

