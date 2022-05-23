using AutoMapper;
using BookStore.Application.Commands.Orders.PlaceOrder;
using BookStore.Application.Queries.Orders.GetOrders;
using BookStore.Domain.Entities;

namespace BookStore.Application.Commands.Orders;
internal class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderForm, Order>()
            .ForMember(order => order.UserName, mapper =>
                mapper.MapFrom(form => $"{form.LastName} {form.FirstName}"));

        CreateMap<BasketProduct, OrderProduct>()
           .ForMember(orderProduct => orderProduct.Name, mapper =>
               mapper.MapFrom(basketProduct => basketProduct.Product.Name))
           .ForMember(orderProduct => orderProduct.Cost, mapper =>
               mapper.MapFrom(basketProduct => basketProduct.Product.Cost))
           .ForMember(orderProduct => orderProduct.Id, mapper =>
               mapper.Ignore()); ;

        CreateMap<Order, OrderDto>()
            .ForMember(orderProduct => orderProduct.PlacedDate, mapper =>
               mapper.MapFrom(basketProduct => basketProduct.PlacedDate.ToString("dd.MM.yyyy")))
            .ForMember(orderProduct => orderProduct.DeliveredDate, mapper =>
               mapper.MapFrom(basketProduct => basketProduct.DeliveredDate.Value.ToString("dd.MM.yyyy")));

        CreateMap<OrderProduct, OrderProductDto>();
    }
}

