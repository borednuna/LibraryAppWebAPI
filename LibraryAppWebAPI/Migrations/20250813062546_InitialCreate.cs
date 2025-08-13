using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryAppWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MembershipId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    MembershipDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Racks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Floor = table.Column<int>(type: "INTEGER", nullable: false),
                    Section = table.Column<string>(type: "TEXT", nullable: false),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    ISBN = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Publisher = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RackId = table.Column<int>(type: "INTEGER", nullable: true),
                    Genre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Racks_RackId",
                        column: x => x.RackId,
                        principalTable: "Racks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "BorrowingRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberId = table.Column<int>(type: "INTEGER", nullable: false),
                    BorrowDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FineAmount = table.Column<decimal>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowingRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowingRecords_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowingRecords_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Email", "IsActive", "MembershipDate", "MembershipId", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "user1@gmail.com", true, new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), "One", "Pass123" },
                    { 2, "user2@gmail.com", true, new DateTime(2021, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22222222-2222-2222-2222-222222222222"), "Two", "Pass123" },
                    { 3, "user3@gmail.com", true, new DateTime(2022, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-3333-3333-3333-333333333333"), "Three", "Pass123" },
                    { 4, "user4@gmail.com", true, new DateTime(2023, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("44444444-4444-4444-4444-444444444444"), "Four", "Pass123" },
                    { 5, "user5@gmail.com", true, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("55555555-5555-5555-5555-555555555555"), "Five", "Pass123" }
                });

            migrationBuilder.InsertData(
                table: "Racks",
                columns: new[] { "Id", "Capacity", "DeletedAt", "Floor", "Section" },
                values: new object[,]
                {
                    { 1, 20, null, 1, "CHILDREN" },
                    { 2, 25, null, 1, "FICTION" },
                    { 3, 30, null, 2, "NONFICTION" },
                    { 4, 15, null, 2, "REFERENCE" },
                    { 5, 20, null, 3, "SCIENCE" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "DeletedAt", "Genre", "ISBN", "PublicationDate", "Publisher", "RackId", "Title" },
                values: new object[,]
                {
                    { 1, "J.K. Rowling", null, "FANTASY", "9781856134033", new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bloomsbury", 1, "Harry Potter and The Philosopher's Stone" },
                    { 2, "J.R.R. Tolkien", null, "FANTASY", "9780261103573", new DateTime(1954, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "George Allen & Unwin", 2, "The Fellowship of the Ring" },
                    { 3, "George Orwell", null, "FICTION", "9780451524935", new DateTime(1949, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Secker & Warburg", 2, "1984" },
                    { 4, "Harper Lee", null, "FICTION", "9780061120084", new DateTime(1960, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "J.B. Lippincott & Co.", 2, "To Kill a Mockingbird" },
                    { 5, "Mary Shelley", null, "HORROR", "9780141439471", new DateTime(1818, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lackington, Hughes, Harding, Mavor & Jones", 5, "Frankenstein" },
                    { 6, "Isaac Asimov", null, "SCIENCE_FICTION", "9780553293357", new DateTime(1951, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gnome Press", 5, "Foundation" },
                    { 7, "Douglas Adams", null, "SCIENCE_FICTION", "9780345391803", new DateTime(1979, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pan Books", 2, "The Hitchhiker's Guide to the Galaxy" },
                    { 8, "Leo Tolstoy", null, "FICTION", "9780199232765", new DateTime(1869, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Russian Messenger", 3, "War and Peace" },
                    { 9, "Mark Twain", null, "FICTION", "9780486280615", new DateTime(1884, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chatto & Windus", 3, "Adventures of Huckleberry Finn" },
                    { 10, "Jane Austen", null, "ROMANCE", "9780141439518", new DateTime(1813, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "T. Egerton", 3, "Pride and Prejudice" },
                    { 11, "Ernest Hemingway", null, "FICTION", "9780684801223", new DateTime(1952, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Charles Scribner's Sons", 3, "The Old Man and the Sea" },
                    { 12, "Agatha Christie", null, "MYSTERY", "9780007119356", new DateTime(1934, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Collins Crime Club", 4, "Murder on the Orient Express" },
                    { 13, "Dan Brown", null, "THRILLER", "9780385504201", new DateTime(2003, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doubleday", 4, "The Da Vinci Code" },
                    { 14, "Stephen King", null, "HORROR", "9780451169525", new DateTime(1977, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doubleday", 5, "The Shining" },
                    { 15, "Suzanne Collins", null, "YOUNG_ADULT", "9780439023481", new DateTime(2008, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Scholastic Press", 1, "The Hunger Games" },
                    { 16, "C.S. Lewis", null, "FANTASY", "9780064471190", new DateTime(1950, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Geoffrey Bles", 1, "The Lion, the Witch and the Wardrobe" },
                    { 17, "H.G. Wells", null, "SCIENCE_FICTION", "9780553213515", new DateTime(1895, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "William Heinemann", 5, "The Time Machine" },
                    { 18, "John Steinbeck", null, "FICTION", "9780140177398", new DateTime(1937, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Covici Friede", 3, "Of Mice and Men" },
                    { 19, "Arthur Conan Doyle", null, "MYSTERY", "9780451528019", new DateTime(1892, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "George Newnes", 4, "The Adventures of Sherlock Holmes" },
                    { 20, "Ray Bradbury", null, "SCIENCE_FICTION", "9781451673319", new DateTime(1953, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ballantine Books", 5, "Fahrenheit 451" },
                    { 21, "William Golding", null, "FICTION", "9780399501487", new DateTime(1954, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faber and Faber", 2, "Lord of the Flies" },
                    { 22, "Charles Dickens", null, "FICTION", "9780141439600", new DateTime(1861, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chapman & Hall", 3, "Great Expectations" },
                    { 23, "Emily Brontë", null, "ROMANCE", "9780141439556", new DateTime(1847, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thomas Cautley Newby", 3, "Wuthering Heights" },
                    { 24, "Homer", null, "HISTORY", "9780140268867", new DateTime(1500, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ancient Greece", 3, "The Odyssey" },
                    { 25, "Victor Hugo", null, "FICTION", "9780451419430", new DateTime(1862, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "A. Lacroix, Verboeckhoven & Cie.", 3, "Les Misérables" },
                    { 26, "Bram Stoker", null, "HORROR", "9780486411095", new DateTime(1897, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Archibald Constable and Company", 5, "Dracula" },
                    { 27, "Aldous Huxley", null, "SCIENCE_FICTION", "9780060850524", new DateTime(1932, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chatto & Windus", 2, "Brave New World" },
                    { 28, "Jules Verne", null, "SCIENCE_FICTION", "9780553213973", new DateTime(1870, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pierre-Jules Hetzel", 5, "Twenty Thousand Leagues Under the Seas" },
                    { 29, "Miguel de Cervantes", null, "FICTION", "9780060934347", new DateTime(1605, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Francisco de Robles", 3, "Don Quixote" },
                    { 30, "George R.R. Martin", null, "FANTASY", "9780553103540", new DateTime(1996, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bantam Spectra", 2, "A Game of Thrones" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_ISBN",
                table: "Books",
                column: "ISBN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_RackId",
                table: "Books",
                column: "RackId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowingRecords_BookId",
                table: "BorrowingRecords",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowingRecords_MemberId",
                table: "BorrowingRecords",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_Email",
                table: "Members",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_MembershipId",
                table: "Members",
                column: "MembershipId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowingRecords");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Racks");
        }
    }
}
