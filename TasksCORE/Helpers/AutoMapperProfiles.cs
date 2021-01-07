using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TasksCORE.DTOs;
using TasksEntities;

namespace TasksCORE.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TaskGroup, TaskGroupDto>();
            CreateMap<TaskGroupForCreationDto, TaskGroup>();
            CreateMap<User, UserDto>();
            CreateMap<UserTask, UserTaskDto>();
            CreateMap<UserTaskForCreationDto, UserTask>();
        }
    }
}
