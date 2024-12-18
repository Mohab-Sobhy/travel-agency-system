using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace travel_agency_system.Models;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationQueue> NotificationQueues { get; set; }

    public virtual DbSet<NotificationStatistic> NotificationStatistics { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Template> Templates { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=TravelAgencyDataBase;User ID=sa;Password=!Mohab#123;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951ACDC58860DB");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Type).HasMaxLength(20);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Event).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Bookings__EventI__47DBAE45");

            entity.HasOne(d => d.Room).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__Bookings__RoomID__46E78A0C");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__UserID__45F365D3");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Events__7944C870D76D258D");

            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.HotelId).HasName("PK__Hotels__46023BBFD2EA63B6");

            entity.Property(e => e.HotelId).HasColumnName("HotelID");
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E328CC9A5B7");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TemplateId).HasColumnName("TemplateID");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Type).HasMaxLength(20);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Template).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.TemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__Templ__5070F446");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__UserI__5165187F");
        });

        modelBuilder.Entity<NotificationQueue>(entity =>
        {
            entity.HasKey(e => e.QueueId).HasName("PK__Notifica__8324E8F5B735AD41");

            entity.ToTable("NotificationQueue");

            entity.Property(e => e.QueueId).HasColumnName("QueueID");
            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Notification).WithMany(p => p.NotificationQueues)
                .HasForeignKey(d => d.NotificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__Notif__5629CD9C");
        });

        modelBuilder.Entity<NotificationStatistic>(entity =>
        {
            entity.HasKey(e => e.StatId).HasName("PK__Notifica__3A162D1E39F127D3");

            entity.Property(e => e.StatId).HasColumnName("StatID");
            entity.Property(e => e.Metric).HasMaxLength(100);
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Value).HasMaxLength(100);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Rooms__328639191E0E48C1");

            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.HotelId).HasColumnName("HotelID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RoomType).HasMaxLength(20);

            entity.HasOne(d => d.Hotel).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK__Rooms__HotelID__3E52440B");
        });

        modelBuilder.Entity<Template>(entity =>
        {
            entity.HasKey(e => e.TemplateId).HasName("PK__Template__F87ADD07A819C28F");

            entity.Property(e => e.TemplateId).HasColumnName("TemplateID");
            entity.Property(e => e.Channel).HasMaxLength(20);
            entity.Property(e => e.Language).HasMaxLength(10);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC8048AD0D");

            entity.HasIndex(e => e.Phone, "UQ__Users__5C7E359E6B005603").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534D0F03EDC").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
