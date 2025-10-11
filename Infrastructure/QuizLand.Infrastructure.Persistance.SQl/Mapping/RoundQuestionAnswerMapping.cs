using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizLand.Domain.Models.RandQuestionAnswers;

namespace QuizLand.Infrastructure.Persistance.SQl.Mapping;

public class RoundQuestionAnswerMapping : IEntityTypeConfiguration<RoundQuestionAnswer>
{
    public void Configure(EntityTypeBuilder<RoundQuestionAnswer> builder)
    {
        builder.ToTable("RoundQuestionAnswers").HasKey(f=>f.Id);
        builder.HasOne(f=>f.Gamer).WithMany(f=>f.RoundQuestionAnswers).HasForeignKey(f=>f.GamerId);
        builder.HasOne(f=>f.RoundQuestion).WithMany(f=>f.RoundQuestionAnswers).HasForeignKey(f=>f.RoundQuestionId);
    }
}