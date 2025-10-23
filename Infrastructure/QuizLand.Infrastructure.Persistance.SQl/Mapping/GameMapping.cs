using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizLand.Domain.Models.Games;

namespace QuizLand.Infrastructure.Persistance.SQl.Mapping;

public class GameMapping : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Games").HasKey(f=>f.Id);
        builder.HasOne(f=>f.Winner).WithMany(f=>f.Games).HasForeignKey(f=>f.WinnerUserId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(f=>f.UserTurn).WithMany(f=>f.MyTurnGames).HasForeignKey(f=>f.UserTurnId).OnDelete(DeleteBehavior.NoAction);
    }
}