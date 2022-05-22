using AutoMapper;
using BookStore.Application.Commands.Basket.GetBasketProducts;
using BookStore.Application.Commands.Orders.PlaceOrder;
using BookStore.Application.Queries.Orders.GetOrders;
using BookStore.Domain.Entities;
using System.Linq;

namespace BookStore.Application.Profiles
{
    internal class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketProduct, BasketProductDto>()
            .ForMember(dto => dto.Name, mapper
                => mapper.MapFrom(basketProduct => basketProduct.Product.Name))
            .ForMember(dto => dto.Cost, mapper
                => mapper.MapFrom(basketProduct => basketProduct.Product.Cost))
            .ForMember(dto => dto.Quantity, mapper
                => mapper.MapFrom(basketProduct => basketProduct.Quantity))
            .ForMember(dto => dto.TitleImage, mapper
                => mapper.MapFrom(basketProduct => basketProduct.Product.Album.Images
                  .Single(image => image.Name == basketProduct.Product.Album.TitleImageName)));

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
}
