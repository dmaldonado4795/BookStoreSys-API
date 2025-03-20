using BookStoreSys_API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreSys_API.Infrastructure.Context
{
    public class BookstoreContext(DbContextOptions<BookstoreContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookModel>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<BookModel>()
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId);

            modelBuilder.Entity<AuthorModel>()
                .HasOne(a => a.Nationality)
                .WithMany(n => n.Authors)
                .HasForeignKey(a => a.NationalityId);
        }

        public DbSet<AuthorModel> Authors { get; set; }
        public DbSet<BookModel> Books { get; set; }
        public DbSet<GenreModel> Genres { get; set; }
        public DbSet<NationalityModel> Nationalities { get; set; }
    }
}
