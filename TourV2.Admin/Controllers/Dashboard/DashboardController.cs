using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.MediatR.Queries;

namespace TourV2.Admin.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    //[Authorize]
    public class DashboardController : ControllerBase
    {

        public IMediator _mediator { get; set; }

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Total Tours
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTotalTourCount")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetTotalTourCount()
        {
            var getQuery = new GetTotalTourCountQuery { };
            var result = await _mediator.Send(getQuery);
            return Ok(result);
        }

        /// <summary>
        /// Get Total Tours
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTotalActiveTourCount")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetTotalActiveTourCount()
        {
            var getQuery = new GetTotalActiveTourCountQuery { };
            var result = await _mediator.Send(getQuery);
            return Ok(result);
        }
        /// <summary>
        /// Get Total Tours
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTotalReservationCount")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetTotalReservationCount()
        {
            var getQuery = new GetTotalReservationCountQuery { };
            var result = await _mediator.Send(getQuery);
            return Ok(result);
        }

        /// <summary>
        /// Get Total Tours
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTotalNewReservationCount")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetTotalNewReservationCount()
        {
            var getQuery = new GetTotalNewReservationCountQuery { };
            var result = await _mediator.Send(getQuery);
            return Ok(result);
        }

        /// <summary>
        /// Get Active User Count
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetActiveUserCount")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetActiveUserCount()
        {
            var getUserQuery = new GetActiveUserCountQuery { };
            var result = await _mediator.Send(getUserQuery);
            return Ok(result);
        }

        /// <summary>
        /// Get Inactive User Count
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetInactiveUserCount")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetInactiveUserCount()
        {
            var getUserQuery = new GetInactiveUserCountQuery { };
            var result = await _mediator.Send(getUserQuery);
            return Ok(result);
        }

        /// <summary>
        /// Get Total user count
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTotalUserCount")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetTotalUserCount()
        {
            var getUserQuery = new GetTotalUserCountQuery { };
            var result = await _mediator.Send(getUserQuery);
            return Ok(result);
        }

        /// <summary>
        /// Gets the online users.
        /// </summary>
        /// <param name="userIds">The user ids.</param>
        /// <returns></returns>
        [HttpGet("GetOnlineUsers")]
        [Produces("application/json", "application/xml", Type = typeof(List<UserDto>))]
        public async Task<IActionResult> GetOnlineUsers()
        {
            var query = new GetOnlineUsersQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
