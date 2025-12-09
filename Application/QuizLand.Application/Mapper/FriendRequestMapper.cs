using QuizLand.Application.Contract.Commands.FriendRequest;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.QueryResults.FriendRequest;
using QuizLand.Domain.Models.FriendRequests;

namespace QuizLand.Application.Mapper;

public static class FriendRequestMapper
{
    public static FriendRequest Factory(this SendNewFriendRequestCommand command,Guid userId,Guid reciverId)
    {
        return new FriendRequest()
        {
            RequesterId = userId,
            ReceiverId = reciverId,
            CreatedAt = DateTime.Now,
            FriendRequestStatus = FriendRequestStatus.Pending
        };
    }

    public static List<AllMyFriendRequestsQueryResult> AllMapper(this List<FriendRequest> query)
    {
        return query.Select(f => new AllMyFriendRequestsQueryResult()
        {
            Id = f.Id,
            RequesterId = f.RequesterId,
            RequesterUsername = f.Requester.Username,
            SendedAt = f.CreatedAt.ToShamsiDate()
        }).ToList();
    }
}