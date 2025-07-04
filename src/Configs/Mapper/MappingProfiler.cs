using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Dtos.Category;
using AdminDashboard.src.Dtos.Order;
using AdminDashboard.src.Dtos.Product;
using AdminDashboard.src.Dtos.Role;
using AdminDashboard.src.Dtos.User;
using AdminDashboard.src.Entities;
using AutoMapper;

namespace AdminDashboard.src.Configs
{
    public class MappingProfiler : Profile
    {
        public MappingProfiler()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Role, RoleDto>();
            CreateMap<RoleCreateDto, Role>();
            CreateMap<AssignRoleToUserDto, User>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>()
                .ForMember(dest => dest.Price, act => act.Condition(src => src.Price > 0))
                .ForMember(dest => dest.SKU, act => act.Condition(src => !string.IsNullOrEmpty(src.SKU)))
                .ForMember(dest => dest.CategoryId, act => act.Condition(src => src.CategoryId != Guid.Empty))
                .ForMember(dest => dest.ImageUrl, act => act.Condition(src => !string.IsNullOrEmpty(src.ImageUrl)))
                .ForMember(dest => dest.ProductName, act => act.Condition(src => !string.IsNullOrEmpty(src.ProductName)))
                .ForMember(dest => dest.Description, act => act.Condition(src => !string.IsNullOrEmpty(src.Description)));

            CreateMap<CreateOrderItemDto, OrderItem>()
                .ForMember(dest => dest.UnitPrice, opt => opt.Ignore());
            CreateMap<OrderItemDto, OrderItem>()
                .ForMember(dest => dest.OrderItemId, opt => opt.Ignore())
                .ForMember(dest => dest.OrderId, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));
            CreateMap<OrderCreateDto, Order>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => OrderStatus.Pending))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore());
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.UnitPrice));
        }
    }
}