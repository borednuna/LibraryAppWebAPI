using Microsoft.EntityFrameworkCore;
using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.Enums;

namespace LibraryAppWebAPI.Data;

public class LibraryDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<BorrowingRecord> BorrowingRecords { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Rack> Racks { get; set; }

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    public LibraryDbContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=LibraryDatabase.db");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasIndex(b => b.ISBN).IsUnique();

            entity.HasOne(b => b.Rack)
                .WithMany(r => r.Books)
                .HasForeignKey(b => b.RackId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasIndex(m => m.MembershipId).IsUnique();
            entity.HasIndex(m => m.Email).IsUnique();
        });

        modelBuilder.Entity<BorrowingRecord>(entity =>
        {
            entity.HasKey(br => br.Id);

            entity.HasOne(br => br.Book)
                .WithMany(b => b.BorrowingHistory)
                .HasForeignKey(br => br.BookId);

            entity.HasOne(br => br.Member)
                .WithMany(m => m.BorrowingHistory)
                .HasForeignKey(br => br.MemberId);
        });

        modelBuilder.Entity<Rack>()
            .Property(r => r.Section)
            .HasConversion<string>();

        modelBuilder.Entity<Book>()
            .Property(b => b.Genre)
            .HasConversion<string>();

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rack>().HasData(
            new Rack { Id = 1, Capacity = 20, Floor = 1, Section = Section.CHILDREN },
            new Rack { Id = 2, Capacity = 25, Floor = 1, Section = Section.FICTION },
            new Rack { Id = 3, Capacity = 30, Floor = 2, Section = Section.NONFICTION },
            new Rack { Id = 4, Capacity = 15, Floor = 2, Section = Section.REFERENCE },
            new Rack { Id = 5, Capacity = 20, Floor = 3, Section = Section.SCIENCE }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Author = "J.K. Rowling", Genre = Genre.FANTASY, ISBN = "9781856134033", PublicationDate = new DateTime(1997, 6, 26), Publisher = "Bloomsbury", RackId = 1, Title = "Harry Potter and The Philosopher's Stone" },
            new Book { Id = 2, Author = "J.R.R. Tolkien", Genre = Genre.FANTASY, ISBN = "9780261103573", PublicationDate = new DateTime(1954, 7, 29), Publisher = "George Allen & Unwin", RackId = 2, Title = "The Fellowship of the Ring" },
            new Book { Id = 3, Author = "George Orwell", Genre = Genre.FICTION, ISBN = "9780451524935", PublicationDate = new DateTime(1949, 6, 8), Publisher = "Secker & Warburg", RackId = 2, Title = "1984" },
            new Book { Id = 4, Author = "Harper Lee", Genre = Genre.FICTION, ISBN = "9780061120084", PublicationDate = new DateTime(1960, 7, 11), Publisher = "J.B. Lippincott & Co.", RackId = 2, Title = "To Kill a Mockingbird" },
            new Book { Id = 5, Author = "Mary Shelley", Genre = Genre.HORROR, ISBN = "9780141439471", PublicationDate = new DateTime(1818, 1, 1), Publisher = "Lackington, Hughes, Harding, Mavor & Jones", RackId = 5, Title = "Frankenstein" },
            new Book { Id = 6, Author = "Isaac Asimov", Genre = Genre.SCIENCE_FICTION, ISBN = "9780553293357", PublicationDate = new DateTime(1951, 6, 1), Publisher = "Gnome Press", RackId = 5, Title = "Foundation" },
            new Book { Id = 7, Author = "Douglas Adams", Genre = Genre.SCIENCE_FICTION, ISBN = "9780345391803", PublicationDate = new DateTime(1979, 10, 12), Publisher = "Pan Books", RackId = 2, Title = "The Hitchhiker's Guide to the Galaxy" },
            new Book { Id = 8, Author = "Leo Tolstoy", Genre = Genre.FICTION, ISBN = "9780199232765", PublicationDate = new DateTime(1869, 1, 1), Publisher = "The Russian Messenger", RackId = 3, Title = "War and Peace" },
            new Book { Id = 9, Author = "Mark Twain", Genre = Genre.FICTION, ISBN = "9780486280615", PublicationDate = new DateTime(1884, 12, 10), Publisher = "Chatto & Windus", RackId = 3, Title = "Adventures of Huckleberry Finn" },
            new Book { Id = 10, Author = "Jane Austen", Genre = Genre.ROMANCE, ISBN = "9780141439518", PublicationDate = new DateTime(1813, 1, 28), Publisher = "T. Egerton", RackId = 3, Title = "Pride and Prejudice" },
            new Book { Id = 11, Author = "Ernest Hemingway", Genre = Genre.FICTION, ISBN = "9780684801223", PublicationDate = new DateTime(1952, 9, 1), Publisher = "Charles Scribner's Sons", RackId = 3, Title = "The Old Man and the Sea" },
            new Book { Id = 12, Author = "Agatha Christie", Genre = Genre.MYSTERY, ISBN = "9780007119356", PublicationDate = new DateTime(1934, 1, 1), Publisher = "Collins Crime Club", RackId = 4, Title = "Murder on the Orient Express" },
            new Book { Id = 13, Author = "Dan Brown", Genre = Genre.THRILLER, ISBN = "9780385504201", PublicationDate = new DateTime(2003, 3, 18), Publisher = "Doubleday", RackId = 4, Title = "The Da Vinci Code" },
            new Book { Id = 14, Author = "Stephen King", Genre = Genre.HORROR, ISBN = "9780451169525", PublicationDate = new DateTime(1977, 1, 28), Publisher = "Doubleday", RackId = 5, Title = "The Shining" },
            new Book { Id = 15, Author = "Suzanne Collins", Genre = Genre.YOUNG_ADULT, ISBN = "9780439023481", PublicationDate = new DateTime(2008, 9, 14), Publisher = "Scholastic Press", RackId = 1, Title = "The Hunger Games" },
            new Book { Id = 16, Author = "C.S. Lewis", Genre = Genre.FANTASY, ISBN = "9780064471190", PublicationDate = new DateTime(1950, 10, 16), Publisher = "Geoffrey Bles", RackId = 1, Title = "The Lion, the Witch and the Wardrobe" },
            new Book { Id = 17, Author = "H.G. Wells", Genre = Genre.SCIENCE_FICTION, ISBN = "9780553213515", PublicationDate = new DateTime(1895, 1, 1), Publisher = "William Heinemann", RackId = 5, Title = "The Time Machine" },
            new Book { Id = 18, Author = "John Steinbeck", Genre = Genre.FICTION, ISBN = "9780140177398", PublicationDate = new DateTime(1937, 2, 6), Publisher = "Covici Friede", RackId = 3, Title = "Of Mice and Men" },
            new Book { Id = 19, Author = "Arthur Conan Doyle", Genre = Genre.MYSTERY, ISBN = "9780451528019", PublicationDate = new DateTime(1892, 10, 14), Publisher = "George Newnes", RackId = 4, Title = "The Adventures of Sherlock Holmes" },
            new Book { Id = 20, Author = "Ray Bradbury", Genre = Genre.SCIENCE_FICTION, ISBN = "9781451673319", PublicationDate = new DateTime(1953, 10, 19), Publisher = "Ballantine Books", RackId = 5, Title = "Fahrenheit 451" },
            new Book { Id = 21, Author = "William Golding", Genre = Genre.FICTION, ISBN = "9780399501487", PublicationDate = new DateTime(1954, 9, 17), Publisher = "Faber and Faber", RackId = 2, Title = "Lord of the Flies" },
            new Book { Id = 22, Author = "Charles Dickens", Genre = Genre.FICTION, ISBN = "9780141439600", PublicationDate = new DateTime(1861, 8, 1), Publisher = "Chapman & Hall", RackId = 3, Title = "Great Expectations" },
            new Book { Id = 23, Author = "Emily Brontë", Genre = Genre.ROMANCE, ISBN = "9780141439556", PublicationDate = new DateTime(1847, 12, 1), Publisher = "Thomas Cautley Newby", RackId = 3, Title = "Wuthering Heights" },
            new Book { Id = 24, Author = "Homer", Genre = Genre.HISTORY, ISBN = "9780140268867", PublicationDate = new DateTime(1500, 1, 1), Publisher = "Ancient Greece", RackId = 3, Title = "The Odyssey" },
            new Book { Id = 25, Author = "Victor Hugo", Genre = Genre.FICTION, ISBN = "9780451419430", PublicationDate = new DateTime(1862, 4, 3), Publisher = "A. Lacroix, Verboeckhoven & Cie.", RackId = 3, Title = "Les Misérables" },
            new Book { Id = 26, Author = "Bram Stoker", Genre = Genre.HORROR, ISBN = "9780486411095", PublicationDate = new DateTime(1897, 5, 26), Publisher = "Archibald Constable and Company", RackId = 5, Title = "Dracula" },
            new Book { Id = 27, Author = "Aldous Huxley", Genre = Genre.SCIENCE_FICTION, ISBN = "9780060850524", PublicationDate = new DateTime(1932, 8, 18), Publisher = "Chatto & Windus", RackId = 2, Title = "Brave New World" },
            new Book { Id = 28, Author = "Jules Verne", Genre = Genre.SCIENCE_FICTION, ISBN = "9780553213973", PublicationDate = new DateTime(1870, 6, 20), Publisher = "Pierre-Jules Hetzel", RackId = 5, Title = "Twenty Thousand Leagues Under the Seas" },
            new Book { Id = 29, Author = "Miguel de Cervantes", Genre = Genre.FICTION, ISBN = "9780060934347", PublicationDate = new DateTime(1605, 1, 16), Publisher = "Francisco de Robles", RackId = 3, Title = "Don Quixote" },
            new Book { Id = 30, Author = "George R.R. Martin", Genre = Genre.FANTASY, ISBN = "9780553103540", PublicationDate = new DateTime(1996, 8, 6), Publisher = "Bantam Spectra", RackId = 2, Title = "A Game of Thrones" }
        );

        modelBuilder.Entity<Member>().HasData(
            new Member { Id = 1, Name = "One", Email = "user1@gmail.com", Password = "Pass123", MembershipDate = new DateTime(2020, 1, 15), MembershipId = Guid.Parse("11111111-1111-1111-1111-111111111111") },
            new Member { Id = 2, Name = "Two", Email = "user2@gmail.com", Password = "Pass123", MembershipDate = new DateTime(2021, 5, 20), MembershipId = Guid.Parse("22222222-2222-2222-2222-222222222222") },
            new Member { Id = 3, Name = "Three", Email = "user3@gmail.com", Password = "Pass123", MembershipDate = new DateTime(2022, 3, 12), MembershipId = Guid.Parse("33333333-3333-3333-3333-333333333333") },
            new Member { Id = 4, Name = "Four", Email = "user4@gmail.com", Password = "Pass123", MembershipDate = new DateTime(2023, 7, 8), MembershipId = Guid.Parse("44444444-4444-4444-4444-444444444444") },
            new Member { Id = 5, Name = "Five", Email = "user5@gmail.com", Password = "Pass123", MembershipDate = new DateTime(2024, 2, 27), MembershipId = Guid.Parse("55555555-5555-5555-5555-555555555555") }
        );
    }
}