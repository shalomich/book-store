using Abp.Specifications;
using BookStore.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace BookStore.Application.DbQueryConfigs.Specifications;

public class HasConfirmedSubsciptionSpecification : Specification<User>
{
    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => user.Subscription.TelegramId != null;
    }
}

