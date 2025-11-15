using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Province;
using QuizLand.Application.Contract.QueryResults.Province;

namespace QuizLand.Api.Controllers.Provinces;

public class ProvinceQueryController : BaseQueryController
{
    public ProvinceQueryController(IQueryBus bus) : base(bus)
    {
    }
    
    [HttpGet("AllProvince")]
    public async Task<ActionResult<GetAllProvinceQueryResult>> AllUser([FromQuery]GetAllProvinceQuery query)
    {
        return Ok(await Bus.Dispatch<GetAllProvinceQuery,List<GetAllProvinceQueryResult>>(query));
    }
}