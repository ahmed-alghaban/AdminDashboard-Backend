using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Configs;

namespace AdminDashboard.src.Dtos.Order
{
    public class OrderCreateDto
{
    public Guid UserId { get; set; }
    public string PaymentMethod { get; set; }
    public string ShippingAddress { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
}
}