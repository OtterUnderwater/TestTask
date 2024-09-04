using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Models;

public partial class PmsContext : DbContext
{
    public PmsContext()
    {
    }

    public PmsContext(DbContextOptions<PmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<WorkTask> WorkTasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ngknn.ru;Port=5442;Database=pms;Username=33P;Password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employees_pk");

            entity.ToTable("employees");

            entity.HasIndex(e => e.IdRole, "employees_un").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.Surname)
                .HasColumnType("character varying")
                .HasColumnName("surname");

            entity.HasOne(d => d.IdRoleNavigation).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employees_fk");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pk");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Role1)
                .HasColumnType("character varying")
                .HasColumnName("role");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("statuses_pk");

            entity.ToTable("statuses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status1)
                .HasColumnType("character varying")
                .HasColumnName("status");
        });

        modelBuilder.Entity<WorkTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("work_tasks_pk");

            entity.ToTable("work_tasks");

            entity.HasIndex(e => e.IdStatus, "work_tasks_un").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.IdEmployee).HasColumnName("id_employee");
            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.WorkTasks)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("work_tasks_fk");

            entity.HasOne(d => d.IdStatusNavigation).WithOne(p => p.WorkTask)
                .HasForeignKey<WorkTask>(d => d.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("work_tasks_fk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
