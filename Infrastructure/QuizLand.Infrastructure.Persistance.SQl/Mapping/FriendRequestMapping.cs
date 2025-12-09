using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizLand.Domain.Models.FriendRequests;

namespace QuizLand.Infrastructure.Persistance.SQl.Mapping;

public class FriendRequestMapping : IEntityTypeConfiguration<FriendRequest>
{
    public void Configure(EntityTypeBuilder<FriendRequest> builder)
    {
        builder.ToTable("FriendRequests").HasKey(f => f.Id);
        builder.HasOne(f => f.Requester).WithMany(f => f.ReuqesterFriendRequests).HasForeignKey(f => f.RequesterId).OnDelete(DeleteBehavior.NoAction);;
        builder.HasOne(f => f.Receiver).WithMany(f => f.ReciverFriendRequests).HasForeignKey(f => f.ReceiverId).OnDelete(DeleteBehavior.NoAction);;
    }
}