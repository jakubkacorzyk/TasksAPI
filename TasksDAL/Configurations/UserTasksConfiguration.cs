using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TasksEntities;

namespace TasksDAL.Configurations
{
    public class UserTasksConfiguration : IEntityTypeConfiguration<UserTask>
    {
        public void Configure(EntityTypeBuilder<UserTask> builder)
        {
            builder.ToTable("UserTask");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.TaskGroup).WithMany(x => x.UserTasks);
            builder.HasOne(x => x.User).WithMany(x => x.UserTasks);
        }
    }
}
