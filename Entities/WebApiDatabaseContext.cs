﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace timeTrackingSystemBackend.Entities
{
    public partial class WebApiDatabaseContext : DbContext
    {
        public WebApiDatabaseContext()
        {
        }

        public WebApiDatabaseContext(DbContextOptions<WebApiDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tunnit> Tunnit { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-35CADGH\\SQLEMA;Database=WebApiDatabase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tunnit>(entity =>
            {
                entity.ToTable("tunnit");

                entity.Property(e => e.TunnitId).HasColumnName("TunnitID");

                entity.Property(e => e.LuokkahuoneId)
                    .HasColumnName("LuokkahuoneID")
                    .HasMaxLength(4);

                entity.Property(e => e.OppilasId)
                    .HasColumnName("OppilasID")
                    .HasMaxLength(128);

                entity.Property(e => e.Sisaan).HasColumnType("datetime");

                entity.Property(e => e.Ulos).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}