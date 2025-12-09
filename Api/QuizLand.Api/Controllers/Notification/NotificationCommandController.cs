using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Notification;

namespace QuizLand.Api.Controllers.Notification;

public class NotificationCommandController : BaseCommandController
{
    public NotificationCommandController(ICommandBus bus) : base(bus)
    {
    }

    [HttpPost("SeenNotifications")]
    public async Task<ActionResult<CommandResult>> SeenNotifications([FromBody] SeenNotificationsCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
}