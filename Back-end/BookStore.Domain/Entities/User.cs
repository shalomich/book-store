using BookStore.Domain.Entities.Battles;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Products;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entities;
public class User : IdentityUser<int>, IEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public int VotingPointCount { get; set; }

    public TelegramBotContact TelegramBotContact { get; set; }

    public IEnumerable<BasketProduct> BasketProducts { get; set; } = new List<BasketProduct>();
    public IEnumerable<Order> Orders { get; set; } = new List<Order>();

    public IEnumerable<Mark> Marks { get; set; } = new List<Mark>();
    public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
    public IEnumerable<View> Views { get; set; } = new List<View>();
    public IEnumerable<Vote> Votes { get; set; } = new List<Vote>();
}
