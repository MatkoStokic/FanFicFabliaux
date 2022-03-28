using FanFicFabliaux.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FanFicFabliaux.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        protected ApplicationDbContext(){}

        public DbSet<Book> Books { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Book>().HasData(new Book { Id = 1, Title = "LOTR: Fellowship Of The Ring" });
            builder.Entity<Book>().HasData(new Book { Id = 2, Title = "LOTR: Two Towers" });
            builder.Entity<Book>().HasData(new Book { Id = 3, Title = "LOTR: The Return Of The King" });
        }

    }
}
