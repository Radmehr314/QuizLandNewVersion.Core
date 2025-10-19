using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizLand.Domain.Models.ErrorLogs;

namespace QuizLand.Infrastructure.Persistance.SQl.Mapping;

public class ErrorLogMapping : IEntityTypeConfiguration<ErrorLog>
{
    public void Configure(EntityTypeBuilder<ErrorLog> builder)
    {
        builder.ToTable("ErrorLogs").HasKey(f=>f.Id);
    }
}