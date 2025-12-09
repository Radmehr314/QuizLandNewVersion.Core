using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizLand.Domain.Models.Friends;

namespace QuizLand.Infrastructure.Persistance.SQl.Mapping;

public class FriendMapper : IEntityTypeConfiguration<Friend>
{
    public void Configure(EntityTypeBuilder<Friend> builder)
    {
        builder.ToTable("Friends").HasKey(f => f.Id);
        builder.HasOne(f => f.FirstUser).WithMany(f => f.FirstUserFriends).HasForeignKey(f => f.FirstUserId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(f => f.SecondUser).WithMany(f => f.SecondUserFriends).HasForeignKey(f => f.SecondUserId).OnDelete(DeleteBehavior.NoAction);;
    }
}