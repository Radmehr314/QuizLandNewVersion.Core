using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizLand.Domain.Models.Supporters;

namespace QuizLand.Infrastructure.Persistance.SQl.Mapping;

public class SupporterMapping : IEntityTypeConfiguration<Supporter>
{
    public void Configure(EntityTypeBuilder<Supporter> builder)
    {
        builder.ToTable("Supporters").HasKey(f=>f.Id);
        builder.Property(f => f.Username).IsRequired();
        builder.Property(f => f.Password).IsRequired();
    }
}