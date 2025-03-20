using Microsoft.EntityFrameworkCore;
using Office_supplies_management.Models;

namespace Office_supplies_management.DAL
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Product_Request> Product_Requests { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserType_Permission> UserTypes_Permissions { get; set; }
        public DbSet<Summary> Summaries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product_Request>()
                 .Property(p => p.Product_RequestID)
                 .ValueGeneratedOnAdd();

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Summary)
                .WithMany(s => s.Requests)
                .HasForeignKey(r => r.SummaryID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserID)
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }


    }
}
