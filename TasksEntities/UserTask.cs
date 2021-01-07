using System;

namespace TasksEntities
{
    public enum Status
    {
        New,
        InProgress,
        Completed
    }
    public class UserTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int TaskGroupId { get; set; }
        public TaskGroup TaskGroup { get; set; }
        public Status Status { get; set; }
    }
}
