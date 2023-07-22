using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(x => x.DeliveryAddress, a =>
            {
                a.WithOwner();
            });

            builder.Property(x => x.Status)
                .HasConversion(
                c => c.ToString(),
                p => (OrderStatus)Enum.Parse(typeof(OrderStatus), p)
                );

            builder.HasMany(x => x.OrderItems)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
