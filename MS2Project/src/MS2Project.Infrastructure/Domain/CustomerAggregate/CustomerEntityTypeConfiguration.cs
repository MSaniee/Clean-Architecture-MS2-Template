using $ext_safeprojectname$.Domain.CustomerAggregate;

namespace $ext_safeprojectname$.Infrastructure.Domain.CustomerAggregate;

public sealed class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    internal const string OrdersList = "_orders";
    internal const string OrderProducts = "_orderProducts";

    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(b => b.Id);
        //builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.Property("_welcomeEmailWasSent").HasColumnName("WelcomeEmailWasSent");
        builder.Property("_email").HasColumnName("Email");
        builder.Property("_name").HasColumnName("Name");
    }
}

