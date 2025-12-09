using Microsoft.Extensions.Configuration;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Friend;
using QuizLand.Application.Contract.QueryResults.Friend;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.QueryHandler;

public class FriendQueryHandler : IQueryHandler<AllMyFriendQuery,List<AllMyFriendQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IConfiguration _config;


    public FriendQueryHandler(IUnitOfWork unitOfWork, IUserInfoService userInfoService, IConfiguration config)
    {
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _config = config;
    }
    public async Task<List<AllMyFriendQueryResult>> Handle(AllMyFriendQuery query)
    {
        var friends = await _unitOfWork.FriendRepository.AllMyFriend(_userInfoService.GetUserIdByToken());
        var BaseXp = Convert.ToInt32(_config["Leveling:BaseXp"]
                                     ?? throw new InvalidOperationException("Missing Leveling:BaseXp"));
        var GrowXp =Convert.ToInt32(_config["Leveling:GrowXp"]
                                    ?? throw new InvalidOperationException("Missing Leveling:GrowXp"));
        return friends.AllMyFriend(_userInfoService.GetUserIdByToken(),BaseXp,GrowXp);
    }
}