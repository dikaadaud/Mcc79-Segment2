using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {

        }

        // Table
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Education> Educations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Constraint Unique
            modelBuilder.Entity<Employee>()
                        .HasIndex(e => new
                        {
                            e.Nik,
                            e.Email,
                            e.PhoneNumber
                        });

            modelBuilder.Entity<University>()
                        .HasMany(e => e.Educations)
                        .WithOne(u => u.University)
                        .HasForeignKey(edu => edu.UniversityGuid);

            modelBuilder.Entity<Role>()
                        .HasMany(ar => ar.AccountRoles)
                        .WithOne(r => r.Role)
                        .HasForeignKey(role => role.RoleGuid);

            modelBuilder.Entity<Account>()
                        .HasMany(ar => ar.AccountRoles)
                        .WithOne(a => a.Account)
                        .HasForeignKey(acc => acc.AccountGuid);

            modelBuilder.Entity<Room>()
                        .HasMany(bk => bk.Bookings)
                        .WithOne(room => room.Room)
                        .HasForeignKey(room => room.RoomGuid);

            modelBuilder.Entity<Account>()
                        .HasOne(e => e.Employee)
                        .WithOne(a => a.Account)
                        .HasForeignKey<Account>(a => a.Guid);

            modelBuilder.Entity<Employee>()
                        .HasMany(bk => bk.Bookings)
                        .WithOne(e => e.Employee)
                        .HasForeignKey(e => e.EmployeeGuid);

            modelBuilder.Entity<Education>()
                        .HasOne(e => e.Employee)
                        .WithOne(edu => edu.Education)
                        .HasForeignKey<Education>(edu => edu.Guid);

        }
    }
}
