using Microsoft.EntityFrameworkCore;
using Entities = Todo.Api.Domain.Entities;

namespace Todo.Api.Persistance
{
    public class TodoApiDbContext : DbContext
    {
        public DbSet<Entities.TaskList> TaskLists { get; set; }

        public DbSet<Entities.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoApiDbContext).Assembly); // it will search all configurations in an assembly and apply them
        }

        public TodoApiDbContext(DbContextOptions<TodoApiDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}