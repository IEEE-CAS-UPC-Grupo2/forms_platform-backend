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

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<EventsCa> EventsCas { get; set; }
    
    public virtual DbSet<Participant> Participants { get; set; }
    
    public virtual DbSet<Attendance> Attendances { get; set; }
    public virtual DbSet<HistorialRefreshToken> HistorialRefreshTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.IdAdministrator).HasName("PK__administ__03E8E3A1A810EB74");

            entity.ToTable("administrators");

            entity.Property(e => e.IdAdministrator).HasColumnName("id_administrator");
            entity.Property(e => e.EmailAdministrator)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email_administrator");
            entity.Property(e => e.NameAdministrator)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name_administrator");
            entity.Property(e => e.PasswordAdministrator)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_administrator");
        });

        modelBuilder.Entity<EventsCa>(entity =>
        {
            entity.HasKey(e => e.IdEvent).HasName("PK__Events__913E426F86C54842");

            entity.ToTable("events_cas");

            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.AddressEvent)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address_event");
            entity.Property(e => e.EventDateTime)
                .HasColumnType("datetime")
                .HasColumnName("event_date_time");
            entity.Property(e => e.EventDescription)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("event_description");
            entity.Property(e => e.EventDuration).HasColumnName("event_duration");
            entity.Property(e => e.EventTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("event_title");
            entity.Property(e => e.IdAdministrator).HasColumnName("id_administrator");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.InstitutionInCharge)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("institution_in_charge");
            entity.Property(e => e.Modality)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("modality");
            entity.Property(e => e.Speaker)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("speaker");
            entity.Property(e => e.Vacancy).HasColumnName("vacancy");

            entity.HasOne(d => d.IdAdministratorNavigation).WithMany(p => p.EventsCas)
                .HasForeignKey(d => d.IdAdministrator)
                .HasConstraintName("fk_id_administrator");
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.HasKey(e => e.IdParticipant).HasName("PK__Particip__D4A1A8D9A3A3D3A4");

            entity.ToTable("participants");

            entity.Property(e => e.IdParticipant).HasColumnName("id_participant");
            entity.Property(e => e.Dni)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("dni");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.StudyCenter)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("study_center");
            entity.Property(e => e.Career)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("career");
            entity.Property(e => e.IeeeMembershipCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ieee_membership_code");
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.IdAttendance).HasName("PK__Attendan__F3A3A3A3A3A3A3A3");
            
            entity.ToTable("attendances");
            
            entity.Property(e => e.IdAttendance).HasColumnName("id_attendance");
            entity.Property(e => e.IdParticipant).HasColumnName("id_participant");
            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.HasOne(e => e.IdParticipantNavigation).WithMany(e => e.Attendances)
                .HasForeignKey(e => e.IdParticipant)
                .HasConstraintName("fk_id_participant");
            entity.HasOne(e => e.IdEventNavigation).WithMany(e => e.Attendances)
                .HasForeignKey(e => e.IdEvent)
                .HasConstraintName("fk_id_event");
            entity.Property(e => e.Dni).HasColumnName("dni");
            entity.Property(e => e.Email).HasColumnName("email");
        });

        modelBuilder.Entity<HistorialRefreshToken>(entity =>
        {
            entity.HasKey(e => e.IdHistorialToken).HasName("PK__Historia__03DC48A5E085D466");

            entity.ToTable("HistorialRefreshToken");

            entity.Property(e => e.EsActivo).HasComputedColumnSql("(case when [FechaExpiracion]<getdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaExpiracion).HasColumnType("datetime");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Token)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.HistorialRefreshTokens)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Historial__IdUsu__49C3F6B7");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
