using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace travel_agency_system.Models;

public partial class TravelAgencySystemContext : DbContext
{
    public TravelAgencySystemContext()
    {
    }

    public TravelAgencySystemContext(DbContextOptions<TravelAgencySystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activation> Activations { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingConfirmation> BookingConfirmations { get; set; }

    public virtual DbSet<EventApi> EventApis { get; set; }

    public virtual DbSet<HotelApi> HotelApis { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationService> NotificationServices { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<ResetPassword> ResetPasswords { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=travel-agency-system;User Id=sa;Password=!Mohab#123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activation>(entity =>
        {
            entity.HasKey(e => e.ActivationId).HasName("PK__Activati__C199CCEF8C4E4888");

            entity.ToTable("Activation");

            entity.Property(e => e.ActivationId).HasColumnName("activationID");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.NotificationId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("notificationID");

            entity.HasOne(d => d.Notification).WithMany(p => p.Activations)
                .HasForeignKey(d => d.NotificationId)
                .HasConstraintName("FK__Activatio__notif__47DBAE45");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__C6D03BED5FA0CA1B");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("bookingID");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Details).HasColumnName("details");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userID");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Booking__userID__3B75D760");
        });

        modelBuilder.Entity<BookingConfirmation>(entity =>
        {
            entity.HasKey(e => e.ConfirmationId).HasName("PK__Booking___C914AC607B2F4853");

            entity.ToTable("Booking_Confirmation");

            entity.Property(e => e.ConfirmationId).HasColumnName("confirmationID");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.NotificationId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("notificationID");

            entity.HasOne(d => d.Notification).WithMany(p => p.BookingConfirmations)
                .HasForeignKey(d => d.NotificationId)
                .HasConstraintName("FK__Booking_C__notif__4AB81AF0");
        });

        modelBuilder.Entity<EventApi>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Event_AP__2DC7BD69AFF47383");

            entity.ToTable("Event_API");

            entity.Property(e => e.EventId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("eventID");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.TicketsAvailable).HasColumnName("ticketsAvailable");
        });

        modelBuilder.Entity<HotelApi>(entity =>
        {
            entity.HasKey(e => e.HotelId).HasName("PK__Hotel_AP__17ADC4920C972DAC");

            entity.ToTable("Hotel_API");

            entity.Property(e => e.HotelId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hotelID");
            entity.Property(e => e.Availability).HasColumnName("availability");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__4BA5CE893AC53EF0");

            entity.ToTable("Notification");

            entity.Property(e => e.NotificationId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("notificationID");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userID");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__userI__3E52440B");
        });

        modelBuilder.Entity<NotificationService>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Notifica__4550733F41E92D90");

            entity.ToTable("NotificationService");

            entity.Property(e => e.ServiceId).HasColumnName("serviceID");
            entity.Property(e => e.NotificationId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("notificationID");
            entity.Property(e => e.ServiceType)
                .HasMaxLength(50)
                .HasColumnName("serviceType");

            entity.HasOne(d => d.Notification).WithMany(p => p.NotificationServices)
                .HasForeignKey(d => d.NotificationId)
                .HasConstraintName("FK__Notificat__notif__44FF419A");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Registra__AB6E616502398166");

            entity.ToTable("Registration");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.VerificationStatus).HasColumnName("verificationStatus");
        });

        modelBuilder.Entity<ResetPassword>(entity =>
        {
            entity.HasKey(e => e.ResetId).HasName("PK__Reset_Pa__429CBC0715EF41B3");

            entity.ToTable("Reset_Password");

            entity.Property(e => e.ResetId).HasColumnName("resetID");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.NotificationId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("notificationID");

            entity.HasOne(d => d.Notification).WithMany(p => p.ResetPasswords)
                .HasForeignKey(d => d.NotificationId)
                .HasConstraintName("FK__Reset_Pas__notif__4D94879B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__CB9A1CDFB27E05AB");

            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userID");
            entity.Property(e => e.BookingHistory).HasColumnName("bookingHistory");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
