using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Avatar;
using QuizLand.Application.Contract.QueryResults.Avatar;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.QueryHandler;

public class AvatarQueryHandler : IQueryHandler<GetAvatarByIdQuery, GetAvatarByIdQueryResult>,IQueryHandler<AllAvatarQuery,List<AllAvatarQueryResult>>,IQueryHandler<AllAvatarPaginationQeury, AllAvatarPaginationQeuryResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public AvatarQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<GetAvatarByIdQueryResult> Handle(GetAvatarByIdQuery query)
    {
        var avatar = await _unitOfWork.AvatarRepository.GetById(query.Id);
        return avatar.GetAvatarMapper();
    }

    public async Task<List<AllAvatarQueryResult>> Handle(AllAvatarQuery query)
    {
        var avatar = await _unitOfWork.AvatarRepository.GetAll();
        return avatar.AllAvatarMapper();
    }

    public async Task<AllAvatarPaginationQeuryResult> Handle(AllAvatarPaginationQeury query)
    {
        var avatar = await _unitOfWork.AvatarRepository.GetAllPagination(query.pageNumber,query.size);
        var count = await _unitOfWork.AvatarRepository.Count();
        return avatar.AllAvatarPaginationMapper(count,query.size);

    }
}