﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PIMTool.Database;

#nullable disable

namespace PIMTool.Migrations
{
    [DbContext(typeof(PimContext))]
    partial class PimContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PIMTool.Core.Domain.Entities.Employee", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(19)
                        .HasColumnType("numeric(19,0)")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"), 1L, 1);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("BIRTH_DATE");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("FIRST_NAME");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("LAST_NAME");

                    b.Property<Guid>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("VERSION");

                    b.Property<string>("Visa")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("char(3)")
                        .HasColumnName("VISA");

                    b.HasKey("Id");

                    b.ToTable("EMPLOYEE", (string)null);
                });

            modelBuilder.Entity("PIMTool.Core.Domain.Entities.Group", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(19)
                        .HasColumnType("numeric(19,0)")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"), 1L, 1);

                    b.Property<decimal>("Group_Leader_Id")
                        .HasPrecision(19)
                        .HasColumnType("numeric(19,0)")
                        .HasColumnName("GROUP_LEADER_ID");

                    b.Property<Guid>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("VERSION");

                    b.HasKey("Id");

                    b.HasIndex("Group_Leader_Id")
                        .IsUnique();

                    b.ToTable("GROUP", (string)null);
                });

            modelBuilder.Entity("PIMTool.Core.Domain.Entities.Project", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(19)
                        .HasColumnType("numeric(19,0)")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"), 1L, 1);

                    b.Property<string>("Customer")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("CUSTOMER");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("date")
                        .HasColumnName("END_DATE");

                    b.Property<decimal>("GroupId")
                        .HasPrecision(19)
                        .HasColumnType("numeric(19,0)")
                        .HasColumnName("GROUP_ID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("NAME");

                    b.Property<decimal>("ProjectNumber")
                        .HasPrecision(4)
                        .HasColumnType("numeric(4,0)")
                        .HasColumnName("PROJECT_NUMBER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date")
                        .HasColumnName("START_DATE");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("char(3)")
                        .HasColumnName("STATUS");

                    b.Property<Guid>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("VERSION");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("ProjectNumber")
                        .IsUnique();

                    b.ToTable("PROJECT", (string)null);

                    b.HasCheckConstraint("CK_Project_Status", "[Status] = 'NEW' OR [STATUS] = 'PLA' OR [STATUS] = 'INP' OR [STATUS] = 'FIN'");
                });

            modelBuilder.Entity("PIMTool.Core.Domain.Entities.ProjectEmployee", b =>
                {
                    b.Property<decimal>("ProjectId")
                        .HasColumnType("numeric(19,0)");

                    b.Property<decimal>("EmployeeId")
                        .HasColumnType("numeric(19,0)");

                    b.HasKey("ProjectId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("PROJECT_EMPLOYEE", (string)null);
                });

            modelBuilder.Entity("PIMTool.Core.Domain.Entities.Group", b =>
                {
                    b.HasOne("PIMTool.Core.Domain.Entities.Employee", "Employee")
                        .WithOne("Group")
                        .HasForeignKey("PIMTool.Core.Domain.Entities.Group", "Group_Leader_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("PIMTool.Core.Domain.Entities.Project", b =>
                {
                    b.HasOne("PIMTool.Core.Domain.Entities.Group", "Group")
                        .WithMany("Projects")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("PIMTool.Core.Domain.Entities.ProjectEmployee", b =>
                {
                    b.HasOne("PIMTool.Core.Domain.Entities.Employee", "Employee")
                        .WithMany("ProjectEmployees")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("EMPLOYEE_ID");

                    b.HasOne("PIMTool.Core.Domain.Entities.Project", "Project")
                        .WithMany("ProjectEmployees")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("PROJECT_ID");

                    b.Navigation("Employee");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("PIMTool.Core.Domain.Entities.Employee", b =>
                {
                    b.Navigation("Group");

                    b.Navigation("ProjectEmployees");
                });

            modelBuilder.Entity("PIMTool.Core.Domain.Entities.Group", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("PIMTool.Core.Domain.Entities.Project", b =>
                {
                    b.Navigation("ProjectEmployees");
                });
#pragma warning restore 612, 618
        }
    }
}