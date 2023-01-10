using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PschoolAPIback.Configuration;
using PschoolAPIback.Models;

namespace PschoolAPIback.DbPschoolContext;

public class PschoolContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Student> Students { get; set; }

    public PschoolContext(DbContextOptions<PschoolContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<PschoolContext>
    {
        public PschoolContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PschoolContext>();
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=master;User=SA;Password=reallyStrongPwd123;TrustServerCertificate=true");
    
            return new PschoolContext(optionsBuilder.Options);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }
    
}