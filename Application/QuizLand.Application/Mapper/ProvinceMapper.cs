using DocumentFormat.OpenXml.Office2010.ExcelAc;
using QuizLand.Application.Contract.QueryResults.Province;
using QuizLand.Domain.Models.Provinces;

namespace QuizLand.Application.Mapper;

public static class ProvinceMapper
{
    public static List<GetAllProvinceQueryResult> GetAllMapper( this List<Province> provinces)
    {
        return provinces.Select(f => new GetAllProvinceQueryResult() { Id = f.Id, Title = f.Title }).ToList();
    }
}