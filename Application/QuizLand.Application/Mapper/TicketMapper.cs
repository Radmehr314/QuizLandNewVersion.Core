using System;
using System.Collections.Generic;
using System.Linq;
using QuizLand.Application.Contract.Commands.Ticket;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.QueryResults.CodeLog;
using QuizLand.Application.Contract.QueryResults.Ticket;
using QuizLand.Domain.Models.CodeLogs;
using QuizLand.Domain.Models.TicketMessages;
using QuizLand.Domain.Models.Tickets;

namespace QuizLand.Application.Mapper;

public static class TicketMapper
{
    public static Ticket Factory(this SubmitNewTicketCommand command, Guid userId)
    {
        return new Ticket()
        {
            UserId = userId,
            Priority = command.Priority,
            Subject = command.Subject,
            TicketStatus = TicketStatus.Open,
            CreatedAt = DateTime.Now,
            LastActivityAt = DateTime.Now,
            TicketMessages = new List<TicketMessage> { command.TicketMessageCommand.SendMessageMapper(userId) }
        };
    }
    
    
    
    public static List<GetAllTicketQueryResult> GetAllMapper(this List<Ticket> tickets)
    {
        return tickets
            .Select(f => new GetAllTicketQueryResult() {Id = f.Id, Subject = f.Subject,UserUsername = f.User.Username,UserId = f.UserId,TicketStatus = f.TicketStatus.ToFa() ,Priority = f.Priority.ToFa(),CreatedAt = f.CreatedAt.ToShamsiDate(),LastActivityAt = f.LastActivityAt.ToShamsiDate(),IsLock = f.IsLocked,IsAssigned = f.AssigneeSupporterId != null ? true : false })
            .ToList();
    }
    
    
    public static GetAllTicketPaginationQueryResult AllPagination(this List<Ticket> ticket, int pageSize,
        long count)
    {
        return new GetAllTicketPaginationQueryResult()
        {
            AllTicketQueryResults = ticket.GetAllMapper(),
            Count = count,
            PageSize = pageSize
        };
    }
    
    public static GetTicketByIdQueryResult GetByIdMapper(this Ticket query)
    {
        var ticket = new GetTicketByIdQueryResult()
        {
            Id = query.Id,
            Subject = query.Subject,
            Priority = query.Priority.ToFa(),
            LastActivityAt = query.LastActivityAt.ToShamsiDate(),
            CreatedAt = query.CreatedAt.ToShamsiDate(),
            IsAssigned = query.AssigneeSupporterId != null ? true : false,
            IsLock = query.IsLocked,
            UserId = query.UserId,
            TicketStatus = query.TicketStatus.ToFa(),
            UserUsername = query.User.Username,
            AllTicketMessageQueryResults = query.TicketMessages.AllMapper()
        };
        return ticket;
    }
}