using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizLand.Domain.Models.RandQuestions;

namespace QuizLand.Infrastructure.Persistance.SQl.Mapping;

public class RounQuestionMapping : IEntityTypeConfiguration<RoundQuestion>
{
    public void Configure(EntityTypeBuilder<RoundQuestion> builder)
    {
        builder.ToTable("RoundQuestions").HasKey(f=>f.Id);
        builder.HasOne(f=>f.Round).WithMany().HasForeignKey(f=>f.RoundId).OnDelete(DeleteBehavior.NoAction);
    }
}