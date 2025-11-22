using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizLand.Domain.Models.QuestionReports;

namespace QuizLand.Infrastructure.Persistance.SQl.Mapping;

public class QuestionReportMapping : IEntityTypeConfiguration<QuestionReport>
{
    public void Configure(EntityTypeBuilder<QuestionReport> builder)
    {
        builder.ToTable("QuestionReports").HasKey(f=>f.Id);
        builder.HasOne(f=>f.User).WithMany(f=>f.QuestionReports).HasForeignKey(f=>f.UserId);
        builder.HasOne(f=>f.Question).WithMany(f=>f.QuestionReports).HasForeignKey(f=>f.QuestionId);
    }
}