using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Enums;

namespace PIMTool.Database
{
    public class PimContext : DbContext
    {
        // TODO: Define your models

        public PimContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Group> Groups { get; set; } 
        public DbSet<ProjectEmployee> ProjectEmployees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ProjectModelCreating(modelBuilder);
            EmployeeModelCreating(modelBuilder);
            GroupModelCreating(modelBuilder);
            ProjectEmployeeModelCreating(modelBuilder);
        }

        private void ProjectModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .ToTable("PROJECT")
                .Property(p => p.Id)
                .HasColumnType("numeric(19,0)")
                .HasColumnName("ID")
                .HasPrecision(19, 0)
                .IsRequired()
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Project>()
                .Property(p => p.GroupId)
                .HasColumnType("numeric")
                .HasColumnName("GROUP_ID")
                .HasPrecision(19,0)
                .IsRequired();
            modelBuilder.Entity<Project>()
                .Property(p => p.ProjectNumber)
                .HasColumnType("numeric")
                .HasColumnName("PROJECT_NUMBER")
                .HasPrecision(4, 0)
                .IsRequired();
            modelBuilder.Entity<Project>()
                .HasIndex(p => p.ProjectNumber)
                .IsUnique();
            modelBuilder.Entity<Project>()
                .Property(p => p.Name)
                .HasColumnType("varchar")
                .HasColumnName("NAME")
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Project>()
                .Property(p => p.Customer)
                .HasColumnType("varchar")
                .HasColumnName("CUSTOMER")
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Project>()
                .Property(p => p.Status)
                .HasColumnType("char")
                .HasColumnName("STATUS")
                .HasMaxLength(3)
                .HasConversion(v => v.ToString(), v => (ProjectStatus.EnumStatus)Enum.Parse(typeof(ProjectStatus.EnumStatus), v))
                .IsRequired();
            modelBuilder.Entity<Project>()
                .HasCheckConstraint("CK_Project_Status", "[Status] = 'NEW' OR [STATUS] = 'PLA' OR [STATUS] = 'INP' OR [STATUS] = 'FIN'");
            modelBuilder.Entity<Project>()
                .Property(p => p.StartDate)
                .HasColumnType("date")
                .HasColumnName("START_DATE")
                .IsRequired();
            modelBuilder.Entity<Project>()
                .Property(p => p.EndDate)
                .HasColumnType("date")
                .HasColumnName("END_DATE");
            modelBuilder.Entity<Project>()
                .Property(p => p.Version)
                .HasColumnName("VERSION")
                .IsConcurrencyToken()
                .IsRequired();
        }

        private void EmployeeModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .ToTable("EMPLOYEE")
                .Property(e => e.Id)
                .HasColumnType("numeric")
                .HasColumnName("ID")
                .HasPrecision(19, 0)
                .IsRequired()
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Employee>()
                .Property(e => e.Visa)
                .HasColumnType("char")
                .HasColumnName("VISA")
                .HasMaxLength(3)
                .IsRequired();
            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .HasColumnType("varchar")
                .HasColumnName("FIRST_NAME")
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName)
                .HasColumnType("varchar")
                .HasColumnName("LAST_NAME")
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Employee>()
                .Property(e => e.BirthDate)
                .HasColumnType("date")
                .HasColumnName("BIRTH_DATE")
                .IsRequired();
            modelBuilder.Entity<Employee>() 
                .Property(e => e.Version)
                .HasColumnName("VERSION")
                .IsConcurrencyToken()
                .IsRequired();
        }

        private void GroupModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Group>()
                .ToTable("GROUP")
                .Property(g => g.Id)
                .HasColumnType("numeric")
                .HasColumnName("ID")
                .HasPrecision(19, 0)
                .IsRequired()
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Group>()
                .Property(g => g.Group_Leader_Id)
                .HasColumnType("numeric")
                .HasColumnName("GROUP_LEADER_ID")
                .HasPrecision(19, 0)
                .IsRequired();

            modelBuilder.Entity<Group>()
                .HasOne(g => g.Employee)
                .WithOne(e => e.Group)
                .HasForeignKey<Group>(g => g.Group_Leader_Id);

            modelBuilder.Entity<Group>()
                .Property(g => g.Version)
                .HasColumnName("VERSION")
                .IsConcurrencyToken()
                .IsRequired();
        }

        private void ProjectEmployeeModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<ProjectEmployee>()
                .ToTable("PROJECT_EMPLOYEE")
                .HasKey(pe => new { pe.ProjectId, pe.EmployeeId });

            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(p => p.Project)
                .WithMany(pe => pe.ProjectEmployees)
                .HasForeignKey(p => p.ProjectId)
                .HasConstraintName("PROJECT_ID")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(e => e.Employee)
                .WithMany(pe => pe.ProjectEmployees)
                .HasForeignKey(e => e.EmployeeId)
                .HasConstraintName("EMPLOYEE_ID")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProjectEmployee>()
                .Ignore(pe => pe.Version)
                .Ignore(pe => pe.Id);

        }
    }
}