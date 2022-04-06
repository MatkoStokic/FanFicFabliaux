using FanFicFabliaux.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FanFicFabliaux.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        protected ApplicationDbContext(){}

        public DbSet<Book> Books { get; set; }
        public DbSet<BookState> BookStates { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BookTag> BookTags { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<BookTag>()
                .HasOne(bt => bt.Book)
                .WithMany(bt => bt.BookTags)
                .HasForeignKey(bt => bt.BookId);

            builder
                .Entity<BookTag>()
                .HasOne(bt => bt.Tag)
                .WithMany(bt => bt.BookTags)
                .HasForeignKey(bt => bt.TagId);

            builder
                .Entity<BookTag>()
                .HasData(
                    new BookTag { Id = 1, TagId = 1, BookId = 1},
                    new BookTag { Id = 2, TagId = 1, BookId = 2 }, 
                    new BookTag { Id = 3, TagId = 1, BookId = 3 }
                );

            builder
                .Entity<Category>()
                .HasData(
                    new Category { Id = 1, CategoryName = "Fantasy"},
                    new Category { Id = 2, CategoryName = "Mystery" },
                    new Category { Id = 3, CategoryName = "Romance" },
                    new Category { Id = 4, CategoryName = "Thriller" }
                );

            builder
                .Entity<Tag>()
                .HasData(
                    new Tag { Id = 1, TagName = "LOTR" },
                    new Tag { Id = 2, TagName = "tolkien" }
                );

            builder
                .Entity<Book>()
                .HasData(
                new Book { Id = 1, Title = "LOTR: Fellowship Of The Ring", Author = "J.R.R. Tolkien", CategoryId = 1 },
                new Book { Id = 2, Title = "LOTR: Two Towers", Author = "J.R.R. Tolkien", CategoryId = 1 },
                new Book { Id = 3, Title = "LOTR: The Return Of The King", Author = "J.R.R. Tolkien", CategoryId = 1 }
            );

        }

    }
}
