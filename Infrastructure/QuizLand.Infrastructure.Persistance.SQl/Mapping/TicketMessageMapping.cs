using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizLand.Domain.Models.TicketMessages;

namespace QuizLand.Infrastructure.Persistance.SQl.Mapping;

public class TicketMessageMapping : IEntityTypeConfiguration<TicketMessage>
{
    public void Configure(EntityTypeBuilder<TicketMessage> builder)
    {
        builder.ToTable("TicketMessages").HasKey(x => x.Id);
        builder.HasOne(f => f.Ticket).WithMany(f => f.TicketMessages).HasForeignKey(f => f.Ticketid).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne<TicketMessage>().WithMany().HasForeignKey(f=>f.ReplyTo).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.SenderUser).WithMany(u => u.TicketMessages).HasForeignKey(x => x.SenderUserId).OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SenderSupporter).WithMany(s => s.TicketMessages).HasForeignKey(x => x.SenderSupporterId).OnDelete(DeleteBehavior.NoAction);
    }
}