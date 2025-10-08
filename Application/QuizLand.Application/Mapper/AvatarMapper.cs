using QuizLand.Application.Contract.Commands.Avatar;
using QuizLand.Application.Contract.QueryResults.Avatar;
using QuizLand.Domain.Models.Avatars;

namespace QuizLand.Application.Mapper;

public static class AvatarMapper
{
    public static Avatar AddAvatarFactory(this string avatarPath)
    {
        return new Avatar()
        {
            FilePath = avatarPath
        };
    }
    
    public static GetAvatarByIdQueryResult GetAvatarMapper(this Avatar avatar)
    {
        return new GetAvatarByIdQueryResult()
        {
            Id = avatar.Id,
            FilePath = avatar.FilePath
        };
    }
    
    public static List<AllAvatarQueryResult> AllAvatarMapper(this List<Avatar> avatars)
    {
      return avatars.Select(f=>new AllAvatarQueryResult(){Id = f.Id, FilePath = f.FilePath}).ToList();
    }
    public static AllAvatarPaginationQeuryResult AllAvatarPaginationMapper(this List<Avatar> avatars,long count , int pageSize)
    {
        return new AllAvatarPaginationQeuryResult()
        {
            AllAvatar = avatars.AllAvatarMapper(),
            Count = count,
             PageSize = pageSize,
        };
    }
}