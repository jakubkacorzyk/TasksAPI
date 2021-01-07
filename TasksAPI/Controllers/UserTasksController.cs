using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TasksCORE.DTOs;
using TasksCORE.Services.UserTasks;
using TasksEntities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TasksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CORSPolicy")]
    public class UserTasksController : ControllerBase
    {
        private readonly IUserTasksService _userTasksService;

        public UserTasksController(IUserTasksService userTasksService)
        {
            _userTasksService = userTasksService ?? throw new ArgumentNullException(nameof(userTasksService));
        }

        /// <summary>
        /// Create new UserTask.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<UserTaskDto>> CreateTaskGroupAsync([FromBody] UserTaskForCreationDto userTaskForCreation)
        {
            var result = await _userTasksService.CreateUserTaskAsync(userTaskForCreation);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        /// <summary>
        /// Delete existing UserTask by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult> DeleteTaskGroupAsync(int id)
        {
            var result = await _userTasksService.DeleteUserTaskAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return NoContent();
        }

    }
}
