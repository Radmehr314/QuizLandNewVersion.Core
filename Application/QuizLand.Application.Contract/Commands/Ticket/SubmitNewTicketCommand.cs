using QuizLand.Application.Contract.Commands.TicketMessage;
using QuizLand.Application.Contract.Framework;
using QuizLand.Domain.Models.TicketMessages;
using QuizLand.Domain.Models.Tickets;

namespace QuizLand.Application.Contract.Commands.Ticket;

public class SubmitNewTicketCommand : ICommand
{
    public string Subject { get; set; }
    public Priority Priority { get; set; }
    public Visibility Visibility { get; set; }
    public bool IsSupporter { get; set; }
    public SubmitTicketMessageCommand TicketMessageCommand { get; set; }
}