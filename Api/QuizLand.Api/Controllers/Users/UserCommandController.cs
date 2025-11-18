using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Framework.Minimal.Security;
using QuizLand.Application.Contract.Commands.User;
using QuizLand.Application.Contract.Framework;

namespace QuizLand.Api.Controllers.Users;

/*
[Authorize]
*/
/*
[RequireDeviceSignature]
*/

public class UserCommandController : BaseCommandController
{
    public UserCommandController(ICommandBus bus) : base(bus)
    {
    }
    
    /*
    [HttpPost("AddUser")]
    public async Task<ActionResult<CommandResult>> AddUser([FromBody]AddUserCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    */
    
    /*
    [HttpPut("UpdateUser")]
    public async Task<ActionResult<CommandResult>> UpdateUser([FromBody]UpdateUserCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    */

    /*[HttpDelete("DeleteUser")]
    public async Task<ActionResult<CommandResult>> DeleteUser([FromQuery] DeleteUserCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }*/

    [HttpPut("RegisterUser")]
    public async Task<ActionResult<CommandResult>> RegisterUser([FromBody] RegisterUserCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }

    [HttpPut("MakeOnlineUser")]
    public async Task<ActionResult<CommandResult>> MakeOnlineUser([FromBody] MakeOnlineUserCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    [HttpPut("MakeOfflineUser")]
    public async Task<ActionResult<CommandResult>> MakeOfflineUser([FromBody] MakeOfflineUserCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }

    [HttpPut("ForgetPassword")]
    public async Task<ActionResult<CommandResult>> ForgetPassword([FromBody] ForgetPasswordCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    [HttpPut("UserStatus")]
    public async Task<ActionResult<CommandResult>> UserStatus([FromBody] UserStatusComamnd command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    
    [HttpPut("ReduceTheCoinForStopTimerCommand")]
    public async Task<ActionResult<CommandResult>> UserStatus([FromBody] ReduceTheCoinForStopTimerCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }

    
    [HttpPut("ReduceTheCoinForSecondChanceCommand")]
    public async Task<ActionResult<CommandResult>> UserStatus([FromBody] ReduceTheCoinForSecondChanceCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }

    
    [HttpPut("ReduceTheCoinForEliminateTwoOptionCommand")]
    public async Task<ActionResult<CommandResult>> UserStatus([FromBody] ReduceTheCoinForEliminateTwoOptionCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }

    [HttpPut("ReduceTheCoinForShowThePercentageOfPeopleClickCommand")]
    public async Task<ActionResult<CommandResult>> UserStatus([FromBody] ReduceTheCoinForShowThePercentageOfPeopleClickCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }

    
    /*[HttpPost("UserValidation")]
    public async Task<ActionResult<CommandResult>> UserValidation([FromBody] VerifyUserCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }*/
}