﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(VacationManagerDbContext))]
    [Migration("20220802172938_MySqlMigration")]
    partial class MySqlMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.15");

            modelBuilder.Entity("Models.Project", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Models.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Name = "CEO"
                        },
                        new
                        {
                            Id = "2",
                            Name = "Developer"
                        },
                        new
                        {
                            Id = "3",
                            Name = "Team Lead"
                        },
                        new
                        {
                            Id = "4",
                            Name = "Unassigned"
                        });
                });

            modelBuilder.Entity("Models.Team", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("ProjectId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("TeamLeaderId")
                        .HasColumnType("text");

                    b.Property<string>("TeamLeaderId1")
                        .HasColumnType("varchar(767)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TeamLeaderId1");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("TeamId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("TeamId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Models.Vacation", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(767)");

                    b.Property<string>("ApplicantId")
                        .HasColumnType("varchar(767)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("FilePath")
                        .HasColumnType("text");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsHalfDay")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime");

                    b.Property<int>("VacationType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.ToTable("Vacations");
                });

            modelBuilder.Entity("Models.Team", b =>
                {
                    b.HasOne("Models.Project", "Project")
                        .WithMany("Teams")
                        .HasForeignKey("ProjectId");

                    b.HasOne("Models.User", "TeamLeader")
                        .WithMany()
                        .HasForeignKey("TeamLeaderId1");

                    b.Navigation("Project");

                    b.Navigation("TeamLeader");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.HasOne("Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("Models.Team", "Team")
                        .WithMany("Developers")
                        .HasForeignKey("TeamId");

                    b.Navigation("Role");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Models.Vacation", b =>
                {
                    b.HasOne("Models.User", "Applicant")
                        .WithMany()
                        .HasForeignKey("ApplicantId");

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("Models.Project", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Models.Team", b =>
                {
                    b.Navigation("Developers");
                });
#pragma warning restore 612, 618
        }
    }
}
