using Microsoft.EntityFrameworkCore;
using FindJobAPI.Model.Domain;
using Microsoft.AspNetCore.Identity;


namespace FindJobAPI.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } // constructor

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        public DbSet<account> accounts { get; set; }
        public DbSet<job> jobs { get; set; }
        public DbSet<admin> admins { get; set; }
        public DbSet<employer> employers { get; set; }
        public DbSet<seeker> seekers { get; set; }
        public DbSet<industry> industry { get; set; }
        public DbSet<job_detail> job_detail { get; set; }
        public DbSet<job_industry> job_industry { get; set;}
        public DbSet<recruitment> recruitment { get; set;}
        public DbSet<role> role { get; set; }
        public DbSet<type> type { get; set; }
    }
}
