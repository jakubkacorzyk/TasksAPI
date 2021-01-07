using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TasksCORE.DTOs;
using TasksCORE.Helpers;
using TasksDAL;
using TasksEntities;

namespace TasksCORE.Services.TaskGroups
{
    public class TaskGroupsService: ITaskGroupsService
    {
        private readonly TasksDbContext _dbContext;
        private readonly IMapper _mapper;
        public TaskGroupsService(TasksDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TaskGroupDto>> CreateTaskGroupAsync(TaskGroupForCreationDto taskGroupForCreation)
        {
            TaskGroup taskGroup = _mapper.Map<TaskGroup>(taskGroupForCreation);
            try
            {
                await _dbContext.TaskGroups.AddAsync(taskGroup);
                await _dbContext.SaveChangesAsync();

                 
                return new ServiceResponse<TaskGroupDto>(_mapper.Map<TaskGroupDto>(await _dbContext.TaskGroups.FirstOrDefaultAsync(x => x.Id == taskGroup.Id)));
            }
            catch (Exception ex)
            {
                return new ServiceResponse<TaskGroupDto>($"An error occurred when creating the TaskGroup: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<TaskGroupDto>> DeleteTaskGroupAsync(int id)
        {
            var taskGroup = await _dbContext.TaskGroups.FirstOrDefaultAsync(x => x.Id == id);

            if (taskGroup == null)
                return new ServiceResponse<TaskGroupDto>("TaskGroup not found");

            try
            {

                _dbContext.TaskGroups.Remove(taskGroup);

                await _dbContext.SaveChangesAsync();

                return new ServiceResponse<TaskGroupDto>(_mapper.Map<TaskGroupDto>(taskGroup));

            }
            catch (Exception ex)
            {
                return new ServiceResponse<TaskGroupDto>($"An error occurred when deleting the TaskGroup: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<TaskGroupDto>> GetTaskGroupByIdAsync(int id)
        {
            var taskGroup = await _dbContext.TaskGroups
                .Include(x => x.UserTasks).ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (taskGroup == null)
                return new ServiceResponse<TaskGroupDto>($"TaskGroup with id = {id} not found");

            return new ServiceResponse<TaskGroupDto>(_mapper.Map<TaskGroupDto>(taskGroup));
        }

        public async Task<ServiceResponse<List<TaskGroupDto>>> GetTaskGroupsAsync()
        {
            var taskGroupsList = await _dbContext.TaskGroups
                .Include(x => x.UserTasks)
                .ToListAsync();

            return new ServiceResponse<List<TaskGroupDto>>(_mapper.Map<List<TaskGroupDto>>(taskGroupsList));
        }

        public async Task<ServiceResponse<TaskGroupDto>> UpdateTaskGroupAsync(int id, TaskGroupForCreationDto taskGroup)
        {
            var oldTaskGroup = await _dbContext.TaskGroups.FirstOrDefaultAsync(x => x.Id == id);

            if (oldTaskGroup == null)
                return new ServiceResponse<TaskGroupDto>("TaskGroup not found.");

            oldTaskGroup.Name = taskGroup.Name;

            try
            {
                _dbContext.TaskGroups.Update(oldTaskGroup);

                await _dbContext.SaveChangesAsync();

                return new ServiceResponse<TaskGroupDto>(_mapper.Map<TaskGroupDto>(oldTaskGroup));
            }
            catch (Exception ex)
            {
                return new ServiceResponse<TaskGroupDto>($"An error occurred when updating the TaskGroup: {ex.Message}");
            }
        }
    }
}
