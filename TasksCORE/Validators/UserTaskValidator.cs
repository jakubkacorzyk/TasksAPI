using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TasksCORE.DTOs;
using TasksDAL;

namespace TasksCORE.Validators
{
    public class UserTaskValidator : AbstractValidator<UserTaskForCreationDto>
	{
		private readonly TasksDbContext _dbContext;
		public UserTaskValidator(TasksDbContext tasksDbContext)
		{
			_dbContext = tasksDbContext;
			Rules();
		}

        public void Rules()
        {
            RuleFor(x => x.Name)
                            .NotNull()
                            .Length(2, 50);

            RuleFor(x => x.UserId)
                .NotNull()
                .MustAsync(async (id, cancellation) =>
                {
                    return await UserExists(id);
                })
                .WithMessage("User must exisit!");

            RuleFor(x => x.TaskGroupId)
                .MustAsync(async (id, cancellation) =>
                {
                    return await TaskGroupExists(id);
                })
                .WithMessage("Task group must exisit!");
        }

        private async Task<bool> UserExists(int userId)
        {
            if (await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId) == null)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> TaskGroupExists(int taskGroupId)
        {
            if (await _dbContext.TaskGroups.FirstOrDefaultAsync(x => x.Id == taskGroupId) == null)
            {
                return false;
            }

            return true;
        }
    }
}
