using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Login;
using QuizLand.Application.Contract.Queries.User;
using QuizLand.Application.Contract.QueryResults.Login;
using QuizLand.Application.Contract.QueryResults.User;

namespace QuizLand.Api.Controllers.Users;


/*
[Authorize]
*/
public class UserQueryController : BaseQueryController
{
    public UserQueryController(IQueryBus bus) : base(bus)
    {
    }

    /*
    [HttpGet("GetById")]
    public async Task<ActionResult<GetUserByIdQueryResult>> GetUser([FromQuery]GetUserByIdQuery query)
    {
        return Ok(await Bus.Dispatch<GetUserByIdQuery,GetUserByIdQueryResult>(query));
    }
    */

    /*[HttpGet("GetLoginUser")]
    public async Task<ActionResult<GetLoginUserQueryResult>> GetUser([FromQuery] GetLoginUserQuery query)
    {
        return Ok(await Bus.Dispatch<GetLoginUserQuery, GetLoginUserQueryResult>(query));
    }*/

    /*[HttpGet("All")]
    public async Task<ActionResult<List<GetAllUserQueryResult>>> AllUser([FromQuery]GetAllUserQuery query)
    {
        return Ok(await Bus.Dispatch<GetAllUserQuery,List<GetAllUserQueryResult>>(query));
    }*/
    
    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginDto>> Login([FromBody] LoginRequestDto query)
    {
        return Ok(await Bus.Dispatch<LoginRequestDto,LoginDto>(query));

    }
    
    [Authorize]
    [HttpGet("LoginUserInfo")]
    public async Task<ActionResult<GetLoginUserInfoQueryResult>> Login([FromQuery] GetLoginUserInfoQuery query)
    {
        return Ok(await Bus.Dispatch<GetLoginUserInfoQuery,GetLoginUserInfoQueryResult>(query));

    }

    /*[HttpGet("GetAllPagination")]
    public async Task<ActionResult<GetAllUserPaginationQueryResult>> GetAllPagination(
        [FromQuery] GetAllUserPaginationQuery query)
    {
        return Ok(await Bus.Dispatch<GetAllUserPaginationQuery,GetAllUserPaginationQueryResult>(query));
    }*/
    
    [HttpGet("CountOnlineUsers")]
    public async Task<ActionResult<CountOfOnlineUserQueryResult>> GetAllPagination(
        [FromQuery] CountOfOnlineUserQuery query)
    {
        return Ok(await Bus.Dispatch<CountOfOnlineUserQuery,CountOfOnlineUserQueryResult>(query));
    }
    
    [HttpGet("CountAllUsers")]
    public async Task<ActionResult<CountAllUserQueryResult>> GetAllPagination(
        [FromQuery]CountAllUserQuery  query)
    {
        return Ok(await Bus.Dispatch<CountAllUserQuery,CountAllUserQueryResult>(query));
    }
    
    /*[HttpGet("ForgetPhoneNumber")]
    public async Task<ActionResult<ForgetPhoneNumberQueryResult>> GetAllPagination(
        [FromQuery]ForgetPhoneNumberQuery  query)
    {
        return Ok(await Bus.Dispatch<ForgetPhoneNumberQuery,ForgetPhoneNumberQueryResult>(query));
    }*/
    
    [HttpGet("GetCodeForForgetPassword")]
    public async Task<ActionResult<GetCodeForForgetPasswordQueryResult>> GetAllPagination(
        [FromQuery]GetCodeForForgetPasswordQuery  query)
    {
        return Ok(await Bus.Dispatch<GetCodeForForgetPasswordQuery,GetCodeForForgetPasswordQueryResult>(query));
    }
    
    [HttpGet("GetCodeForUserValidation")]
    public async Task<ActionResult<GetCodeForUserValidationQueryResult>> GetAllPagination(
        [FromQuery]GetCodeForUserValidationQuery  query)
    {
        return Ok(await Bus.Dispatch<GetCodeForUserValidationQuery,GetCodeForUserValidationQueryResult>(query));
    }
    
    
    /*[HttpGet("GetUserByPersonelCode")]
    public async Task<ActionResult<GetUserByPersonelCodeQueryResult>> GetAllPagination(
        [FromQuery]GetUserByPersonelCodeQuery  query)
    {
        return Ok(await Bus.Dispatch<GetUserByPersonelCodeQuery,GetUserByPersonelCodeQueryResult>(query));
    }*/
}