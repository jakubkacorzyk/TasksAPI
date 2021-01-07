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

namespace TasksCORE.Services.UserTasks
{
    public class UserTasksService : IUserTasksService
    {
        private readonly TasksDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserTasksService(TasksDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<UserTaskDto>> CreateUserTaskAsync(UserTaskForCreationDto userTaskForCreation)
        {
            UserTask userTask = _mapper.Map<UserTask>(userTaskForCreation);

            try
            {
                await _dbContext.UserTasks.AddAsync(userTask);
                await _dbContext.SaveChangesAsync();


                return new ServiceResponse<UserTaskDto>(_mapper.Map<UserTaskDto>(await _dbContext.UserTasks.FirstOrDefaultAsync(x => x.Id == userTask.Id)));
            }
            catch (Exception ex)
            {
                return new ServiceResponse<UserTaskDto>($"An error occurred when creating the UserTask: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<UserTaskDto>> DeleteUserTaskAsync(int id)
        {
            var userTask = await _dbContext.UserTasks.FirstOrDefaultAsync(x => x.Id == id);

            if (userTask == null)
                return new ServiceResponse<UserTaskDto>("UserTask not found");

            try
            {

                _dbContext.UserTasks.Remove(userTask);

                await _dbContext.SaveChangesAsync();

                return new ServiceResponse<UserTaskDto>(_mapper.Map<UserTaskDto>(userTask));

            }
            catch (Exception ex)
            {
                return new ServiceResponse<UserTaskDto>($"An error occurred when deleting the UserTask: {ex.Message}");
            }
        }
    }
}
