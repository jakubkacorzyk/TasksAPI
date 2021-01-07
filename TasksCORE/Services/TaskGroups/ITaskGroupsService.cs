using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TasksCORE.DTOs;
using TasksCORE.Helpers;
using TasksEntities;

namespace TasksCORE.Services.TaskGroups
{
    public interface ITaskGroupsService
    {
        Task<ServiceResponse<List<TaskGroupDto>>> GetTaskGroupsAsync();
        Task<ServiceResponse<TaskGroupDto>> GetTaskGroupByIdAsync(int id);
        Task<ServiceResponse<TaskGroupDto>> CreateTaskGroupAsync(TaskGroupForCreationDto taskGroup);
        Task<ServiceResponse<TaskGroupDto>> UpdateTaskGroupAsync(int id, TaskGroupForCreationDto taskGroup);
        Task<ServiceResponse<TaskGroupDto>> DeleteTaskGroupAsync(int id);
    }
}
