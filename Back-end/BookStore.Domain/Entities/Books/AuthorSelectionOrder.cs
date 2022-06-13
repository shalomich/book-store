﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace BookStore.Domain.Entities.Books;
public class AuthorSelectionOrder : IEntity
{
    public int Id { set; get; }
    public DateTime SelectionDate { private set; get; } = DateTime.Now;
    public Author Author { set; get; }
    public int AuthorId { set; get; }
}

