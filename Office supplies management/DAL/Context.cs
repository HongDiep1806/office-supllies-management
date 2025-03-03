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
        
    }
}
