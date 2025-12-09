using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.FriendRequest;
using QuizLand.Application.Contract.QueryResults.FriendRequest;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.QueryHandler;

public class FriendRequestQueryHandler : IQueryHandler<AllMyFriendRequestsQuery,List<AllMyFriendRequestsQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public FriendRequestQueryHandler(IUnitOfWork unitOfWork, IUserInfoService userInfoService)
    {
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }
    public async Task<List<AllMyFriendRequestsQueryResult>> Handle(AllMyFriendRequestsQuery query)
    {
        var requests = await _unitOfWork.FriendRequestRepository.AllMyReuests(_userInfoService.GetUserIdByToken());
        return requests.AllMapper();
    }
}