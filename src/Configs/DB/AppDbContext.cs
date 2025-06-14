using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Entities;
using Microsoft.EntityFrameworkCore;
namespace AdminDashboard.src.Configs
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(user => user.UserId).HasDefaultValueSql("uuid_generate_v4()");
                entity.Property(user => user.FirstName).IsRequired();
                entity.Property(user => user.LastName).IsRequired();
                entity.Property(user => user.Email).IsRequired();
                entity.HasIndex(user => user.Email).IsUnique();
                entity.Property(user => user.PasswordHash).IsRequired();
                entity.Property(user => user.PhoneNumber).IsRequired();
                entity.HasIndex(user => user.PhoneNumber).IsUnique();
                entity.Property(user => user.RoleId).IsRequired();
                entity.Property(user => user.Status).IsRequired();
                entity.Property(user => user.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(user => user.LastLoginAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(user => user.ProfileImageUrl).IsRequired();

                entity.HasOne(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.RoleId);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(role => role.RoleId);
                entity.Property(role => role.RoleId).HasDefaultValueSql("uuid_generate_v4()");
                entity.Property(role => role.Name).IsRequired();
                entity.HasIndex(role => role.Name).IsUnique();
                entity.Property(role => role.Description).IsRequired();
            });

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(auditLog => auditLog.AuditLogId);
                entity.Property(auditLog => auditLog.AuditLogId).HasDefaultValueSql("uuid_generate_v4()");
                entity.Property(auditLog => auditLog.UserId).IsRequired();
                entity.Property(auditLog => auditLog.ActionType).IsRequired();
                entity.Property(auditLog => auditLog.EntityName).IsRequired();
                entity.Property(auditLog => auditLog.EntityId).IsRequired();
                entity.Property(auditLog => auditLog.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(auditLog => auditLog.IpAddress).IsRequired();
                entity.Property(auditLog => auditLog.Description).IsRequired();

                entity.HasOne(auditLog => auditLog.User)
                .WithMany(user => user.AuditLogs)
                .HasForeignKey(auditLog => auditLog.UserId);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(order => order.OrderId);
                entity.Property(order => order.OrderId).HasDefaultValueSql("uuid_generate_v4()");
                entity.Property(order => order.UserId).IsRequired();
                entity.Property(order => order.OrderDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(order => order.TotalAmount).IsRequired();
                entity.Property(order => order.Status).IsRequired();
                entity.Property(order => order.PaymentMethod).IsRequired();
                entity.Property(order => order.ShippingAddress).IsRequired();

                entity.HasOne(order => order.User)
                .WithMany(user => user.Orders)
                .HasForeignKey(order => order.UserId);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(orderItem => orderItem.OrderItemId);
                entity.Property(orderItem => orderItem.OrderItemId).HasDefaultValueSql("uuid_generate_v4()");
                entity.Property(orderItem => orderItem.OrderId).IsRequired();
                entity.Property(orderItem => orderItem.ProductId).IsRequired();
                entity.Property(orderItem => orderItem.Quantity).IsRequired();
                entity.Property(orderItem => orderItem.UnitPrice).IsRequired();

                entity.HasOne(orderItem => orderItem.Order)
                .WithMany(order => order.OrderItems)
                .HasForeignKey(orderItem => orderItem.OrderId);

                entity.HasOne(orderItem => orderItem.Product)
                .WithMany(product => product.OrderItems)
                .HasForeignKey(orderItem => orderItem.ProductId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(product => product.ProductId);
                entity.Property(product => product.ProductId).HasDefaultValueSql("uuid_generate_v4()");
                entity.Property(product => product.ProductName).IsRequired();
                entity.Property(product => product.Description).IsRequired();
                entity.Property(product => product.SKU).IsRequired();
                entity.Property(product => product.Price).IsRequired();
                entity.Property(product => product.QuantityInStock).IsRequired();
                entity.Property(product => product.CategoryId).IsRequired();
                entity.Property(product => product.ImageUrl).IsRequired();
                entity.Property(product => product.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(product => product.IsActive).IsRequired();

                entity.HasOne(product => product.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.CategoryId);

                entity.HasOne(product => product.Inventory)
                .WithOne(inventory => inventory.Product)
                .HasForeignKey<Inventory>(inventory => inventory.ProductId);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(inventory => inventory.InventoryId);
                entity.Property(inventory => inventory.InventoryId).HasDefaultValueSql("uuid_generate_v4()");
                entity.Property(inventory => inventory.ProductId).IsRequired();
                entity.Property(inventory => inventory.QuantityAvailable).IsRequired();
                entity.Property(inventory => inventory.ReorderLevel).IsRequired();
                entity.Property(inventory => inventory.LastRestockedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(inventory => inventory.Product)
                .WithOne(product => product.Inventory)
                .HasForeignKey<Product>(product => product.ProductId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(category => category.CategoryId);
                entity.Property(category => category.CategoryId).HasDefaultValueSql("uuid_generate_v4()");
                entity.Property(category => category.Name).IsRequired();
                entity.Property(category => category.Description).IsRequired();
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasKey(setting => setting.SettingId);
                entity.Property(setting => setting.SettingId).HasDefaultValueSql("uuid_generate_v4()");
                entity.Property(setting => setting.Key).IsRequired();
                entity.Property(setting => setting.Value).IsRequired();
                entity.Property(setting => setting.Category).IsRequired();
                entity.Property(setting => setting.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(setting => setting.UpdatedBy).IsRequired();
            });
        }
    }
}