using System;
using System.Collections.Generic;
using System.Text;

namespace TasksCORE.DTOs
{
    public class TaskGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserTaskDto> UserTasks { get; set; }
    }
}
