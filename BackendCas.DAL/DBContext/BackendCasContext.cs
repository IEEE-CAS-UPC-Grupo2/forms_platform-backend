using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BackendCas.MODEL;

namespace BackendCas.DAL.DBContext;

public partial class BackendCasContext : DbContext
{
    public BackendCasContext()
    {
    }

    public BackendCasContext(DbContextOptions<BackendCasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdministratorsCa> AdministratorsCas { get; set; }

    public virtual DbSet<EventsCa> EventsCas { get; set; }

    public virtual DbSet<Participation> Participations { get; set; }

    public virtual DbSet<TokenLog> TokenLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdministratorsCa>(entity =>
        {
            entity.HasKey(e => e.IdAdministrator).HasName("PK__Administ__27833EB96E1ACFC4");

            entity.ToTable("AdministratorsCa");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(32);
        });

        modelBuilder.Entity<EventsCa>(entity =>
        {
            entity.HasKey(e => e.IdEvent).HasName("PK__EventsCa__E0B2AF393AF45615");

            entity.ToTable("EventsCa");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EventDateAndTime)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EventDescription).HasColumnType("text");
            entity.Property(e => e.EventTitle)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.InstitutionInCharge)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Modality)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Speaker)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAdministratorNavigation).WithMany(p => p.EventsCas)
                .HasForeignKey(d => d.IdAdministrator)
                .HasConstraintName("FK__EventsCa__IdAdmi__3C69FB99");
        });

        modelBuilder.Entity<Participation>(entity =>
        {
            entity.HasKey(e => e.IdParticipation).HasName("PK__Particip__C632615858C1D98D");

            entity.Property(e => e.Career)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("DNI");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IeeemembershipCode)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("IEEEMembershipCode");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StudyCenter)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEventNavigation).WithMany(p => p.Participations)
                .HasForeignKey(d => d.IdEvent)
                .HasConstraintName("FK__Participa__IdEve__3F466844");
        });

        modelBuilder.Entity<TokenLog>(entity =>
        {
            entity.HasKey(e => e.IdTokenLog).HasName("PK__TokenLog__96BC59C9947633A4");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ExpiredAt).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasComputedColumnSql("(case when [ExpiredAt]<getdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", false);
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Token)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAdministratorNavigation).WithMany(p => p.TokenLogs)
                .HasForeignKey(d => d.IdAdministrator)
                .HasConstraintName("FK__TokenLogs__IdAdm__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}