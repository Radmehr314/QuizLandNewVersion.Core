using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizLand.Domain.Models.Notifications;

namespace QuizLand.Infrastructure.Persistance.SQl.Mapping;

public class NotificationMapping : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notifications").HasKey(f => f.Id);
        builder.HasOne(f => f.User).WithMany(f => f.Notifications).HasForeignKey(f => f.UserId);
    }
}