using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AspNetCoreWebApi.Sql.Entities
{
    public partial class TurboBootcampDbContext : DbContext
    {
        public TurboBootcampDbContext(DbContextOptions<TurboBootcampDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<School> Schools { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<School>(entity =>
            {
                entity.ToTable("schools");

                entity.Property(e => e.SchoolId)
                    .HasColumnName("school_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.EstablishedAt).HasColumnName("established_at");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("students");

                entity.HasIndex(e => e.SchoolId, "idx_students_school_id");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.FullName).HasColumnName("full_name");

                entity.Property(e => e.JoinedAt).HasColumnName("joined_at");

                entity.Property(e => e.Nickname).HasColumnName("nickname");

                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("students__school_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
