using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizLand.Domain.Models.Tickets;

namespace QuizLand.Infrastructure.Persistance.SQl.Mapping;

public class TicketMapping : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets").HasKey(f=>f.Id);
        builder.HasOne(f=>f.User).WithMany(f=>f.Tickets).HasForeignKey(f=>f.UserId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(f=>f.Supporter).WithMany(f=>f.Tickets).HasForeignKey(f=>f.AssigneeSupporterId).OnDelete(DeleteBehavior.NoAction);
    }
}