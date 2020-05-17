using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cw5.GeneratedModels
{
    public partial class s18310Context : DbContext
    {
        public s18310Context()
        {
        }

        public s18310Context(DbContextOptions<s18310Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Dept> Dept { get; set; }
        public virtual DbSet<Emp> Emp { get; set; }
        public virtual DbSet<Enrollment> Enrollment { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Salgrade> Salgrade { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentRoles> StudentRoles { get; set; }
        public virtual DbSet<Studies> Studies { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<TaskType> TaskType { get; set; }
        public virtual DbSet<TeamMember> TeamMember { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s18310;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dept>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DEPT");

                entity.Property(e => e.Deptno).HasColumnName("DEPTNO");

                entity.Property(e => e.Dname)
                    .HasColumnName("DNAME")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Loc)
                    .HasColumnName("LOC")
                    .HasMaxLength(13)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Emp>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("EMP");

                entity.Property(e => e.Comm).HasColumnName("COMM");

                entity.Property(e => e.Deptno).HasColumnName("DEPTNO");

                entity.Property(e => e.Empno).HasColumnName("EMPNO");

                entity.Property(e => e.Ename)
                    .HasColumnName("ENAME")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Hiredate)
                    .HasColumnName("HIREDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Job)
                    .HasColumnName("JOB")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Mgr).HasColumnName("MGR");

                entity.Property(e => e.Sal).HasColumnName("SAL");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.IdEnrollment)
                    .HasName("Enrollment_pk");

                entity.Property(e => e.IdEnrollment).ValueGeneratedNever();

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.IdStudyNavigation)
                    .WithMany(p => p.Enrollment)
                    .HasForeignKey(d => d.IdStudy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Enrollment_Studies");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.IdProject)
                    .HasName("Project_pk");

                entity.Property(e => e.Deadline).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__Roles__8AFACE1AC42509B8");

                entity.Property(e => e.RoleId).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Salgrade>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SALGRADE");

                entity.Property(e => e.Grade).HasColumnName("GRADE");

                entity.Property(e => e.Hisal).HasColumnName("HISAL");

                entity.Property(e => e.Losal).HasColumnName("LOSAL");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IndexNumber)
                    .HasName("Student_pk");

                entity.Property(e => e.IndexNumber).HasMaxLength(100);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RefreshToken).HasMaxLength(1000);

                entity.Property(e => e.Salt)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TokenExpiration).HasMaxLength(1000);

                entity.HasOne(d => d.IdEnrollmentNavigation)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.IdEnrollment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Student_Enrollment");
            });

            modelBuilder.Entity<StudentRoles>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.IndexNumber).HasMaxLength(100);

                entity.Property(e => e.RoleId).HasMaxLength(100);

                entity.HasOne(d => d.IndexNumberNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IndexNumber)
                    .HasConstraintName("FK__StudentRo__Index__412EB0B6");

                entity.HasOne(d => d.Role)
                    .WithMany()
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__StudentRo__RoleI__4222D4EF");
            });

            modelBuilder.Entity<Studies>(entity =>
            {
                entity.HasKey(e => e.IdStudy)
                    .HasName("Studies_pk");

                entity.Property(e => e.IdStudy).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.IdTask)
                    .HasName("Task_pk");

                entity.Property(e => e.Deadline).HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdAssignedToNavigation)
                    .WithMany(p => p.TaskIdAssignedToNavigation)
                    .HasForeignKey(d => d.IdAssignedTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_TeamMember2");

                entity.HasOne(d => d.IdCreatorNavigation)
                    .WithMany(p => p.TaskIdCreatorNavigation)
                    .HasForeignKey(d => d.IdCreator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_TeamMember1");

                entity.HasOne(d => d.IdProjectNavigation)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.IdProject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_Project");

                entity.HasOne(d => d.IdTaskTypeNavigation)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.IdTaskType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_TaskType");
            });

            modelBuilder.Entity<TaskType>(entity =>
            {
                entity.HasKey(e => e.IdTaskType)
                    .HasName("TaskType_pk");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TeamMember>(entity =>
            {
                entity.HasKey(e => e.IdTeamMember)
                    .HasName("TeamMember_pk");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
