using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TasksEntities;

namespace TasksDAL.Configurations
{
    public class TaskGroupsConfiguration : IEntityTypeConfiguration<TaskGroup>
    {
        public void Configure(EntityTypeBuilder<TaskGroup> builder)
        {
            builder.ToTable("TaskGroup");
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.UserTasks).WithOne(x => x.TaskGroup).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
