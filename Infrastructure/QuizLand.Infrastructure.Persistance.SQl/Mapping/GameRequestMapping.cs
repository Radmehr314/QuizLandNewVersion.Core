using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizLand.Domain.Models.GameRequests;

namespace QuizLand.Infrastructure.Persistance.SQl.Mapping;

public class GameRequestMapping : IEntityTypeConfiguration<GameRequest>
{
    public void Configure(EntityTypeBuilder<GameRequest> builder)
    {
        builder.ToTable("GameRequests").HasKey(f => f.Id);
        builder.HasOne(f => f.FirstUser).WithMany(f => f.FirstUserGameRequest).HasForeignKey(f => f.FirstUserId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(f => f.SecondUser).WithMany(f => f.SecondUserGameRequest).HasForeignKey(f => f.SecondUserId)
            .OnDelete(DeleteBehavior.NoAction);
        
    }
}