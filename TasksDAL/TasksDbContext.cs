using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using TasksDAL.Configurations;
using TasksEntities;

namespace TasksDAL
{
    public class TasksDbContext : DbContext
    {

        public TasksDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }


        public DbSet<User> Users { get; set; }
        public DbSet<TaskGroup> TaskGroups { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new TaskGroupsConfiguration());
            modelBuilder.ApplyConfiguration(new UserTasksConfiguration());
        }
    }
}
