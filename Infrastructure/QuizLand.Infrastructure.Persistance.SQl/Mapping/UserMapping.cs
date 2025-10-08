using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizLand.Domain.Models.Users;

namespace QuizLand.Infrastructure.Persistance.SQl.Mapping;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(f => f.Id);
        builder.Property(f => f.Username).IsRequired();
        builder.Property(f => f.PhoneNumber).IsRequired();
        builder.HasOne(f => f.Avatar).WithMany(f => f.Users).HasForeignKey(f => f.AvatarId).OnDelete(DeleteBehavior.NoAction);

    }
}