using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizLand.Domain.Models.Gamers;

namespace QuizLand.Infrastructure.Persistance.SQl.Mapping;

public class GamerMapping : IEntityTypeConfiguration<Gamer>
{
    public void Configure(EntityTypeBuilder<Gamer> builder)
    {
        builder.ToTable("Gamers").HasKey(f=>f.Id);
        builder.HasOne(f=>f.Game).WithMany(g=>g.Gamers).HasForeignKey(f=>f.GameId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(f=>f.User).WithMany(g=>g.Gamers).HasForeignKey(f=>f.UserId).OnDelete(DeleteBehavior.NoAction);
    }
}