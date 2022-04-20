﻿
using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities.Battles;
public class Battle : IEntity
{
    public int Id { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTimeOffset EndDate { get; set; }
    public IEnumerable<BattleBook> BattleBooks { get; set; } = new List<BattleBook>();
}
