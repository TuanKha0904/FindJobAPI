﻿using Microsoft.EntityFrameworkCore;
using FindJobAPI.Model.Domain;
using Microsoft.AspNetCore.Identity;


namespace FindJobAPI.Data
{
    public class AppDbContext :DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } // constructor
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<account>()
                .HasOne(a => a.role)
                .WithMany(a => a.Account)
                .HasForeignKey(a => a.role_id);
            modelBuilder.Entity<seeker>()
              .HasOne(s => s.account)
              .WithMany(a => a.seekers)
              .HasForeignKey(s => s.email )
              .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<employer>()
                .HasOne(s => s.account)
                .WithMany(s => s.employers)
                .HasForeignKey(s => s.email);
            modelBuilder.Entity<recruitment>()
                .HasOne(r => r.seeker)
                .WithMany(r => r.recruitments)
                .HasForeignKey(r => r.seeker_id);
            modelBuilder.Entity<recruitment>()
               .HasOne(r => r.job)
               .WithMany(r => r.recruitment)
               .HasForeignKey(r => r.job_id);
            modelBuilder.Entity<job>()
                .HasOne(j => j.employer)
                .WithMany(j => j.jobs)
                .HasForeignKey(j => j.employer_id);
            modelBuilder.Entity<job>()
               .HasOne(j => j.type)
               .WithMany(j => j.jobs)
               .HasForeignKey(j => j.type_id);
            modelBuilder.Entity<job_detail>()
              .HasOne(j => j.job)
              .WithMany(j => j.job_detail)
              .HasForeignKey(j => j.job_id);
            modelBuilder.Entity<job_detail>()
              .HasOne(j => j.industry)
              .WithMany(j => j.job_detail)
              .HasForeignKey(j => j.industry_id);
            modelBuilder.Entity<job_industry>()
             .HasOne(j => j.industry)
             .WithMany(j => j.job_industry)
             .HasForeignKey(j => j.industry_id);
        }

        public DbSet<account> Account { get; set; }
        public DbSet<job> Job { get; set; }
        public DbSet<admin> Admin { get; set; }
        public DbSet<employer> Employer { get; set; }
        public DbSet<seeker> Seeker { get; set; }
        public DbSet<industry> Industry { get; set; }
        public DbSet<job_detail> Job_Detail { get; set; }
        public DbSet<job_industry> Job_Industry { get; set;}
        public DbSet<recruitment> Recruitment { get; set;}
        public DbSet<role> Role { get; set; }
        public DbSet<type> Type { get; set; }
    }
}
