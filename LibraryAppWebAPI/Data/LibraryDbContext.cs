namespace LibraryAppWebAPI.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.Enums;
using Microsoft.AspNetCore.Identity;

public class LibraryDbContext : IdentityDbContext<Member>  // remove custom Role
{
    public DbSet<Book> Books { get; set; }
    public DbSet<BorrowingRecord> BorrowingRecords { get; set; }
    public DbSet<Member> Members { get; set; }

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=LibraryDatabase.db")
                .EnableSensitiveDataLogging();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Your existing model configs (Books, BorrowingRecords, etc.)
        modelBuilder.Entity<Book>().HasIndex(b => b.ISBN).IsUnique();
        modelBuilder.Entity<Member>().HasIndex(m => m.MembershipId).IsUnique();
        modelBuilder.Entity<Member>().HasIndex(m => m.Email).IsUnique();

        modelBuilder.Entity<BorrowingRecord>(entity =>
        {
            entity.HasKey(br => br.Id);

            entity.HasOne(br => br.Member)
                .WithMany(m => m.BorrowingHistory)
                .HasPrincipalKey(m => m.MembershipId)
                .HasForeignKey(br => br.MemberId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Seed roles with IdentityRole
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "2",
                Name = "User",
                NormalizedName = "USER"
            }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Author = "J.K. Rowling", Genre = Genre.FANTASY, ISBN = "9781856134033", Title = "Harry Potter and The Philosopher's Stone" },
            new Book { Id = 2, Author = "J.R.R. Tolkien", Genre = Genre.FANTASY, ISBN = "9780261103573", Title = "The Fellowship of the Ring" },
            new Book { Id = 3, Author = "George Orwell", Genre = Genre.FICTION, ISBN = "9780451524935", Title = "1984" },
            new Book { Id = 4, Author = "Harper Lee", Genre = Genre.FICTION, ISBN = "9780061120084", Title = "To Kill a Mockingbird" },
            new Book { Id = 5, Author = "Mary Shelley", Genre = Genre.HORROR, ISBN = "9780141439471", Title = "Frankenstein" },
            new Book { Id = 6, Author = "Isaac Asimov", Genre = Genre.SCIENCE_FICTION, ISBN = "9780553293357", Title = "Foundation" },
            new Book { Id = 7, Author = "Douglas Adams", Genre = Genre.SCIENCE_FICTION, ISBN = "9780345391803", Title = "The Hitchhiker's Guide to the Galaxy" },
            new Book { Id = 8, Author = "Leo Tolstoy", Genre = Genre.FICTION, ISBN = "9780199232765", Title = "War and Peace" },
            new Book { Id = 9, Author = "Mark Twain", Genre = Genre.FICTION, ISBN = "9780486280615", Title = "Adventures of Huckleberry Finn" },
            new Book { Id = 10, Author = "Jane Austen", Genre = Genre.ROMANCE, ISBN = "9780141439518", Title = "Pride and Prejudice" }
        );
    }
}