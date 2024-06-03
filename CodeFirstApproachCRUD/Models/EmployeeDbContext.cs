using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstApproachCRUD.Models
{
    public class EmployeeDbContext  : IdentityDbContext<IdentityUser>
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
                
        }
        public DbSet<Employee> Employees { get; set;}
        public DbSet<Department> Departments { get; set; }
       
        /// <summary>
        /// Configure one-to-one relationship using Fluent API
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DeptId)
                .OnDelete(DeleteBehavior.Cascade); 
        }       
    }
}
