using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourV2.Data.Dto;
using TourV2.MediatR.Commands;
using TourV2.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TourV2.API.Controllers
{
    /// <summary>
    /// Role
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class RoleController : BaseController
    {
        public IMediator _mediator { get; set; }
        /// <summary>
        /// Role
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public RoleController(
            IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Create A Role
        /// </summary>
        /// <param name="addRoleCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(RoleDto))]
        public async Task<IActionResult> AddRole(AddRoleCommand addRoleCommand)
        {
            var result = await _mediator.Send(addRoleCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return CreatedAtAction("GetRole", new { id = result.Data.Id }, result.Data);
        }
        /// <summary>
        /// Update Exist Role By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateRoleCommand"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(RoleDto))]
        public async Task<IActionResult> UpdateRole(Guid id, UpdateRoleCommand updateRoleCommand)
        {
            updateRoleCommand.Id = id;
            var result = await _mediator.Send(updateRoleCommand);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get Role By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}", Name = "GetRole")]
        [Produces("application/json", "application/xml", Type = typeof(RoleDto))]
        public async Task<IActionResult> GetRole(Guid id)
        {
            var getRoleQuery = new GetRoleQuery { Id = id };

            var result = await _mediator.Send(getRoleQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetRoles")]
        [Produces("application/json", "application/xml", Type = typeof(List<RoleDto>))]
        public async Task<IActionResult> GetRoles()
        {
            var getAllRoleQuery = new GetAllRoleQuery { };
            var result = await _mediator.Send(getAllRoleQuery);
            return Ok(result);
        }
        /// <summary>
        /// Delete Role By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteRole(Guid Id)
        {
            var deleteRoleCommand = new DeleteRoleCommand { Id = Id };
            var result = await _mediator.Send(deleteRoleCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
