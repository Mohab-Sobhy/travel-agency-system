using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace travel_agency_system.Models
{
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
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code.
            optionsBuilder.UseSqlServer("Server=localhost;Database=TravelAgencyDataBase;User ID=sa;Password=!Mohab#123;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // إعداد كيانات User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Users");

                entity.HasIndex(e => e.PhoneNumber).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();

                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
                entity.Property(e => e.Name).HasMaxLength(100);

                // العلاقة مع Bookings و Notifications
                entity.HasMany(e => e.Bookings)
                    .WithOne(b => b.User)
                    .HasForeignKey(b => b.UserId) // يطابق المفتاح الأجنبي مع UserId من نوع string
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Notifications)
                    .WithOne(n => n.User)
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // إعداد كيانات Booking
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.BookingId).HasName("PK_Bookings");

                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.EventId).HasColumnName("EventID");
                entity.Property(e => e.RoomId).HasColumnName("RoomID");
                entity.Property(e => e.Status).HasMaxLength(20).HasDefaultValue("Pending");
                entity.Property(e => e.Timestamp).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
                entity.Property(e => e.Type).HasMaxLength(20);

                // العلاقات مع Event و Room و User
                entity.HasOne(d => d.Event).WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Bookings_EventID");

                entity.HasOne(d => d.Room).WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_Bookings_RoomID");

                entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bookings_UserID");
            });

            // إعداد كيانات Event
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.EventId).HasName("PK_Events");

                entity.Property(e => e.EventId).HasColumnName("EventID");
                entity.Property(e => e.Location).HasMaxLength(100);
                entity.Property(e => e.Name).HasMaxLength(100);
            });

            // إعداد كيانات Hotel
            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(e => e.HotelId).HasName("PK_Hotels");

                entity.Property(e => e.HotelId).HasColumnName("HotelID");
                entity.Property(e => e.Location).HasMaxLength(100);
                entity.Property(e => e.Name).HasMaxLength(100);
            });

            // إعداد كيانات Notification
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.NotificationId).HasName("PK_Notifications");

                entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
                entity.Property(e => e.Status).HasMaxLength(20).HasDefaultValue("Pending");
                entity.Property(e => e.TemplateId).HasColumnName("TemplateID");
                entity.Property(e => e.Timestamp).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
                entity.Property(e => e.Type).HasMaxLength(20);
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Template).WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.TemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notifications_TemplateID");

                entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notifications_UserID");
            });

            // إعداد كيانات NotificationQueue
            modelBuilder.Entity<NotificationQueue>(entity =>
            {
                entity.HasKey(e => e.QueueId).HasName("PK_NotificationQueue");

                entity.Property(e => e.QueueId).HasColumnName("QueueID");
                entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
                entity.Property(e => e.Status).HasMaxLength(20).HasDefaultValue("Pending");
                entity.Property(e => e.Timestamp).HasDefaultValueSql("(getdate())").HasColumnType("datetime");

                entity.HasOne(d => d.Notification).WithMany(p => p.NotificationQueues)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotificationQueue_NotificationID");
            });

            // إعداد كيانات NotificationStatistic
            modelBuilder.Entity<NotificationStatistic>(entity =>
            {
                entity.HasKey(e => e.StatId).HasName("PK_NotificationStatistics");

                entity.Property(e => e.StatId).HasColumnName("StatID");
                entity.Property(e => e.Metric).HasMaxLength(100);
                entity.Property(e => e.Timestamp).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
                entity.Property(e => e.Value).HasMaxLength(100);
            });

            // إعداد كيانات Room
            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.RoomId).HasName("PK_Rooms");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");
                entity.Property(e => e.HotelId).HasColumnName("HotelID");
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.RoomType).HasMaxLength(20);

                entity.HasOne(d => d.Hotel).WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_Rooms_HotelID");
            });

            // إعداد كيانات Template
            modelBuilder.Entity<Template>(entity =>
            {
                entity.HasKey(e => e.TemplateId).HasName("PK_Templates");

                entity.Property(e => e.TemplateId).HasColumnName("TemplateID");
                entity.Property(e => e.Channel).HasMaxLength(20);
                entity.Property(e => e.Language).HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
