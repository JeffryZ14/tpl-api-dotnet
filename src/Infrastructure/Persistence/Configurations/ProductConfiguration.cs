using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");
               builder.Property(p => p.Id)
                   .HasConversion(
                       idVo => idVo.Value,             
                       guid => ProductId.From(guid))   
                   .ValueGeneratedNever()
                   .HasColumnName("id");

            builder.OwnsOne(p => p.Name, nav =>
            {
                nav.Property(n => n.Value)
                   .HasColumnName("name")
                   .HasMaxLength(100)
                   .IsRequired();
            });

            builder.OwnsOne(p => p.Price, nav =>
            {
                nav.Property(m => m.Amount)
                   .HasColumnName("price")
                   .IsRequired();

                nav.Property(m => m.Currency)
                   .HasColumnName("currency")
                   .HasMaxLength(3)
                   .IsRequired();
            });

            builder.Property(p => p.IsActive)
                .HasColumnName("is_active")
                   .HasDefaultValue(true);

           /* builder.Property(p => p.CreatedOn)
                   .IsRequired();
            builder.Property(p => p.CreatedBy)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(p => p.ModifiedOn);
            builder.Property(p => p.ModifiedBy)
                   .HasMaxLength(100);*/
        }
    }

}
