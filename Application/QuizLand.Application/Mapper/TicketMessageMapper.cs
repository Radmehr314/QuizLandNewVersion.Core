using System;
using System.Collections.Generic;
using System.Linq;
using QuizLand.Application.Contract.Commands.Ticket;
using QuizLand.Application.Contract.Commands.TicketMessage;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.QueryResults.Ticket;
using QuizLand.Application.Contract.QueryResults.TicketMesage;
using QuizLand.Domain.Models.TicketMessages;
using QuizLand.Domain.Models.Tickets;

namespace QuizLand.Application.Mapper;

public static class TicketMessageMapper
{
    public static TicketMessage Factory(this SubmitNewTicketCommand command,Ticket ticket, Guid userId)
    {
        
        return new TicketMessage()
        {
            Body = command.TicketMessageCommand.Content,
            Visibility = command.Visibility,
            Ticketid = ticket.Id,
            SenderSupporterId  = command.IsSupporter ? userId : (Guid?)null,
            SenderUserId       = command.IsSupporter ? (Guid?)null : userId,
            SentAt = DateTime.Now
        };
    }

    public static TicketMessage SendMessageMapper(this SubmitTicketMessageCommand command,Guid UserId)
    {
        return new TicketMessage()
        {
            Body = command.Content,
            Visibility = command.Visibility,
            SenderSupporterId = command.IsSupporter ? UserId : null,
            SenderUserId = !command.IsSupporter ? UserId : null,
            SentAt = DateTime.Now,
            /*
            Ticketid = command.TicketId,
            */
            IsSupporter = command.IsSupporter,
            ReplyTo = command.ReplyTo
        };
    }

    public static TicketMessage SendMessageInTicketMapper(this SubmitNewMessageInTicketCommand command,Guid UserId)
    {
        return new TicketMessage()
        {
            Body = command.Content,
            Visibility = command.Visibility,
            SenderSupporterId = command.IsSupporter ? UserId : null,
            SenderUserId = !command.IsSupporter ? UserId : null,
            SentAt = DateTime.Now,
            Ticketid = command.TicketId,
            IsSupporter = command.IsSupporter,
            ReplyTo = command.ReplyTo
        };
    }
    public static IEnumerable<AllTicketMessageQueryResult> AllMapper(this IEnumerable<TicketMessage> query)
    {
        return query.Select(f=> new  AllTicketMessageQueryResult(){Id = f.Id,TicketId = f.Ticketid,Visibility =f.Visibility.ToFa(),SentAt = f.SentAt.ToShamsiDate(),IsSupporter = f.SenderSupporterId != null ? true : false,Body = f.Body,ReplyTo = f.ReplyTo,UserId = f.SenderSupporterId !=  null ? (Guid)f.SenderSupporterId : (Guid)f.SenderUserId,Username = f.SenderSupporterId != null ? f.SenderSupporter.Username : f.SenderUser.Username}).ToList();
    }
}