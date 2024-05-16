using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ULMS2.Models;

namespace ULMS2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Account>(entity => { entity.ToTable("Accounts"); });
            builder.Entity<Faculty>(entity => { entity.ToTable("Faculty"); });
            builder.Entity<Librarian>(entity => { entity.ToTable("Librarians"); });
            builder.Entity<Student>(entity => { entity.ToTable("Students"); });
        }
    }
}