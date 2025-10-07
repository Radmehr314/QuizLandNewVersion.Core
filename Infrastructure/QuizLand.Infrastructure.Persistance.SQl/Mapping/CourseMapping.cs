using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizLand.Domain.Models.Courses;

namespace QuizLand.Infrastructure.Persistance.SQl.Mapping;

public class CourseMapping : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Course").HasKey(f=>f.Id);
    }
}