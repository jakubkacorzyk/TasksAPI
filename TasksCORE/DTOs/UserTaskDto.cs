using System;
using System.Collections.Generic;
using System.Text;
using TasksEntities;

namespace TasksCORE.DTOs
{
    public class UserTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public UserDto User { get; set; }
        public Status Status { get; set; }
    }
}
