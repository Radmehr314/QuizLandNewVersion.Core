using Microsoft.EntityFrameworkCore;
using QuizLand.Domain.Models.CodeLogs;
using QuizLand.Domain.Models.Supporters;
using QuizLand.Domain.Models.TicketMessages;
using QuizLand.Domain.Models.Tickets;
using QuizLand.Domain.Models.Users;

namespace QuizLand.Infrastructure.Persistance.SQl;

public class DataBaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<CodeLogs> CodeLogs { get; set; }
    public DbSet<Supporter> Supporters { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketMessage> TicketMessages { get; set; }
    public DataBaseContext(DbContextOptions options) : base(options) 
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}