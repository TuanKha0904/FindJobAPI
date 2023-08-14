﻿// <auto-generated />
using System;
using FindJobAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FindJobAPI.Data.FindJobAPI_DB
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FindJobAPI.Model.Domain.account", b =>
                {
                    b.Property<int>("account_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("account_id"), 1L, 1);

                    b.Property<DateTime>("date_create")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("account_id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.admin", b =>
                {
                    b.Property<int>("admin_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("admin_id"), 1L, 1);

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("admin_id");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.employer", b =>
                {
                    b.Property<int>("account_id")
                        .HasColumnType("int");

                    b.Property<string>("contact_phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("employer_about")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("employer_address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("employer_image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("employer_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("employer_website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("account_id");

                    b.ToTable("Employer");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.industry", b =>
                {
                    b.Property<int>("industry_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("industry_id"), 1L, 1);

                    b.Property<string>("industry_name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("industry_id");

                    b.ToTable("Industry");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.job", b =>
                {
                    b.Property<int>("job_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("job_id"), 1L, 1);

                    b.Property<int>("account_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("deadline")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("posted_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("type_id")
                        .HasColumnType("int");

                    b.HasKey("job_id");

                    b.HasIndex("account_id");

                    b.HasIndex("type_id");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.job_detail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("industry_id")
                        .HasColumnType("int");

                    b.Property<string>("job_description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("job_id")
                        .HasColumnType("int");

                    b.Property<string>("job_title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("maximum_salary")
                        .HasColumnType("real");

                    b.Property<float>("minimum_salary")
                        .HasColumnType("real");

                    b.Property<string>("requirement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("industry_id");

                    b.HasIndex("job_id");

                    b.ToTable("Job_Detail");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.job_industry", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("industry_id")
                        .HasColumnType("int");

                    b.Property<string>("job")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("industry_id");

                    b.ToTable("Job_Industry");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.recruitment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("account_id")
                        .HasColumnType("int");

                    b.Property<int>("job_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("registation_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("seeker_desire")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("account_id");

                    b.HasIndex("job_id");

                    b.ToTable("Recruitment");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.seeker", b =>
                {
                    b.Property<int>("account_id")
                        .HasColumnType("int");

                    b.Property<string>("academic_level")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("first_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("last_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone_number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("seeker_image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("skills")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("url_cv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("website_seeker")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("account_id");

                    b.ToTable("Seeker");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.type", b =>
                {
                    b.Property<int>("type_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("type_id"), 1L, 1);

                    b.Property<string>("type_name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("type_id");

                    b.ToTable("Type");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.employer", b =>
                {
                    b.HasOne("FindJobAPI.Model.Domain.account", "account")
                        .WithMany("employers")
                        .HasForeignKey("account_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("account");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.job", b =>
                {
                    b.HasOne("FindJobAPI.Model.Domain.employer", "employer")
                        .WithMany("jobs")
                        .HasForeignKey("account_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FindJobAPI.Model.Domain.type", "type")
                        .WithMany("jobs")
                        .HasForeignKey("type_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("employer");

                    b.Navigation("type");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.job_detail", b =>
                {
                    b.HasOne("FindJobAPI.Model.Domain.industry", "industry")
                        .WithMany("job_detail")
                        .HasForeignKey("industry_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FindJobAPI.Model.Domain.job", "job")
                        .WithMany("job_detail")
                        .HasForeignKey("job_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("industry");

                    b.Navigation("job");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.job_industry", b =>
                {
                    b.HasOne("FindJobAPI.Model.Domain.industry", "industry")
                        .WithMany("job_industry")
                        .HasForeignKey("industry_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("industry");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.recruitment", b =>
                {
                    b.HasOne("FindJobAPI.Model.Domain.seeker", "seeker")
                        .WithMany("recruitments")
                        .HasForeignKey("account_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FindJobAPI.Model.Domain.job", "job")
                        .WithMany("recruitment")
                        .HasForeignKey("job_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("job");

                    b.Navigation("seeker");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.seeker", b =>
                {
                    b.HasOne("FindJobAPI.Model.Domain.account", "account")
                        .WithMany("seekers")
                        .HasForeignKey("account_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("account");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.account", b =>
                {
                    b.Navigation("employers");

                    b.Navigation("seekers");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.employer", b =>
                {
                    b.Navigation("jobs");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.industry", b =>
                {
                    b.Navigation("job_detail");

                    b.Navigation("job_industry");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.job", b =>
                {
                    b.Navigation("job_detail");

                    b.Navigation("recruitment");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.seeker", b =>
                {
                    b.Navigation("recruitments");
                });

            modelBuilder.Entity("FindJobAPI.Model.Domain.type", b =>
                {
                    b.Navigation("jobs");
                });
#pragma warning restore 612, 618
        }
    }
}
