using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Commands.Supporter;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Notification;

namespace QuizLand.Api.Controllers.Notifications;

public class NotificationCommandController : BaseCommandController
{
    public NotificationCommandController(ICommandBus bus) : base(bus)
    {
    }
    
    [HttpPost("SendNotification")]
    public async Task<ActionResult<CommandResult>> SendNotification([FromBody]SendNotificationCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
}