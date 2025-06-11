using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Configs;

namespace AdminDashboard.src.Entities
{
   public class Order
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public string PaymentMethod { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;

    public ICollection<OrderItem> OrderItems { get; set; }
}
}