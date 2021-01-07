using System;
using System.Collections.Generic;
using System.Text;
using TasksEntities;

namespace TasksCORE.DTOs
{
    public class UserTaskForCreationDto
    {
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public Status Status { get; set; }
        public int UserId { get; set; }
        public int TaskGroupId { get; set; }
    }
}
