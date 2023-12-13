using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Tasks.API.Domain;

namespace Tasks.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.Property(e => e.Title).IsRequired().HasColumnType("nvarchar(50)").HasColumnName("TITLE");
                entity.Property(e => e.Description).IsRequired().HasColumnType("nvarchar(500)").HasColumnName("DESCRIPTION");
                entity.Property(e => e.DueDate).IsRequired().HasColumnType("datetime").HasColumnName("DUE_DATE");
                entity.Property(e => e.Status).IsRequired().HasConversion<string>().HasColumnType("nvarchar(15)").HasColumnName("STATUS");

                entity.HasIndex(t => new { t.DueDate, t.Status });
                entity.ToTable("Tasks");

            });
        }

        public DbSet<TaskItem> Tasks { get; init; }

    }
}
