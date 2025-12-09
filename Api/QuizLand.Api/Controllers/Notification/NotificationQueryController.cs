using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Notification;
using QuizLand.Application.Contract.QueryResults.Notification;

namespace QuizLand.Api.Controllers.Notification;

[Authorize]
public class NotificationQueryController : BaseQueryController
{
    public NotificationQueryController(IQueryBus bus) : base(bus)
    {
    }

    [HttpGet("AllMyNotifications")]
    public async Task<ActionResult<List<AllMyNotificationsQueryResult>>> AllMyNotifications(
        [FromQuery] AllMyNotificationsQuery query)
    {
        return Ok(await Bus.Dispatch<AllMyNotificationsQuery,List<AllMyNotificationsQueryResult>>(query));

    }
}