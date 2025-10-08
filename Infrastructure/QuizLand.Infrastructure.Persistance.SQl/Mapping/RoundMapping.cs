using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizLand.Domain.Models.Rands;

namespace QuizLand.Infrastructure.Persistance.SQl.Mapping;

public class RoundMapping : IEntityTypeConfiguration<Round>
{
    public void Configure(EntityTypeBuilder<Round> builder)
    {
        builder.ToTable("Rounds").HasKey(f=>f.Id);
        builder.HasOne(f => f.Game).WithMany(f => f.Rounds).HasForeignKey(f => f.GameId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(f => f.Course).WithMany(f => f.Rounds).HasForeignKey(f => f.CourseId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(f => f.Gamer).WithMany().HasForeignKey(f => f.SelectingGamerId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(f => f.FirstAnswerGamer).WithMany().HasForeignKey(f => f.FirstAnswerGamerId).OnDelete(DeleteBehavior.NoAction);

        
        builder.HasOne(x => x.FirstRoundQuestion).WithMany()
            .HasForeignKey(x => x.FirstRandQuestionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SecondRoundQuestion).WithMany()
            .HasForeignKey(x => x.SecondRandQuestionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.ThirdRoundQuestion).WithMany()
            .HasForeignKey(x => x.ThirdRandQuestionId)
            .OnDelete(DeleteBehavior.NoAction);
        
    }
}