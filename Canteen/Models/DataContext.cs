namespace Canteen.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<MenuDetail> MenuDetails { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Dishes)
                .WithOptional(e => e.Category1)
                .HasForeignKey(e => e.Category)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Dish>()
                .HasMany(e => e.MenuDetails)
                .WithOptional(e => e.Dish)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Dish>()
                .HasMany(e => e.OrderDetails)
                .WithOptional(e => e.Dish)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.MenuDetails)
                .WithOptional(e => e.Menu)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithOptional(e => e.Order)
                .WillCascadeOnDelete();

            modelBuilder.Entity<UserRole>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.UserRole)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.User)
                .WillCascadeOnDelete();
        }
    }
}
