using System;
using System.Collections.Generic;
using GestorPersonalTareas.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorPersonalTareas.Data;

public partial class TaskDbContext : DbContext
{
    public TaskDbContext()
    {
    }

    public TaskDbContext(DbContextOptions<TaskDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TaskItem> Tasks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.ToTable("Tasks");

            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

    }
    }
