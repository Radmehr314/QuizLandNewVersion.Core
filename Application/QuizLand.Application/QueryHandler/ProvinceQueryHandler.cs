using DocumentFormat.OpenXml.Office2010.ExcelAc;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Province;
using QuizLand.Application.Contract.QueryResults.Province;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.QueryHandler;

public class ProvinceQueryHandler : IQueryHandler<GetAllProvinceQuery,List<GetAllProvinceQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProvinceQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<GetAllProvinceQueryResult>> Handle(GetAllProvinceQuery query)
    {
        var provinces = await _unitOfWork.ProvinceRepository.GetAll();
        return provinces.GetAllMapper();
    }
}