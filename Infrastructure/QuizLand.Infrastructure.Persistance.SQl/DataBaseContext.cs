using Microsoft.EntityFrameworkCore;
using QuizLand.Domain.Models.Avatars;
using QuizLand.Domain.Models.CodeLogs;
using QuizLand.Domain.Models.Courses;
using QuizLand.Domain.Models.ErrorLogs;
using QuizLand.Domain.Models.Gamers;
using QuizLand.Domain.Models.Games;
using QuizLand.Domain.Models.Questions;
using QuizLand.Domain.Models.RandQuestionAnswers;
using QuizLand.Domain.Models.RandQuestions;
using QuizLand.Domain.Models.Rands;
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
    public DbSet<Course> Courses { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Avatar> Avatars { get; set; }
    public DbSet<Gamer> Gamers { get; set; }
    public DbSet<Round> Rounds { get; set; }
    public DbSet<RoundQuestion> RoundQuestions { get; set; }
    public DbSet<RoundQuestionAnswer> RoundQuestionAnswers { get; set; }
    public DbSet<ErrorLog> ErrorLogs { get; set; }
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