using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TasksCORE.DTOs;
using TasksCORE.Services.TaskGroups;
using TasksEntities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TasksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CORSPolicy")]
    public class TaskGroupsController : ControllerBase
    {
        private readonly ITaskGroupsService _taskGroupsService;

        public TaskGroupsController(ITaskGroupsService taskGroupsService)
        {
            _taskGroupsService = taskGroupsService ?? throw new ArgumentNullException(nameof(taskGroupsService));
        }

        /// <summary>
        /// Get a list of TaskGroups.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<ActionResult<List<TaskGroupDto>>> GetTaskGroupsAsync()
        {
            var result = await _taskGroupsService.GetTaskGroupsAsync();

            return Ok(result.Data);
        }

        /// <summary>
        /// Get a TaskGroup tag by ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<TaskGroupDto>> GetTaskGroupAsync(int id)
        {
            var result = await _taskGroupsService.GetTaskGroupByIdAsync(id);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Data);
        }

        /// <summary>
        /// Create new TaskGroup.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<TaskGroupDto>> CreateTaskGroupAsync([FromBody] TaskGroupForCreationDto taskGroupForCreation)
        {
            var result = await _taskGroupsService.CreateTaskGroupAsync(taskGroupForCreation);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        /// <summary>
        /// Delete existing TaskGroup by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult> DeleteTaskGroupAsync(int id)
        {
            var result = await _taskGroupsService.DeleteTaskGroupAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return NoContent();
        }

        /// <summary>
        /// Update existing TaskGroup by ID.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<TaskGroupDto>> UpdateTaskGroupAsync(int id, [FromBody] TaskGroupForCreationDto taskGroupForCreation)
        {
            var result = await _taskGroupsService.UpdateTaskGroupAsync(id, taskGroupForCreation);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }
    }
}
