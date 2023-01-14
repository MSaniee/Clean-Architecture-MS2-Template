using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MS2Project.Domain.Core.Enums;
using MS2Project.Domain.CustomerAggregate.Orders;
using MS2Project.Domain.PaymentAggregate;

namespace MS2Project.Infrastructure.Domain.PaymentAggregate;

internal sealed class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments", SchemaNames.Payments);

        builder.HasKey(b => b.Id);

        builder.Property<DateTime>("_createDate").HasColumnName("CreateDate");
        builder.Property<OrderId>("_orderId").HasColumnName("OrderId");
        builder.Property("_status").HasColumnName("StatusId").HasConversion(new EnumToNumberConverter<PaymentStatus, byte>());
        builder.Property<bool>("_emailNotificationIsSent").HasColumnName("EmailNotificationIsSent");
    }
}
