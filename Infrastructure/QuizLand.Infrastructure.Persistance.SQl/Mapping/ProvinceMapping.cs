using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizLand.Domain.Models.Provinces;

namespace QuizLand.Infrastructure.Persistance.SQl.Mapping;

public class ProvinceMapping : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.ToTable("Provinces").HasKey(f=>f.Id);
    }
}