using System;
using System.Collections.Generic;
using System.Text;

namespace TasksEntities
{
    public class TaskGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserTask> UserTasks { get; set; }
    }
}
