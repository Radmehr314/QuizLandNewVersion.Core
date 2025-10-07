using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizLand.Domain.Models.Questions;

namespace QuizLand.Infrastructure.Persistance.SQl.Mapping;

public class QuestionMapping : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Question").HasKey(f => f.Id);
        builder.HasOne(f=>f.Course).WithMany(f=>f.Questions).HasForeignKey(f=>f.CourseId).OnDelete(DeleteBehavior.NoAction);
    }
}