using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizLand.Domain.Models.Avatars;

namespace QuizLand.Infrastructure.Persistance.SQl.Mapping;

public class AvatarMapping : IEntityTypeConfiguration<Avatar>
{
    public void Configure(EntityTypeBuilder<Avatar> builder)
    {
        builder.ToTable("Avatars").HasKey(f=>f.Id);
        
    }
}