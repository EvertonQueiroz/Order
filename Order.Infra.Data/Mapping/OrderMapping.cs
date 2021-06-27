using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Infra.Data.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Domain.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(p => p.Number);
            builder.HasMany(p => p.Items)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata
                .FindNavigation("Items")
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
