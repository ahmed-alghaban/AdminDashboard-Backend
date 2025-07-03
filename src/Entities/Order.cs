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
    public  string UserFullName { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public string PaymentMethod { get; set; }
    public string ShippingAddress { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}
}