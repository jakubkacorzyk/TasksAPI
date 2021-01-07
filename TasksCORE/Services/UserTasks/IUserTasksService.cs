using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TasksCORE.DTOs;
using TasksCORE.Helpers;
using TasksEntities;

namespace TasksCORE.Services.UserTasks
{
    public interface IUserTasksService
    {
        Task<ServiceResponse<UserTaskDto>> CreateUserTaskAsync(UserTaskForCreationDto userTaskForCreation);
        Task<ServiceResponse<UserTaskDto>> DeleteUserTaskAsync(int id);
    }
}
