using Microsoft.EntityFrameworkCore;
using UNIFY.Identity;
using UNIFY.Model.Entities;

namespace UNIFY.Context
{
    public class ApplicationContext : DbContext
    {
        //protected internal virtual void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("server=localhost;user=root;database=UNIFY;port=3306;password=oyeyemialabi22311");
        //}

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<MarketPlace> MarketPlaces { get; set; }
        public DbSet<Security> Securities { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<SecurityAgency> SecurityAgencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<User>()
                .HasOne<Admin>(b => b.Admin)
                .WithOne(d => d.User)
                .HasForeignKey<Admin>(d => d.UserId);

            modelBuilder.Entity<Community>()
               .HasOne<MarketPlace>(b => b.MarketPlace)
               .WithOne(d => d.Community)
               .HasForeignKey<MarketPlace>(d => d.CommunityId);

           

            

            modelBuilder.Entity<User>()
               .HasOne<Member>(b => b.Member)
               .WithOne(d => d.User)
               .HasForeignKey<Member>(d => d.UserId);
        }
    }
}
