using System.Collections.Generic;

namespace TasksEntities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<UserTask> UserTasks { get; set; }
    }
}