using AutoMapper;
using BookStore.Application.Commands.Battles.StartBattle;
using BookStore.Application.Commands.BookEditing.Common;
using BookStore.Application.Commands.Selection.Common;
using BookStore.Application.Dto;
using BookStore.Application.Services;
using BookStore.Domain.Entities.Books;
using BookStore.Persistance;
using Microsoft.EntityFrameworkCore;
using QueryWorker;
using System;
using System.Linq;

namespace BookStore.Application.Commands.Selection.GetBattleSelection;

public record GetBattleSelectionQuery(OptionParameters OptionParameters) : GetSelectionQuery(OptionParameters);
internal class GetBattleSelectionQueryHandler : GetSelectionQueryHandler<GetBattleSelectionQuery>
{
    public ApplicationContext Context { get; }
    public BattleSettingsProvider BattleSettingsProvider { get; }

    public GetBattleSelectionQueryHandler(
        SelectionConfigurator<Book> selectionConfigurator,
        IMapper mapper,
        ImageFileRepository imageFileRepository,
        ApplicationContext context,
        BattleSettingsProvider battleSettingsProvider) : base(selectionConfigurator, mapper, imageFileRepository)
    {
        Context = context;
        BattleSettingsProvider = battleSettingsProvider;
    }

    protected override IQueryable<Book> GetSelectionQuery(GetBattleSelectionQuery request)
    {
        return BattleProvider.GetBattleBooks(Context, BattleSettingsProvider.GetBattleSettings());
    }
}

